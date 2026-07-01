#!/usr/bin/env python3
import argparse
import csv
import math
import os
import random
import re
import struct
from dataclasses import dataclass


DIRS = {
    "U": (0, 1),
    "R": (1, 0),
    "D": (0, -1),
    "L": (-1, 0),
}

COLORS = [
    "#ff3b4f",
    "#1ea7ff",
    "#ffe63b",
    "#35c957",
    "#a25bbd",
    "#00c8d9",
    "#ff9d00",
    "#ff2d7a",
]


@dataclass
class Chain:
    cells: tuple
    color: int = 0

    @property
    def head(self):
        return self.cells[0]

    @property
    def out_dir(self):
        hx, hy = self.cells[0]
        nx, ny = self.cells[1]
        dx = hx - nx
        dy = hy - ny
        for key, val in DIRS.items():
            if val == (dx, dy):
                return key
        return "U"


def in_bounds(w, h, cell):
    x, y = cell
    return 0 <= x < w and 0 <= y < h


def add(a, b):
    return (a[0] + b[0], a[1] + b[1])


def build_occupancy(chains, active=None):
    occ = {}
    if active is None:
        active = [True] * len(chains)
    for i, chain in enumerate(chains):
        if not active[i]:
            continue
        for cell in chain.cells:
            if cell in occ:
                return None
            occ[cell] = i
    return occ


def ray_cells(w, h, chain):
    dx, dy = DIRS[chain.out_dir]
    x, y = chain.head
    result = []
    x += dx
    y += dy
    while 0 <= x < w and 0 <= y < h:
        result.append((x, y))
        x += dx
        y += dy
    return result


def can_exit(w, h, chains, index, active=None):
    if active is not None and not active[index]:
        return False
    occ = build_occupancy(chains, active)
    if occ is None:
        return False
    for cell in ray_cells(w, h, chains[index]):
        owner = occ.get(cell)
        if owner is not None and (active is None or active[owner]):
            return False
    return True


def available(w, h, chains, active=None):
    if active is None:
        active = [True] * len(chains)
    return [i for i in range(len(chains)) if active[i] and can_exit(w, h, chains, i, active)]


def planned_solve_report(w, h, chains, forward_order):
    active = [True] * len(chains)
    curve = []
    for chain_id in forward_order:
        open_now = available(w, h, chains, active)
        curve.append(len(open_now))
        if chain_id not in open_now:
            return {
                "solved": False,
                "removed": len([x for x in active if not x]),
                "initial": curve[0] if curve else 0,
                "avg": sum(curve) / len(curve) if curve else 0,
                "max": max(curve) if curve else 0,
                "curve": curve,
            }
        active[chain_id] = False
    return {
        "solved": True,
        "removed": len(chains),
        "initial": curve[0] if curve else 0,
        "avg": sum(curve) / len(curve) if curve else 0,
        "max": max(curve) if curve else 0,
        "curve": curve,
    }


def greedy_report(w, h, chains):
    active = [True] * len(chains)
    curve = []
    order = []
    for _ in range(len(chains)):
        open_now = available(w, h, chains, active)
        curve.append(len(open_now))
        if not open_now:
            break
        picked = max(
            open_now,
            key=lambda i: (
                1000 if is_boundary(w, h, chains[i].head) else 0,
                len(chains[i].cells),
                -i,
            ),
        )
        active[picked] = False
        order.append(picked)
    return {
        "solved": len(order) == len(chains),
        "removed": len(order),
        "initial": curve[0] if curve else 0,
        "avg": sum(curve[: len(order)]) / len(order) if order else 0,
        "max": max(curve) if curve else 0,
        "curve": curve,
        "order": order,
    }


def is_boundary(w, h, cell):
    x, y = cell
    return x == 0 or y == 0 or x == w - 1 or y == h - 1


def direction_from_delta(dx, dy):
    for key, val in DIRS.items():
        if val == (dx, dy):
            return key
    return None


def random_tail(w, h, occ, rng, start, forbidden, min_extra, max_extra, target_cells, allowed_cells=None):
    cells = []
    used = set(forbidden)
    cur = start
    extra = rng.randint(min_extra, max_extra)
    for _ in range(extra):
        step_dirs = list(DIRS.values())
        rng.shuffle(step_dirs)
        best = None
        best_score = -999
        for sx, sy in step_dirs:
            nxt = (cur[0] + sx, cur[1] + sy)
            if not in_bounds(w, h, nxt) or nxt in occ or nxt in used:
                continue
            if allowed_cells is not None and nxt not in allowed_cells:
                continue
            score = 0
            if nxt in target_cells:
                score += 5
            score += rng.random()
            if score > best_score:
                best_score = score
                best = nxt
        if best is None:
            break
        cells.append(best)
        used.add(best)
        cur = best
    return cells


def make_targeted_candidate(w, h, chains, rng, min_len, max_len, target_cells, allowed_cells=None):
    occ = build_occupancy(chains) or {}
    if not target_cells:
        return None

    targets = list(target_cells)
    rng.shuffle(targets)
    for target in targets[:16]:
        dirs = list(DIRS.items())
        rng.shuffle(dirs)
        for _, (dx, dy) in dirs:
            head = (target[0] + dx, target[1] + dy)
            if not in_bounds(w, h, head) or head in occ or target in occ:
                continue
            if allowed_cells is not None and (head not in allowed_cells or target not in allowed_cells):
                continue
            probe = Chain((head, target))
            temp = chains + [probe]
            if not can_exit(w, h, temp, len(temp) - 1):
                continue
            max_extra = max(0, max_len - 2)
            min_extra = max(0, min_len - 2)
            tail = random_tail(w, h, occ, rng, target, {head, target}, min_extra, max_extra, target_cells, allowed_cells)
            cells = [head, target] + tail
            if len(cells) >= 2:
                return Chain(tuple(cells), rng.randrange(len(COLORS)))
    return None


def make_candidate(w, h, chains, rng, min_len, max_len, target_cells, allowed_cells=None):
    occ = build_occupancy(chains) or {}
    if allowed_cells is None:
        empties = [(x, y) for y in range(h) for x in range(w) if (x, y) not in occ]
    else:
        empties = [cell for cell in allowed_cells if cell not in occ]
    if len(empties) < 2:
        return None

    head = rng.choice(empties)
    dirs = list(DIRS.items())
    rng.shuffle(dirs)
    for _, (dx, dy) in dirs:
        second = (head[0] - dx, head[1] - dy)
        if not in_bounds(w, h, second) or second in occ:
            continue
        if allowed_cells is not None and second not in allowed_cells:
            continue
        probe = Chain((head, second))
        temp = chains + [probe]
        if not can_exit(w, h, temp, len(temp) - 1):
            continue

        cells = [head, second]
        length = rng.randint(min_len, max_len)
        cells += random_tail(w, h, occ, rng, second, set(cells), max(0, length - 2), max(0, length - 2), target_cells, allowed_cells)

        if len(cells) >= 2:
            return Chain(tuple(cells), rng.randrange(len(COLORS)))
    return None


def score_candidate(w, h, chains, cand, before_openers, block_weight, choice_weight):
    merged = chains + [cand]
    if build_occupancy(merged) is None:
        return None
    if not can_exit(w, h, merged, len(merged) - 1):
        return None

    after_open = set(available(w, h, merged))
    blocked_old = [i for i in before_openers if i not in after_open]
    cand_cells = set(cand.cells)
    blocked_rays = 0
    for i in range(len(chains)):
        if cand_cells.intersection(ray_cells(w, h, chains[i])):
            blocked_rays += 1

    direct_boundary = 1 if is_boundary(w, h, cand.head) else 0
    head_ray_len = len(ray_cells(w, h, cand))
    score = 0
    score += block_weight * len(blocked_old)
    score += 4 * blocked_rays
    score += len(cand.cells)
    score -= choice_weight * len(after_open)
    score -= 3 * direct_boundary
    score -= max(0, head_ray_len - 5) * 0.2
    return score, len(blocked_old), len(after_open), blocked_rays


def generate_one(seed, w, h, target_chains, min_len, max_len, attempts_per_step, block_weight, choice_weight, nonblock_penalty):
    rng = random.Random(seed)
    chains = []
    add_order = []
    rows = []

    for step in range(target_chains):
        before_open = set(available(w, h, chains)) if chains else set()
        target_cells = set()
        for i in before_open:
            target_cells.update(ray_cells(w, h, chains[i]))

        best = None
        for _ in range(attempts_per_step):
            cand = None
            if target_cells and rng.random() < 0.75:
                cand = make_targeted_candidate(w, h, chains, rng, min_len, max_len, target_cells)
            if cand is None:
                cand = make_candidate(w, h, chains, rng, min_len, max_len, target_cells)
            if cand is None:
                continue
            scored = score_candidate(w, h, chains, cand, before_open, block_weight, choice_weight)
            if scored is None:
                continue
            # After the seed phase, prefer actual blocker candidates when possible.
            score, blocked, after_choices, blocked_rays = scored
            if step >= 4 and blocked == 0:
                score -= nonblock_penalty
            score += rng.random() * 0.01
            if best is None or score > best[0]:
                best = (score, cand, blocked, after_choices, blocked_rays)

        if best is None:
            break
        _, cand, blocked, after_choices, blocked_rays = best
        chains.append(cand)
        add_order.append(len(chains) - 1)
        rows.append(
            {
                "reverseStep": step,
                "chainId": len(chains) - 1,
                "length": len(cand.cells),
                "head": f"{cand.head[0]}:{cand.head[1]}",
                "dir": cand.out_dir,
                "blockedOpeners": blocked,
                "afterChoices": after_choices,
                "blockedRays": blocked_rays,
            }
        )

    forward_order = list(reversed(add_order))
    planned = planned_solve_report(w, h, chains, forward_order)
    greedy = greedy_report(w, h, chains)
    occupied = sum(len(c.cells) for c in chains)
    coverage = occupied / float(w * h)
    return {
        "seed": seed,
        "chains": chains,
        "rows": rows,
        "forward_order": forward_order,
        "planned": planned,
        "greedy": greedy,
        "coverage": coverage,
        "occupied": occupied,
    }


def parse_hex_indices(hex_text):
    raw = bytes.fromhex(hex_text.strip())
    return tuple(struct.unpack("<I", raw[i : i + 4])[0] for i in range(0, len(raw), 4))


def load_unity_level_asset(path):
    text = open(path, "r", encoding="utf-8-sig").read()
    width_match = re.search(r"authoredLevel:\s*\n\s+width:\s*(\d+)\s*\n\s+height:\s*(\d+)", text)
    if not width_match:
        width_match = re.search(r"board:\s*\n\s+width:\s*(\d+)\s*\n\s+height:\s*(\d+)", text)
    if not width_match:
        raise ValueError(f"Cannot find level dimensions in {path}")
    w = int(width_match.group(1))
    h = int(width_match.group(2))
    pattern = re.compile(r"- indices:\s*([0-9a-fA-F]+)\s*\n\s+colorIndex:\s*(\d+)")
    chains = []
    for match in pattern.finditer(text):
        indices = parse_hex_indices(match.group(1))
        color = int(match.group(2))
        cells = tuple((idx % w, idx // w) for idx in indices)
        if len(cells) >= 2:
            chains.append(Chain(cells, color))
    if not chains:
        raise ValueError(f"Cannot find authored arrows in {path}")
    return w, h, chains


def encode_unity_int_hex_list(values):
    return b"".join(struct.pack("<i", int(v)) for v in values).hex()


def write_unity_level_asset(template_path, output_path, level_id, w, h, chains, seed):
    with open(template_path, "r", encoding="utf-8-sig") as f:
        lines = f.read().splitlines()
    cell_count = sum(len(c.cells) for c in chains)
    coverage = round(cell_count / float(w * h), 7)
    for i, line in enumerate(lines):
        if re.match(r"^\s*m_Name:\s*", line):
            lines[i] = f"  m_Name: {level_id}"
        elif re.match(r"^\s*levelId:\s*", line):
            lines[i] = f"  levelId: {level_id}"
        elif re.match(r"^\s{4}arrowCoverage:\s*", line):
            lines[i] = f"    arrowCoverage: {coverage}"
        elif re.match(r"^\s{4}initialMovableArrowCount:\s*", line):
            lines[i] = "    initialMovableArrowCount: 4"
        elif re.match(r"^\s{4}randomSeed:\s*", line):
            lines[i] = f"    randomSeed: {seed}"

    arrows_line = -1
    block_line = -1
    for i, line in enumerate(lines):
        if re.match(r"^\s{4}arrows:\s*$", line):
            arrows_line = i
            continue
        if arrows_line >= 0 and re.match(r"^\s{4}blockIndices:", line):
            block_line = i
            break
    if arrows_line < 0 or block_line < 0:
        raise ValueError(f"Cannot rewrite arrows block for template {template_path}")

    out = lines[: arrows_line + 1]
    for i, chain in enumerate(chains):
        indices = [y * w + x for x, y in chain.cells]
        out.append(f"    - indices: {encode_unity_int_hex_list(indices)}")
        out.append(f"      colorIndex: {chain.color % len(COLORS)}")
    out.extend(lines[block_line:])

    os.makedirs(os.path.dirname(output_path), exist_ok=True)
    with open(output_path, "w", encoding="utf-8") as f:
        f.write("\n".join(out) + "\n")
    with open(output_path + ".meta", "w", encoding="utf-8") as f:
        f.write(
            "\n".join(
                [
                    "fileFormatVersion: 2",
                    f"guid: {''.join(random.choice('0123456789abcdef') for _ in range(32))}",
                    "NativeFormatImporter:",
                    "  externalObjects: {}",
                    "  mainObjectFileID: 11400000",
                    "  userData: ",
                    "  assetBundleName: ",
                    "  assetBundleVariant: ",
                ]
            )
            + "\n"
        )


def write_candidate_csv(path, level_id, asset_path, result, w, h, source_level_id=""):
    occupied = sum(len(c.cells) for c in result["chains"])
    coverage = occupied / float(w * h)
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", newline="", encoding="utf-8") as f:
        writer = csv.DictWriter(
            f,
            fieldnames=[
                "selected",
                "levelId",
                "path",
                "sourceLevelId",
                "coverage",
                "canvasWidth",
                "canvasHeight",
                "chains",
                "baseChains",
                "addedChains",
                "plannedSolved",
                "plannedInitial",
                "plannedAvg",
                "plannedMax",
                "greedySolved",
                "greedyInitial",
                "greedyAvg",
                "greedyMax",
                "addedMeta",
            ],
        )
        writer.writeheader()
        writer.writerow(
            {
                "selected": 1,
                "levelId": level_id,
                "path": asset_path,
                "sourceLevelId": source_level_id,
                "coverage": f"{coverage:.7f}",
                "canvasWidth": w,
                "canvasHeight": h,
                "chains": len(result["chains"]),
                "baseChains": result.get("baseCount", ""),
                "addedChains": len(result["chains"]) - result.get("baseCount", len(result["chains"])),
                "plannedSolved": result["planned"]["solved"],
                "plannedInitial": result["planned"]["initial"],
                "plannedAvg": f"{result['planned']['avg']:.3f}",
                "plannedMax": result["planned"]["max"],
                "greedySolved": result["greedy"]["solved"],
                "greedyInitial": result["greedy"]["initial"],
                "greedyAvg": f"{result['greedy']['avg']:.3f}",
                "greedyMax": result["greedy"]["max"],
                "addedMeta": "reverseSlotRefillV1",
            }
        )


def extend_from_base(seed, w, h, base_chains, add_chains, min_len, max_len, attempts_per_step, block_weight, choice_weight, nonblock_penalty, slot_cells=None, strict_slots=False):
    rng = random.Random(seed)
    chains = list(base_chains)
    base_count = len(chains)
    add_order = []
    rows = []

    for step in range(add_chains):
        before_open = set(available(w, h, chains))
        target_cells = set()
        for i in before_open:
            target_cells.update(ray_cells(w, h, chains[i]))
        if slot_cells:
            target_cells.update(slot_cells)
        allowed_cells = set(slot_cells) if (strict_slots and slot_cells) else None

        best = None
        for _ in range(attempts_per_step):
            cand = None
            if target_cells and rng.random() < 0.85:
                cand = make_targeted_candidate(w, h, chains, rng, min_len, max_len, target_cells, allowed_cells)
            if cand is None:
                cand = make_candidate(w, h, chains, rng, min_len, max_len, target_cells, allowed_cells)
            if cand is None:
                continue
            scored = score_candidate(w, h, chains, cand, before_open, block_weight, choice_weight)
            if scored is None:
                continue
            score, blocked, after_choices, blocked_rays = scored
            if blocked == 0:
                score -= nonblock_penalty
            # Prefer additions that do not increase the opener count over the parent state.
            if after_choices > len(before_open):
                score -= (after_choices - len(before_open)) * 30
            score += rng.random() * 0.01
            if best is None or score > best[0]:
                best = (score, cand, blocked, after_choices, blocked_rays)
        if best is None:
            break
        _, cand, blocked, after_choices, blocked_rays = best
        chains.append(cand)
        add_order.append(len(chains) - 1)
        rows.append(
            {
                "reverseStep": step,
                "chainId": len(chains) - 1,
                "length": len(cand.cells),
                "head": f"{cand.head[0]}:{cand.head[1]}",
                "dir": cand.out_dir,
                "blockedOpeners": blocked,
                "afterChoices": after_choices,
                "blockedRays": blocked_rays,
            }
        )

    prefix = list(reversed(add_order))
    prefix_report = planned_prefix_then_greedy_report(w, h, chains, prefix)
    greedy = greedy_report(w, h, chains)
    base_greedy = greedy_report(w, h, base_chains)
    occupied = sum(len(c.cells) for c in chains)
    return {
        "seed": seed,
        "chains": chains,
        "rows": rows,
        "forward_order": prefix,
        "planned": prefix_report,
        "greedy": greedy,
        "baseGreedy": base_greedy,
        "coverage": occupied / float(w * h),
        "occupied": occupied,
        "baseCount": base_count,
    }


def planned_prefix_then_greedy_report(w, h, chains, prefix_order):
    active = [True] * len(chains)
    curve = []
    removed = []
    for chain_id in prefix_order:
        open_now = available(w, h, chains, active)
        curve.append(len(open_now))
        if chain_id not in open_now:
            return {
                "solved": False,
                "removed": len(removed),
                "initial": curve[0] if curve else 0,
                "avg": sum(curve) / len(curve) if curve else 0,
                "max": max(curve) if curve else 0,
                "curve": curve,
            }
        active[chain_id] = False
        removed.append(chain_id)
    # Finish with greedy over the base/remainder.
    while len(removed) < len(chains):
        open_now = available(w, h, chains, active)
        curve.append(len(open_now))
        if not open_now:
            break
        picked = max(
            open_now,
            key=lambda i: (
                1000 if is_boundary(w, h, chains[i].head) else 0,
                len(chains[i].cells),
                -i,
            ),
        )
        active[picked] = False
        removed.append(picked)
    return {
        "solved": len(removed) == len(chains),
        "removed": len(removed),
        "initial": curve[0] if curve else 0,
        "avg": sum(curve) / len(curve) if curve else 0,
        "max": max(curve) if curve else 0,
        "curve": curve,
    }


def write_chain_csv(path, result, w, h):
    with open(path, "w", newline="", encoding="utf-8") as f:
        writer = csv.writer(f)
        writer.writerow(["chainId", "orderForward", "color", "headX", "headY", "dir", "length", "cells"])
        order_index = {chain_id: i for i, chain_id in enumerate(result["forward_order"])}
        for i, chain in enumerate(result["chains"]):
            cells = ";".join(f"{x}:{y}" for x, y in chain.cells)
            role = "base" if i < result.get("baseCount", 0) else "added"
            writer.writerow([i, order_index.get(i, ""), chain.color, chain.head[0], chain.head[1], chain.out_dir, len(chain.cells), cells])


def write_step_csv(path, result):
    fields = ["reverseStep", "chainId", "length", "head", "dir", "blockedOpeners", "afterChoices", "blockedRays"]
    with open(path, "w", newline="", encoding="utf-8") as f:
        writer = csv.DictWriter(f, fieldnames=fields)
        writer.writeheader()
        for row in result["rows"]:
            writer.writerow(row)


def write_summary_csv(path, results):
    fields = [
        "seed",
        "chains",
        "occupied",
        "coverage",
        "plannedSolved",
        "plannedInitial",
        "plannedAvg",
        "plannedMax",
        "greedySolved",
        "greedyInitial",
        "greedyAvg",
        "greedyMax",
        "baseChains",
        "baseGreedyInitial",
        "baseGreedyAvg",
        "baseGreedyMax",
    ]
    with open(path, "w", newline="", encoding="utf-8") as f:
        writer = csv.DictWriter(f, fieldnames=fields)
        writer.writeheader()
        for result in results:
            writer.writerow(
                {
                    "seed": result["seed"],
                    "chains": len(result["chains"]),
                    "occupied": result["occupied"],
                    "coverage": f"{result['coverage']:.4f}",
                    "plannedSolved": result["planned"]["solved"],
                    "plannedInitial": result["planned"]["initial"],
                    "plannedAvg": f"{result['planned']['avg']:.3f}",
                    "plannedMax": result["planned"]["max"],
                    "greedySolved": result["greedy"]["solved"],
                    "greedyInitial": result["greedy"]["initial"],
                    "greedyAvg": f"{result['greedy']['avg']:.3f}",
                    "greedyMax": result["greedy"]["max"],
                    "baseChains": result.get("baseCount", ""),
                    "baseGreedyInitial": result.get("baseGreedy", {}).get("initial", ""),
                    "baseGreedyAvg": f"{result.get('baseGreedy', {}).get('avg', 0):.3f}" if "baseGreedy" in result else "",
                    "baseGreedyMax": result.get("baseGreedy", {}).get("max", ""),
                }
            )


def write_svg(path, result, w, h, cell_size=26):
    pad = 16
    width = w * cell_size + pad * 2
    height = h * cell_size + pad * 2
    lines = [
        f'<svg xmlns="http://www.w3.org/2000/svg" width="{width}" height="{height}" viewBox="0 0 {width} {height}">',
        '<rect width="100%" height="100%" fill="#171d2d"/>',
    ]
    for x in range(w + 1):
        px = pad + x * cell_size
        lines.append(f'<line x1="{px}" y1="{pad}" x2="{px}" y2="{pad + h * cell_size}" stroke="#242b3d" stroke-width="1"/>')
    for y in range(h + 1):
        py = pad + y * cell_size
        lines.append(f'<line x1="{pad}" y1="{py}" x2="{pad + w * cell_size}" y2="{py}" stroke="#242b3d" stroke-width="1"/>')

    for i, chain in enumerate(result["chains"]):
        color = COLORS[chain.color % len(COLORS)]
        points = []
        for x, y in chain.cells:
            sx = pad + (x + 0.5) * cell_size
            sy = pad + (h - y - 0.5) * cell_size
            points.append((sx, sy))
        point_text = " ".join(f"{x:.1f},{y:.1f}" for x, y in points)
        lines.append(f'<polyline points="{point_text}" fill="none" stroke="{color}" stroke-width="5" stroke-linecap="round" stroke-linejoin="round"/>')
        hx, hy = points[0]
        dx, dy = DIRS[chain.out_dir]
        # SVG y axis is inverted.
        ax = hx + dx * 8
        ay = hy - dy * 8
        left = math.atan2(hy - ay, ax - hx) + math.pi * 0.78
        right = math.atan2(hy - ay, ax - hx) - math.pi * 0.78
        p1 = (ax, ay)
        p2 = (ax - math.cos(left) * 10, ay + math.sin(left) * 10)
        p3 = (ax - math.cos(right) * 10, ay + math.sin(right) * 10)
        lines.append(
            f'<polygon points="{p1[0]:.1f},{p1[1]:.1f} {p2[0]:.1f},{p2[1]:.1f} {p3[0]:.1f},{p3[1]:.1f}" fill="{color}"/>'
        )
    lines.append("</svg>")
    with open(path, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--out", default=".codex-run/reverse_unlock_skeleton_probe_v1")
    parser.add_argument("--width", type=int, default=14)
    parser.add_argument("--height", type=int, default=18)
    parser.add_argument("--base-asset", default="")
    parser.add_argument("--slot-asset", default="")
    parser.add_argument("--strict-slots", action="store_true")
    parser.add_argument("--add-chains", type=int, default=0)
    parser.add_argument("--runs", type=int, default=24)
    parser.add_argument("--seed", type=int, default=91001)
    parser.add_argument("--chains", type=int, default=42)
    parser.add_argument("--min-len", type=int, default=2)
    parser.add_argument("--max-len", type=int, default=5)
    parser.add_argument("--attempts", type=int, default=700)
    parser.add_argument("--block-weight", type=float, default=42)
    parser.add_argument("--choice-weight", type=float, default=5)
    parser.add_argument("--nonblock-penalty", type=float, default=35)
    parser.add_argument("--unity-level-dir", default="")
    parser.add_argument("--unity-level-id", default="")
    parser.add_argument("--candidate-csv", default="")
    args = parser.parse_args()

    os.makedirs(args.out, exist_ok=True)
    results = []
    if args.base_asset:
        w, h, base_chains = load_unity_level_asset(args.base_asset)
        slot_cells = set()
        if args.slot_asset:
            sw, sh, slot_chains = load_unity_level_asset(args.slot_asset)
            if sw != w or sh != h:
                raise ValueError(f"Slot/base canvas mismatch: {args.slot_asset}")
            base_cells = {cell for chain in base_chains for cell in chain.cells}
            full_cells = {cell for chain in slot_chains for cell in chain.cells}
            slot_cells = full_cells.difference(base_cells)
        add_count = args.add_chains if args.add_chains > 0 else args.chains
        for i in range(args.runs):
            results.append(
                extend_from_base(
                    args.seed + i * 1009,
                    w,
                    h,
                    base_chains,
                    add_count,
                    args.min_len,
                    args.max_len,
                    args.attempts,
                    args.block_weight,
                    args.choice_weight,
                    args.nonblock_penalty,
                    slot_cells=slot_cells,
                    strict_slots=args.strict_slots,
                )
            )
        args.width = w
        args.height = h
    else:
        for i in range(args.runs):
            results.append(
                generate_one(
                    args.seed + i * 1009,
                    args.width,
                    args.height,
                    args.chains,
                    args.min_len,
                    args.max_len,
                    args.attempts,
                    args.block_weight,
                    args.choice_weight,
                    args.nonblock_penalty,
                )
            )

    def rank_key(result):
        planned = result["planned"]
        greedy = result["greedy"]
        solved_bonus = 10000 if planned["solved"] else 0
        greedy_bonus = 1000 if greedy["solved"] else 0
        pressure = -abs(planned["initial"] - 3) * 10 - planned["avg"] * 2 - planned["max"]
        return (solved_bonus + greedy_bonus + result["coverage"] * 100 + pressure, result["coverage"])

    results.sort(key=rank_key, reverse=True)
    best = results[0]

    write_summary_csv(os.path.join(args.out, "summary.csv"), results)
    write_chain_csv(os.path.join(args.out, "best_chains.csv"), best, args.width, args.height)
    write_step_csv(os.path.join(args.out, "best_reverse_steps.csv"), best)
    write_svg(os.path.join(args.out, "best.svg"), best, args.width, args.height)
    if args.unity_level_dir:
        level_id = args.unity_level_id or f"reverse_slot_refill_v1_{best['seed']}"
        asset_path = os.path.abspath(os.path.join(args.unity_level_dir, f"{level_id}.asset"))
        template = args.base_asset if args.base_asset else ""
        if not template:
            raise ValueError("--unity-level-dir requires --base-asset as a template")
        write_unity_level_asset(template, asset_path, level_id, args.width, args.height, best["chains"], best["seed"])
        if args.candidate_csv:
            write_candidate_csv(
                os.path.abspath(args.candidate_csv),
                level_id,
                asset_path,
                best,
                args.width,
                args.height,
                source_level_id=os.path.splitext(os.path.basename(args.base_asset))[0],
            )

    print(f"out={os.path.abspath(args.out)}")
    print(f"bestSeed={best['seed']}")
    print(f"chains={len(best['chains'])} occupied={best['occupied']} coverage={best['coverage']:.4f}")
    if "baseCount" in best:
        print(
            f"base chains={best['baseCount']} "
            f"greedyInitial={best['baseGreedy']['initial']} "
            f"greedyAvg={best['baseGreedy']['avg']:.3f} "
            f"greedyMax={best['baseGreedy']['max']}"
        )
    print(
        "planned "
        f"solved={best['planned']['solved']} initial={best['planned']['initial']} "
        f"avg={best['planned']['avg']:.3f} max={best['planned']['max']}"
    )
    print(
        "greedy "
        f"solved={best['greedy']['solved']} initial={best['greedy']['initial']} "
        f"avg={best['greedy']['avg']:.3f} max={best['greedy']['max']} "
        f"removed={best['greedy']['removed']}"
    )


if __name__ == "__main__":
    main()
