#!/usr/bin/env python3
"""Small constraint-slot planning probe for Arrow role maps.

This is intentionally independent of Unity assets. It reads a RoleMap cells CSV
and tries to plan a high-coverage set of non-overlapping chains using static
head/body/outer/critical constraints. The output is a diagnostic role plan, not
a production level.
"""

from __future__ import annotations

import argparse
import csv
import math
import os
import random
from dataclasses import dataclass, field
from typing import Dict, Iterable, List, Sequence, Set, Tuple


DIRS = {
    "U": (0, 1),
    "D": (0, -1),
    "L": (-1, 0),
    "R": (1, 0),
}

ALLOW_CRITICAL_BODY = False
ALLOW_ALL_BODY = False
ALLOW_ANY_HEAD = False


@dataclass(frozen=True)
class Cell:
    index: int
    x: int
    y: int
    region: str
    outer_side: str
    occupied: bool
    primary_role: str
    head_anchored_dirs: str
    head_direct_outer_dirs: str
    head_hits_owners: str
    head_potential: int
    body_blocker_potential: int
    active_ray_pass_count: int
    critical_ray_pass_count: int
    direct_exit_ray_pass_count: int
    fill_priority: str


@dataclass(frozen=True)
class Candidate:
    path: Tuple[int, ...]
    head_dir: str
    score: float
    outer_head: bool
    anchored_head: bool
    pressure: int
    label: str


@dataclass
class State:
    used: Set[int]
    candidates: List[Candidate] = field(default_factory=list)
    score: float = 0.0
    outer_heads: int = 0
    anchored_heads: int = 0
    pressure: int = 0
    fingerprint: Tuple[int, ...] = field(default_factory=tuple)


def parse_bool(value: str) -> bool:
    return str(value).strip().lower() == "true"


def parse_int(value: str, default: int = 0) -> int:
    try:
        return int(float(str(value).strip()))
    except Exception:
        return default


def dir_tokens(raw: str) -> Set[str]:
    text = str(raw or "").strip()
    if not text:
        return set()
    tokens: Set[str] = set()
    for part in text.replace(";", ",").replace("|", ",").split(","):
        p = part.strip().upper()
        if p in DIRS:
            tokens.add(p)
    if not tokens:
        for ch in text.upper():
            if ch in DIRS:
                tokens.add(ch)
    return tokens


def read_cells(path: str) -> Tuple[Dict[int, Cell], int, int]:
    cells: Dict[int, Cell] = {}
    with open(path, newline="", encoding="utf-8-sig") as f:
        for row in csv.DictReader(f):
            c = Cell(
                index=parse_int(row.get("index", -1), -1),
                x=parse_int(row.get("x", 0), 0),
                y=parse_int(row.get("y", 0), 0),
                region=str(row.get("region", "")),
                outer_side=str(row.get("outerSide", "")),
                occupied=parse_bool(row.get("occupied", "false")),
                primary_role=str(row.get("primaryRole", "")),
                head_anchored_dirs=str(row.get("headAnchoredDirections", "")),
                head_direct_outer_dirs=str(row.get("headDirectOuterDirections", "")),
                head_hits_owners=str(row.get("headHitsOwners", "")),
                head_potential=parse_int(row.get("headPotential", 0)),
                body_blocker_potential=parse_int(row.get("bodyBlockerPotential", 0)),
                active_ray_pass_count=parse_int(row.get("activeRayPassCount", 0)),
                critical_ray_pass_count=parse_int(row.get("criticalRayPassCount", 0)),
                direct_exit_ray_pass_count=parse_int(row.get("directExitRayPassCount", 0)),
                fill_priority=str(row.get("fillPriority", "")),
            )
            if c.index >= 0:
                cells[c.index] = c
    if not cells:
        raise ValueError(f"No cells read from {path}")
    width = max(c.x for c in cells.values()) + 1
    height = max(c.y for c in cells.values()) + 1
    return cells, width, height


def idx(x: int, y: int, width: int) -> int:
    return y * width + x


def xy(index: int, width: int) -> Tuple[int, int]:
    return index % width, index // width


def in_bounds(x: int, y: int, width: int, height: int) -> bool:
    return 0 <= x < width and 0 <= y < height


def role_allowed_body(cell: Cell) -> bool:
    if cell.occupied:
        return False
    if ALLOW_ALL_BODY:
        return True
    if cell.primary_role in {"OccupiedCriticalChain", "OccupiedExistingChain"}:
        return False
    if cell.primary_role == "CriticalTimingZone" and not ALLOW_CRITICAL_BODY:
        return False
    return True


def role_allowed_head(cell: Cell, allow_free_exit: bool, allow_safe_heads: bool) -> bool:
    if not role_allowed_body(cell):
        return False
    if ALLOW_ANY_HEAD:
        return True
    if cell.primary_role in {"HeadAllowed", "GuardSlot", "BodyOnlyRayBlocker"}:
        return True
    if allow_safe_heads and cell.primary_role == "SafeFillZone":
        return True
    if allow_free_exit and cell.primary_role == "HighRiskFreeHead":
        return True
    return False


def head_dirs_for(cell: Cell, allow_free_exit: bool, allow_safe_heads: bool) -> Set[str]:
    if ALLOW_ANY_HEAD:
        return set(DIRS)
    anchored = dir_tokens(cell.head_anchored_dirs)
    direct = dir_tokens(cell.head_direct_outer_dirs)
    if anchored:
        return anchored
    if cell.primary_role in {"GuardSlot", "BodyOnlyRayBlocker", "HeadAllowed"}:
        if direct:
            return direct | set(DIRS)
        return set(DIRS)
    if allow_safe_heads and cell.primary_role == "SafeFillZone":
        return set(DIRS)
    if allow_free_exit and cell.primary_role == "HighRiskFreeHead":
        return direct or set(DIRS)
    return set()


def generate_walks(
    head: Cell,
    head_dir: str,
    cells: Dict[int, Cell],
    width: int,
    height: int,
    min_len: int,
    max_len: int,
    rng: random.Random,
    random_walks_per_head: int,
) -> Iterable[Tuple[int, ...]]:
    """Generate simple straight, one-turn, and random-walk paths.

    Path starts at the head. Body initially extends opposite the arrow direction,
    matching the project convention used by existing scripts.
    """
    dx, dy = DIRS[head_dir]
    start_dir = (-dx, -dy)
    h = head.index

    def extend(seq_dirs: Sequence[Tuple[int, int]], length: int) -> Tuple[int, ...] | None:
        path = [h]
        x, y = head.x, head.y
        seen = {h}
        for step in range(1, length):
            sx, sy = seq_dirs[min(step - 1, len(seq_dirs) - 1)]
            x += sx
            y += sy
            if not in_bounds(x, y, width, height):
                return None
            ni = idx(x, y, width)
            if ni in seen:
                return None
            cell = cells.get(ni)
            if cell is None or not role_allowed_body(cell):
                return None
            seen.add(ni)
            path.append(ni)
        return tuple(path)

    for length in range(min_len, max_len + 1):
        straight = extend([start_dir], length)
        if straight is not None:
            yield straight
        for turn in range(1, length - 1):
            for td in ((start_dir[1], -start_dir[0]), (-start_dir[1], start_dir[0])):
                seq = [start_dir] * turn + [td] * (length - turn)
                p = extend(seq, length)
                if p is not None:
                    yield p

    directions = list(DIRS.values())
    for _ in range(random_walks_per_head):
        length = rng.randint(min_len, max_len)
        seq = [start_dir]
        last = start_dir
        for _step in range(1, length - 1):
            options = [d for d in directions if d != (-last[0], -last[1])]
            if rng.random() < 0.58:
                d = last
            else:
                d = rng.choice(options)
            seq.append(d)
            last = d
        p = extend(seq, length)
        if p is not None:
            yield p


def score_candidate(path: Tuple[int, ...], head_dir: str, cells: Dict[int, Cell]) -> Candidate:
    head = cells[path[0]]
    anchored = head_dir in dir_tokens(head.head_anchored_dirs)
    direct_outer = head_dir in dir_tokens(head.head_direct_outer_dirs)
    pressure = 0
    ray_pressure = 0
    for i in path:
        c = cells[i]
        pressure += c.body_blocker_potential + c.head_potential
        ray_pressure += c.active_ray_pass_count + c.critical_ray_pass_count * 2
    score = len(path) * 10 + pressure * 5 + ray_pressure * 1.5
    if anchored:
        score += 90
    if head.primary_role == "GuardSlot":
        score += 35
    if head.primary_role == "BodyOnlyRayBlocker":
        score += 20
    if direct_outer and not anchored:
        score -= 110
    if head.primary_role == "HighRiskFreeHead":
        score -= 75
    if head.primary_role == "SafeFillZone":
        score -= 55
    bends = 0
    prev = None
    coords = [xy(i, width_hint) for i in path] if False else []
    del coords
    # Mildly reward non-trivial chain language without turning it into the goal.
    for a, b, c in zip(path, path[1:], path[2:]):
        ax, ay = xy(a, global_width_for_scoring)
        bx, by = xy(b, global_width_for_scoring)
        cx, cy = xy(c, global_width_for_scoring)
        d1 = (bx - ax, by - ay)
        d2 = (cx - bx, cy - by)
        if prev is not None and d1 != prev:
            bends += 1
        if d1 != d2:
            bends += 1
        prev = d2
    if bends == 0 and len(path) >= 5:
        score -= 20
    elif bends >= 1:
        score += min(20, bends * 5)
    label = "anchored" if anchored else ("outer" if direct_outer else "plain")
    return Candidate(path=path, head_dir=head_dir, score=score, outer_head=(direct_outer and not anchored), anchored_head=anchored, pressure=pressure, label=label)


global_width_for_scoring = 1


def empty_components(cells: Dict[int, Cell], used: Set[int], width: int, height: int) -> Tuple[int, int, int]:
    free = {
        i
        for i, c in cells.items()
        if i not in used and role_allowed_body(c)
    }
    components = 0
    small = 0
    largest = 0
    while free:
        start = next(iter(free))
        stack = [start]
        free.remove(start)
        size = 0
        while stack:
            cur = stack.pop()
            size += 1
            x, y = xy(cur, width)
            for dx, dy in DIRS.values():
                nx, ny = x + dx, y + dy
                ni = idx(nx, ny, width)
                if in_bounds(nx, ny, width, height) and ni in free:
                    free.remove(ni)
                    stack.append(ni)
        components += 1
        largest = max(largest, size)
        if size <= 3:
            small += 1
    return components, small, largest


def plan(
    cells: Dict[int, Cell],
    width: int,
    height: int,
    candidates: List[Candidate],
    beam_width: int,
    max_steps: int,
    target_coverage: float,
    outer_budget: int,
) -> List[State]:
    fixed_used = {i for i, c in cells.items() if c.occupied}
    total = width * height
    states = [State(used=set(fixed_used))]
    candidates_by_quality = sorted(candidates, key=lambda c: c.score, reverse=True)
    for _step in range(max_steps):
        next_states: List[State] = []
        for state in states:
            if len(state.used) / total >= target_coverage:
                next_states.append(state)
                continue
            compatible = []
            for cand in candidates_by_quality:
                if cand.outer_head and state.outer_heads >= outer_budget:
                    continue
                if any(i in state.used for i in cand.path):
                    continue
                compatible.append(cand)
                if len(compatible) >= 36:
                    break
            if not compatible:
                next_states.append(state)
                continue
            for cand in compatible:
                new_used = set(state.used)
                new_used.update(cand.path)
                comps, small, largest = empty_components(cells, new_used, width, height)
                frag_penalty = comps * 12 + small * 18
                coverage = len(new_used) / total
                next_states.append(
                    State(
                        used=new_used,
                        candidates=state.candidates + [cand],
                        score=state.score + cand.score + coverage * 60 - frag_penalty,
                        outer_heads=state.outer_heads + (1 if cand.outer_head else 0),
                        anchored_heads=state.anchored_heads + (1 if cand.anchored_head else 0),
                        pressure=state.pressure + cand.pressure,
                    )
                )
        # Deduplicate by coarse used fingerprint to keep the beam useful.
        unique: Dict[Tuple[int, ...], State] = {}
        for st in next_states:
            fp = tuple(sorted(st.used))[-80:]
            prev = unique.get(fp)
            if prev is None or st.score > prev.score:
                st.fingerprint = fp
                unique[fp] = st
        states = sorted(unique.values(), key=lambda s: (len(s.used), s.score), reverse=True)[:beam_width]
        if states and len(states[0].used) / total >= target_coverage:
            break
    return sorted(states, key=lambda s: (len(s.used), s.score), reverse=True)


def write_outputs(out_dir: str, cells: Dict[int, Cell], width: int, height: int, states: List[State]) -> None:
    os.makedirs(out_dir, exist_ok=True)
    summary_path = os.path.join(out_dir, "slot_plan_summary.csv")
    chains_path = os.path.join(out_dir, "slot_plan_chains.csv")
    with open(summary_path, "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(
            f,
            fieldnames=[
                "planId",
                "coverage",
                "newChains",
                "newCells",
                "outerHeads",
                "anchoredHeads",
                "pressure",
                "emptyComponents",
                "smallEmptyComponents",
                "largestEmptyComponent",
                "score",
            ],
        )
        w.writeheader()
        for plan_id, st in enumerate(states[:10], start=1):
            comps, small, largest = empty_components(cells, st.used, width, height)
            fixed = sum(1 for c in cells.values() if c.occupied)
            w.writerow(
                {
                    "planId": plan_id,
                    "coverage": round(len(st.used) / (width * height), 4),
                    "newChains": len(st.candidates),
                    "newCells": len(st.used) - fixed,
                    "outerHeads": st.outer_heads,
                    "anchoredHeads": st.anchored_heads,
                    "pressure": st.pressure,
                    "emptyComponents": comps,
                    "smallEmptyComponents": small,
                    "largestEmptyComponent": largest,
                    "score": round(st.score, 2),
                }
            )
    with open(chains_path, "w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(
            f,
            fieldnames=["planId", "chainIndex", "headIndex", "headDir", "length", "outerHead", "anchoredHead", "pressure", "label", "path", "xyPath"],
        )
        w.writeheader()
        for plan_id, st in enumerate(states[:10], start=1):
            for ci, cand in enumerate(st.candidates, start=1):
                w.writerow(
                    {
                        "planId": plan_id,
                        "chainIndex": ci,
                        "headIndex": cand.path[0],
                        "headDir": cand.head_dir,
                        "length": len(cand.path),
                        "outerHead": cand.outer_head,
                        "anchoredHead": cand.anchored_head,
                        "pressure": cand.pressure,
                        "label": cand.label,
                        "path": " ".join(map(str, cand.path)),
                        "xyPath": " ".join(f"{xy(i, width)[0]}:{xy(i, width)[1]}" for i in cand.path),
                    }
                )

    for plan_id, st in enumerate(states[:3], start=1):
        grid = [["." for _ in range(width)] for _ in range(height)]
        for i, c in cells.items():
            if c.occupied:
                grid[c.y][c.x] = "O"
            elif c.primary_role == "CriticalTimingZone":
                grid[c.y][c.x] = "X"
        for cand in st.candidates:
            hx, hy = xy(cand.path[0], width)
            grid[hy][hx] = "H" if cand.anchored_head else ("E" if cand.outer_head else "h")
            for bi in cand.path[1:]:
                bx, by = xy(bi, width)
                if grid[by][bx] in {".", "X"}:
                    grid[by][bx] = "b"
        with open(os.path.join(out_dir, f"slot_plan_{plan_id:02d}.txt"), "w", encoding="utf-8") as f:
            for y in range(height - 1, -1, -1):
                f.write("".join(grid[y]) + "\n")


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--rolemap", required=True)
    parser.add_argument("--out", required=True)
    parser.add_argument("--target-coverage", type=float, default=0.86)
    parser.add_argument("--outer-budget", type=int, default=3)
    parser.add_argument("--beam-width", type=int, default=28)
    parser.add_argument("--max-steps", type=int, default=42)
    parser.add_argument("--min-len", type=int, default=3)
    parser.add_argument("--max-len", type=int, default=7)
    parser.add_argument("--random-walks-per-head", type=int, default=3)
    parser.add_argument("--candidate-limit", type=int, default=2200)
    parser.add_argument("--seed", type=int, default=1337)
    parser.add_argument("--allow-free-exit", action="store_true")
    parser.add_argument("--allow-safe-heads", action="store_true")
    parser.add_argument("--allow-critical-body", action="store_true")
    parser.add_argument("--allow-all-body", action="store_true")
    parser.add_argument("--allow-any-head", action="store_true")
    args = parser.parse_args()

    cells, width, height = read_cells(args.rolemap)
    global global_width_for_scoring
    global_width_for_scoring = width
    global ALLOW_CRITICAL_BODY, ALLOW_ALL_BODY, ALLOW_ANY_HEAD
    ALLOW_CRITICAL_BODY = args.allow_critical_body
    ALLOW_ALL_BODY = args.allow_all_body
    ALLOW_ANY_HEAD = args.allow_any_head
    rng = random.Random(args.seed)

    candidates_by_head: Dict[Tuple[int, str], List[Candidate]] = {}
    seen_paths: Set[Tuple[int, ...]] = set()
    for c in cells.values():
        if not role_allowed_head(c, args.allow_free_exit, args.allow_safe_heads):
            continue
        for d in sorted(head_dirs_for(c, args.allow_free_exit, args.allow_safe_heads)):
            for path in generate_walks(c, d, cells, width, height, args.min_len, args.max_len, rng, args.random_walks_per_head):
                if path in seen_paths:
                    continue
                seen_paths.add(path)
                cand = score_candidate(path, d, cells)
                candidates_by_head.setdefault((c.index, d), []).append(cand)

    candidates: List[Candidate] = []
    for key, group in candidates_by_head.items():
        candidates.extend(sorted(group, key=lambda c: c.score, reverse=True)[:3])
    candidates.sort(key=lambda c: c.score, reverse=True)
    candidates = candidates[: args.candidate_limit]

    states = plan(cells, width, height, candidates, args.beam_width, args.max_steps, args.target_coverage, args.outer_budget)
    write_outputs(args.out, cells, width, height, states)

    best = states[0] if states else State(used=set())
    fixed = sum(1 for c in cells.values() if c.occupied)
    comps, small, largest = empty_components(cells, best.used, width, height)
    print(f"rolemap={args.rolemap}")
    print(f"grid={width}x{height} fixedOccupied={fixed} candidates={len(candidates)}")
    print(
        "best "
        f"coverage={len(best.used)/(width*height):.4f} "
        f"newChains={len(best.candidates)} newCells={len(best.used)-fixed} "
        f"outerHeads={best.outer_heads} anchoredHeads={best.anchored_heads} "
        f"emptyComponents={comps} smallEmptyComponents={small} largestEmpty={largest}"
    )
    print(f"out={args.out}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
