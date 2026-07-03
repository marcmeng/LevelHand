#!/usr/bin/env python3
"""Build a first-50 Campaign500 layout from existing resources.

Inputs:
- locked V2 slot spec for target duty/load/evidence
- existing RhythmV4 Final StrictComplete resource index as the candidate pool

This is a layout rehearsal. It does not generate new levels.
"""

from __future__ import annotations

import csv
import math
from collections import Counter
from dataclasses import dataclass
from pathlib import Path
from statistics import mean
from typing import Any


ROOT = Path(__file__).resolve().parents[2]
OUT_DIR = ROOT / "Exports" / "Campaign500_First50_V2Existing_20260702"

SLOT_SPEC = ROOT / "Exports" / "Campaign500_DesignPlanning_20260702" / "campaign500_slot_spec_v2_locked.csv"
RESOURCE_INDEX = ROOT / "Exports" / "C5V4FSC" / "Docs" / "campaign500_rhythm_v4_final_strict_complete_per_level_config_index.csv"

LAYOUT_CSV = OUT_DIR / "campaign500_first50_v2_existing_layout_v1.csv"
MATCH_REPORT_CSV = OUT_DIR / "campaign500_first50_v2_existing_match_report_v1.csv"
RESOURCE_CHECK_CSV = OUT_DIR / "campaign500_first50_v2_existing_resource_check_v1.csv"
SUMMARY_MD = OUT_DIR / "campaign500_first50_v2_existing_summary_v1.md"


def read_csv(path: Path) -> list[dict[str, str]]:
    with path.open("r", encoding="utf-8-sig", newline="") as f:
        return list(csv.DictReader(f))


def write_csv(path: Path, rows: list[dict[str, Any]], fields: list[str]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding="utf-8", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=fields, extrasaction="ignore")
        writer.writeheader()
        for row in rows:
            writer.writerow(row)


def write_text(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8")


def parse_int(value: Any, default: int = 0) -> int:
    s = str(value or "").strip()
    if not s:
        return default
    try:
        return int(float(s))
    except ValueError:
        return default


def parse_float(value: Any, default: float | None = 0.0) -> float | None:
    s = str(value or "").strip()
    if not s:
        return default
    try:
        return float(s)
    except ValueError:
        return default


def safe(row: dict[str, str], key: str, default: str = "") -> str:
    value = row.get(key)
    return default if value is None else str(value).strip()


def rel_exists(asset_path: str) -> bool:
    if not asset_path:
        return False
    return (ROOT / asset_path.replace("/", "\\")).exists()


def token_text(*parts: str) -> str:
    return " ".join(str(p or "").lower() for p in parts)


@dataclass
class Candidate:
    row: dict[str, str]
    source_order: int
    category: str
    level_id: str
    asset_path: str
    status: str
    style: str
    chain_language: str
    load: float | None
    raw_chains: float | None
    coverage: str
    max_choices: str
    local_run: str
    sts: str

    @property
    def text(self) -> str:
        return token_text(self.level_id, self.status, self.style, self.chain_language)


def candidate_load(row: dict[str, str]) -> tuple[float | None, float | None]:
    effective = parse_float(row.get("effectiveChainLoad"), None)
    raw = parse_float(row.get("chains"), None)
    if effective is not None:
        return effective, raw
    return raw, raw


def build_candidates(rows: list[dict[str, str]]) -> list[Candidate]:
    candidates: list[Candidate] = []
    for row in rows:
        asset_path = safe(row, "loadedAssetPath")
        if not asset_path or not rel_exists(asset_path):
            continue
        load, raw = candidate_load(row)
        candidates.append(
            Candidate(
                row=row,
                source_order=parse_int(row.get("order")),
                category=safe(row, "category", "normal"),
                level_id=safe(row, "finalLevelId"),
                asset_path=asset_path,
                status=safe(row, "finalStatus"),
                style=safe(row, "productionStyle"),
                chain_language=safe(row, "productionChainLanguage"),
                load=load,
                raw_chains=raw,
                coverage=safe(row, "sourceCoverage"),
                max_choices=safe(row, "maxChoices"),
                local_run=safe(row, "localPatchSolveRunMax"),
                sts=safe(row, "solveTraceQualityScore"),
            )
        )
    return candidates


def style_fit_score(slot: dict[str, str], cand: Candidate) -> tuple[float, list[str]]:
    target_style = safe(slot, "styleTargetV2").lower()
    target_lang = safe(slot, "chainLanguageTargetV2").lower()
    duty = safe(slot, "slotDutyV2")
    text = cand.text
    score = 0.0
    reasons: list[str] = []

    checks = [
        ("flow", -10),
        ("peel", -10),
        ("longchain", -12),
        ("hub", -12),
        ("mixed", -6),
        ("shape", -8),
        ("hole", -8),
        ("lock", -8),
    ]
    for word, bonus in checks:
        if word in target_style and word in text:
            score += bonus
            reasons.append(f"style:{word}")
            break
    if "mixed" in target_style and ("neutral" in text or "mixed" in text):
        score -= 5
        reasons.append("style:neutral-mixed")

    for word in ("rail", "curve", "patch", "spine", "mixed"):
        if word in target_lang and word in text:
            score -= 6
            reasons.append(f"lang:{word}")
            break

    if duty in {"ReadCheck", "LocalRunBreaker"} and any(w in text for w in ("strict", "hard", "pressure", "peel", "longchain", "lock")):
        score -= 10
        reasons.append("readcheck-suitable")
    if duty in {"DependencyPeak", "ExtremeMemory", "StylePeak"} and any(w in text for w in ("strict", "hard", "pressure", "peak", "longchain")):
        score -= 14
        reasons.append("peak-suitable")

    return score, reasons


def candidate_score(slot: dict[str, str], cand: Candidate, used_assets: set[str]) -> tuple[float, str]:
    order = parse_int(slot.get("order"))
    code = parse_int(slot.get("difficultyCode"), 1)
    category = safe(slot, "category")
    duty = safe(slot, "slotDutyV2")
    target = parse_float(slot.get("targetEffectiveLoad"), 0.0) or 0.0
    band_min = parse_float(slot.get("targetEffectiveLoadMin"), target) or target
    band_max = parse_float(slot.get("targetEffectiveLoadMax"), target) or target

    if cand.asset_path in used_assets:
        return math.inf, "already-used"
    if cand.category != category:
        return math.inf, "category-mismatch"
    if category == "normal" and cand.status == "OriginalNonTarget":
        return math.inf, "old-normal-excluded"
    if order != 1 and cand.status == "OriginalKeptRhythmV4":
        return math.inf, "tutorial-only"

    score = 0.0
    reasons: list[str] = []

    if cand.load is None:
        # Shape/hole assets often do not carry a calibrated effective load. Keep
        # exact category anchors usable, but make non-exact unknowns less likely.
        score += 12 if cand.source_order == order else 40
        reasons.append("load:unknown")
    else:
        distance = abs(cand.load - target)
        score += distance * 2.8
        if band_min <= cand.load <= band_max:
            score -= 12
            reasons.append("load:in-band")
        else:
            score += min(28, abs(cand.load - max(min(cand.load, band_max), band_min)) * 1.2)
            reasons.append("load:near-band")

    style_score, style_reasons = style_fit_score(slot, cand)
    score += style_score
    reasons.extend(style_reasons)

    if cand.source_order == order:
        score -= 6
        reasons.append("same-order-stable")
    if abs(cand.source_order - order) <= 10:
        score -= 2
        reasons.append("near-order")

    if "FinalStrictComplete" in cand.status:
        score -= 8
        reasons.append("strict-complete")
    elif "Final" in cand.status:
        score -= 5
        reasons.append("final")
    elif "ReplacedRhythmV4" in cand.status:
        score -= 3
        reasons.append("replaced")

    text = cand.text
    if code == 1 and duty not in {"ReadCheck", "LocalRunBreaker", "BridgeRamp", "CanvasStep"}:
        if any(w in text for w in ("strict", "hard", "peak", "pressure")):
            score += 20
            reasons.append("code1-too-hard-style")
    if code >= 3 and not any(w in text for w in ("strict", "hard", "peak", "pressure", "longchain")):
        score += 16
        reasons.append("peak-weak-style")
    if order <= 20 and cand.load is not None and cand.load > band_max + 12:
        score += 28
        reasons.append("front20-over-band")

    return score, ";".join(reasons)


def choose_exact_theme(slot: dict[str, str], candidates: list[Candidate], used: set[str]) -> Candidate | None:
    order = parse_int(slot.get("order"))
    category = safe(slot, "category")
    for cand in candidates:
        if cand.source_order == order and cand.category == category and cand.asset_path not in used:
            return cand
    return None


def build_layout() -> tuple[list[dict[str, Any]], list[dict[str, Any]], list[dict[str, Any]]]:
    spec_rows = [r for r in read_csv(SLOT_SPEC) if parse_int(r.get("order")) <= 50]
    candidates = build_candidates(read_csv(RESOURCE_INDEX))
    used: set[str] = set()
    layout: list[dict[str, Any]] = []
    match_report: list[dict[str, Any]] = []
    resource_check: list[dict[str, Any]] = []

    for slot in spec_rows:
        order = parse_int(slot.get("order"))
        category = safe(slot, "category")
        code = parse_int(slot.get("difficultyCode"), 1)

        selected: Candidate | None = None
        score = 0.0
        reason = ""
        match_mode = "scored-match"

        if order == 1 or category in {"shape", "hole"}:
            selected = choose_exact_theme(slot, candidates, used)
            if selected is not None:
                match_mode = "exact-theme-keep"
                score = 0.0
                reason = "preserve tutorial/category theme"

        if selected is None:
            scored: list[tuple[float, str, Candidate]] = []
            for cand in candidates:
                s, r = candidate_score(slot, cand, used)
                if math.isfinite(s):
                    scored.append((s, r, cand))
            if not scored:
                raise RuntimeError(f"No candidate for slot {order} {category}")
            scored.sort(key=lambda item: (item[0], abs(item[2].source_order - order), item[2].source_order))
            score, reason, selected = scored[0]

        used.add(selected.asset_path)

        target = parse_float(slot.get("targetEffectiveLoad"), 0.0) or 0.0
        selected_load = selected.load
        calibrated_load_note = ""
        if selected.category in {"shape", "hole"}:
            selected_load = target
            calibrated_load_note = "category-anchor-calibrated-to-v2-effective-load"
        elif selected_load is None:
            selected_load = target
            calibrated_load_note = "unknown-load-calibrated-to-v2-effective-load"
        final_chain = int(round(selected_load))
        raw_chains = "" if selected.raw_chains is None else int(round(selected.raw_chains))
        band_min = parse_float(slot.get("targetEffectiveLoadMin"), target) or target
        band_max = parse_float(slot.get("targetEffectiveLoadMax"), target) or target
        band_fit = "in_band" if band_min <= selected_load <= band_max else "out_of_band"
        if calibrated_load_note:
            band_fit = "theme_anchor_unscored_load"

        row = {
            **slot,
            "finalLevelId": selected.level_id,
            "finalAssetPath": selected.asset_path,
            "loadedAssetPath": selected.asset_path,
            "sourceOrder": selected.source_order,
            "sourceStatus": selected.status,
            "sourcePool": "C5V4FSC",
            "productionLane": safe(selected.row, "productionLane"),
            "productionStyle": selected.style,
            "chainLanguage": selected.chain_language,
            "productionChainLanguage": selected.chain_language,
            "candidateRawChains": raw_chains,
            "candidateEffectiveLoad": "" if selected.load is None else round(selected.load, 2),
            "finalChain": final_chain,
            "actualChains": raw_chains,
            "finalArrow": safe(slot, "templateArrowTiles"),
            "sourceCoverage": selected.coverage,
            "maxChoices": selected.max_choices,
            "localPatchSolveRunMax": selected.local_run,
            "solveTraceQualityScore": selected.sts,
            "matchMode": match_mode,
            "matchScore": round(score, 3),
            "matchReason": reason,
            "calibratedLoadNote": calibrated_load_note,
            "bandFit": band_fit,
            "changedFromV4Order": "false" if selected.source_order == order else "true",
            "layoutVersion": "Campaign500First50V2ExistingV1",
        }
        layout.append(row)
        match_report.append(
            {
                "order": order,
                "category": category,
                "difficultyCode": code,
                "slotDutyV2": safe(slot, "slotDutyV2"),
                "targetEffectiveLoad": safe(slot, "targetEffectiveLoad"),
                "effectiveLoadBand": safe(slot, "effectiveLoadBand"),
                "selectedLevelId": selected.level_id,
                "selectedAssetPath": selected.asset_path,
                "sourceOrder": selected.source_order,
                "sourceStatus": selected.status,
                "selectedLoad": round(selected_load, 2),
                "rawChains": raw_chains,
                "bandFit": band_fit,
                "matchMode": match_mode,
                "matchScore": round(score, 3),
                "matchReason": reason,
                "calibratedLoadNote": calibrated_load_note,
                "productionStyle": selected.style,
                "chainLanguage": selected.chain_language,
            }
        )
        resource_check.append(
            {
                "order": order,
                "levelId": selected.level_id,
                "assetPath": selected.asset_path,
                "assetExists": rel_exists(selected.asset_path),
                "metaExists": rel_exists(selected.asset_path + ".meta"),
                "category": category,
                "sourceOrder": selected.source_order,
                "status": selected.status,
            }
        )

    return layout, match_report, resource_check


FIELDS = [
    "order",
    "section10",
    "positionIn10",
    "macroArc",
    "sectionRole",
    "levelName",
    "category",
    "difficultyCode",
    "difficultyNameZh",
    "slotDutyV2",
    "targetExperience",
    "styleTargetV2",
    "chainLanguageTargetV2",
    "targetEffectiveLoadMin",
    "targetEffectiveLoad",
    "targetEffectiveLoadMax",
    "effectiveLoadBand",
    "readDemandTarget",
    "requiredEvidence",
    "avoidRules",
    "loadInterpretationNote",
    "productionNeed",
    "finalLevelId",
    "finalAssetPath",
    "loadedAssetPath",
    "sourceOrder",
    "sourceStatus",
    "sourcePool",
    "productionLane",
    "productionStyle",
    "chainLanguage",
    "productionChainLanguage",
    "candidateRawChains",
    "candidateEffectiveLoad",
    "finalChain",
    "actualChains",
    "finalArrow",
    "sourceCoverage",
    "maxChoices",
    "localPatchSolveRunMax",
    "solveTraceQualityScore",
    "matchMode",
    "matchScore",
    "matchReason",
    "calibratedLoadNote",
    "bandFit",
    "changedFromV4Order",
    "layoutVersion",
]


def write_summary(layout: list[dict[str, Any]], match_report: list[dict[str, Any]], resource_check: list[dict[str, Any]]) -> None:
    code_counts = Counter(str(r["difficultyCode"]) for r in layout)
    category_counts = Counter(str(r["category"]) for r in layout)
    duty_counts = Counter(str(r["slotDutyV2"]) for r in layout)
    fit_counts = Counter(str(r["bandFit"]) for r in layout)
    changed = sum(1 for r in layout if str(r["changedFromV4Order"]).lower() == "true")
    missing_assets = [r for r in resource_check if not r["assetExists"] or not r["metaExists"]]
    chains = [parse_float(r.get("finalChain"), 0.0) or 0.0 for r in layout]

    def counts(counter: Counter[str]) -> str:
        return "\n".join(f"- `{k}`: `{counter[k]}`" for k in sorted(counter))

    lines = [
        "# Campaign500 First50 V2 Existing Resource Layout V1",
        "",
        "- Scope: first 50 levels only.",
        "- Source: existing C5V4FSC resources; no new generation.",
        f"- Layout CSV: `{LAYOUT_CSV.relative_to(ROOT).as_posix()}`",
        f"- Match report: `{MATCH_REPORT_CSV.relative_to(ROOT).as_posix()}`",
        f"- Resource check: `{RESOURCE_CHECK_CSV.relative_to(ROOT).as_posix()}`",
        f"- Missing assets/metas: `{len(missing_assets)}`",
        f"- Rows changed from same V4 order: `{changed}`",
        f"- Effective load avg/min/max: `{round(mean(chains), 1)}/{round(min(chains), 1)}/{round(max(chains), 1)}`",
        "",
        "## Difficulty Counts",
        "",
        counts(code_counts),
        "",
        "## Category Counts",
        "",
        counts(category_counts),
        "",
        "## Slot Duty Counts",
        "",
        counts(duty_counts),
        "",
        "## Band Fit",
        "",
        counts(fit_counts),
        "",
        "## Notes",
        "",
        "- Shape slots preserve theme/perspective first and use raw chains only as an anchor; hole slots preserve spatial contrast.",
        "- Normal slots are matched by V2 duty, effective-load band, style/language hints, and existing quality status.",
        "- This layout is a rehearsal for the full 500 flow, not a final front50 decision.",
    ]
    write_text(SUMMARY_MD, "\n".join(lines) + "\n")


def main() -> None:
    layout, match_report, resource_check = build_layout()
    write_csv(LAYOUT_CSV, layout, FIELDS)
    write_csv(
        MATCH_REPORT_CSV,
        match_report,
        [
            "order",
            "category",
            "difficultyCode",
            "slotDutyV2",
            "targetEffectiveLoad",
            "effectiveLoadBand",
            "selectedLevelId",
            "selectedAssetPath",
            "sourceOrder",
            "sourceStatus",
            "selectedLoad",
            "rawChains",
            "bandFit",
            "matchMode",
            "matchScore",
            "matchReason",
            "productionStyle",
            "chainLanguage",
        ],
    )
    write_csv(RESOURCE_CHECK_CSV, resource_check, ["order", "levelId", "assetPath", "assetExists", "metaExists", "category", "sourceOrder", "status"])
    write_summary(layout, match_report, resource_check)
    print(f"Wrote {LAYOUT_CSV}")
    print(f"Wrote {SUMMARY_MD}")


if __name__ == "__main__":
    main()
