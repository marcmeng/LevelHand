#!/usr/bin/env python3
"""Build the locked Campaign500 slot specification V2.

This script creates planning artifacts only. It does not choose or replace
LevelDefinition assets. The goal is to make the campaign pacing method stable:
template skeleton -> per-slot duty/experience/evidence -> candidate matching.
"""

from __future__ import annotations

import csv
from collections import Counter, defaultdict
from dataclasses import dataclass
from pathlib import Path
from statistics import mean
from typing import Any


ROOT = Path(__file__).resolve().parents[2]
OUT_DIR = ROOT / "Exports" / "Campaign500_DesignPlanning_20260702"

TEMPLATE_CSV = ROOT / "Exports" / "Campaign500_PSG_Template_20260626_095625" / "campaign500_psg_regeneration_template.csv"
FRONT20_CSV = OUT_DIR / "campaign500_front20_handcrafted_duty_v1.csv"
ARC_CSV = OUT_DIR / "campaign500_50level_arc_v1.csv"

PRINCIPLES_MD = OUT_DIR / "campaign500_locked_planning_principles_v2.md"
SECTION_PLAN_CSV = OUT_DIR / "campaign500_10level_section_plan_v2_locked.csv"
SLOT_SPEC_CSV = OUT_DIR / "campaign500_slot_spec_v2_locked.csv"
FRONT100_CSV = OUT_DIR / "campaign500_slot_spec_v2_front100_preview.csv"
DEMAND_SUMMARY_CSV = OUT_DIR / "campaign500_slot_spec_v2_demand_summary.csv"
GAP_SUMMARY_MD = OUT_DIR / "campaign500_slot_spec_v2_gap_summary.md"


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


def parse_float(value: Any, default: float = 0.0) -> float:
    s = str(value or "").strip()
    if not s:
        return default
    try:
        return float(s)
    except ValueError:
        return default


def truthy(value: Any) -> bool:
    return str(value or "").strip().lower() in {"1", "true", "yes", "y"}


def safe(row: dict[str, str], key: str, default: str = "") -> str:
    value = row.get(key)
    if value is None:
        return default
    return str(value).strip()


def band_string(lo: int, hi: int) -> str:
    return f"{lo}-{hi}"


def arc_index(section10: int) -> int:
    return ((section10 - 1) // 5) + 1


def macro_arc_name(section10: int) -> str:
    names = {
        1: "A1_OnboardingSafety",
        2: "A2_LoopFormation",
        3: "A3_StyleExpansion",
        4: "A4_ReadDemandRise",
        5: "A5_CoreMiddle",
        6: "A6_CoreClosure",
        7: "A7_AdvancedVariation",
        8: "A8_HighRead",
        9: "A9_MatureReview",
        10: "A10_Endgame",
    }
    return names.get(arc_index(section10), "A?_Unknown")


def section_role(section10: int) -> str:
    if section10 == 1:
        return "front10_onboard_light_quiz"
    if section10 == 2:
        return "front20_onboard_first_boss"
    if section10 <= 5:
        return "early_ramp_with_visual_anchors"
    if section10 <= 10:
        return "loop_formation"
    if section10 <= 15:
        return "style_expansion"
    if section10 <= 20:
        return "read_demand_rise"
    if section10 <= 25:
        return "front300_core_middle"
    if section10 <= 30:
        return "front300_core_closure"
    if section10 <= 35:
        return "advanced_variation"
    if section10 <= 40:
        return "high_read"
    if section10 <= 45:
        return "mature_review"
    return "endgame"


def normal_center(section10: int) -> float:
    explicit = {
        1: 22,
        2: 36,
        3: 44,
        4: 50,
        5: 58,
        6: 62,
        7: 66,
        8: 70,
        9: 74,
        10: 78,
    }
    if section10 in explicit:
        return explicit[section10]
    if section10 <= 20:
        return 78 + (section10 - 10) * 1.4
    if section10 <= 30:
        return 92 + (section10 - 20) * 0.9
    if section10 <= 40:
        return 101 + (section10 - 30) * 0.5
    return min(112, 106 + (section10 - 40) * 0.35)


POSITION_OFFSET = {
    1: -8,
    2: -3,
    3: -1,
    4: 0,
    5: 3,
    6: 8,
    7: -5,
    8: 10,
    9: 2,
    10: 14,
}

FRONT20_BANDS = {
    1: (3, 8),
    2: (8, 18),
    3: (18, 28),
    4: (20, 30),
    5: (24, 34),
    6: (24, 36),
    7: (30, 40),
    8: (32, 42),
    9: (30, 42),
    10: (34, 42),
    11: (28, 38),
    12: (30, 42),
    13: (32, 44),
    14: (34, 46),
    15: (38, 50),
    16: (40, 52),
    17: (32, 44),
    18: (44, 56),
    19: (56, 66),
    20: (36, 48),
}

SPECIFIC_LOAD_BANDS = {
    48: (70, 76, 82),   # release after the L47 early extreme slot
    65: (90, 96, 102),  # keep the section peak without an early-wall jump
    66: (74, 80, 86),   # hard-code slot, but first duty is release after L65
    96: (106, 112, 118),  # section 10 peak, but still front100-readable
}


def target_load_band(order: int, section10: int, position: int, code: int, category: str) -> tuple[int, int, int]:
    if order in FRONT20_BANDS:
        lo, hi = FRONT20_BANDS[order]
        return lo, round((lo + hi) / 2), hi
    if order in SPECIFIC_LOAD_BANDS:
        lo, mid, hi = SPECIFIC_LOAD_BANDS[order]
        return lo, mid, hi

    center = normal_center(section10) + POSITION_OFFSET.get(position, 0)
    if code == 2:
        center += 15 if section10 <= 10 else 18
    elif code == 3:
        center += 26 if section10 <= 10 else 30
    elif code >= 4:
        center += 38 if section10 <= 10 else 45

    if category == "shape":
        center += 3
    elif category == "hole":
        center += 5 if code >= 2 else 1

    if code == 1:
        center = min(center, 108 if section10 <= 30 else 112)
    elif code == 2:
        center = min(center, 130)
    elif code == 3:
        center = min(center, 146)
    else:
        center = min(center, 160)

    width = 6
    if code == 2:
        width = 8
    elif code == 3:
        width = 10
    elif code >= 4:
        width = 12

    lo = max(3, int(round(center - width)))
    hi = int(round(center + width))
    return lo, int(round(center)), hi


FRONT20_SLOT_DUTIES = {
    1: ("TutorialRule", "Flow/Mixed", "curve_short", "first click, chain clear, win condition"),
    2: ("HoleSpatialAnchor", "Hole/Mixed", "theme_space", "rescue theme and hole-space contrast"),
    3: ("CanvasStep", "Flow/Peel/Mixed", "rail_clear", "camera and wider board scale introduction"),
    4: ("ShapeAnchor", "ShapeHosted/Peel/Mixed", "theme_shape", "first crystalball visual unlock"),
    5: ("NormalPractice", "Flow/Peel", "rail_or_curve", "post-shape normal continuation"),
    6: ("ShapeAnchor", "ShapeHosted/Peel/Mixed", "theme_shape", "second shape unlock, keep pressure low"),
    7: ("ReadCheck", "Peel/LockLite/Mixed", "rail_patch", "first lock-like one-step read"),
    8: ("NormalPractice", "Flow/Peel/Mixed", "curve_or_rail", "ordinary practice and gentle ramp"),
    9: ("ShapeAnchor", "ShapeHosted/Peel/Mixed", "theme_shape", "third visual reward"),
    10: ("ReadCheck", "Peel/LockLite/Mixed", "rail_patch", "front10 light quiz"),
    11: ("RecoveryFlow", "Flow/Mixed", "rail_or_curve", "release after L10"),
    12: ("NormalPractice", "Flow/Peel/Mixed", "rail_or_curve", "formal board baseline"),
    13: ("ShapeAnchor", "ShapeHosted/Peel/Mixed", "theme_shape", "magicwand visual unlock"),
    14: ("LanguageVariation", "Mixed/Peel/Flow", "curve_to_rail_shift", "chain language switch"),
    15: ("CanvasStep", "Peel/Mixed", "rail_patch", "first section feeling"),
    16: ("ReadCheck", "Peel/LockLite/Mixed", "rail_patch", "lock structure rehearsal"),
    17: ("HoleSpatialAnchor", "Hole/Mixed", "theme_space", "hole rescue breath"),
    18: ("BridgeRamp", "Peel/Mixed", "rail_patch", "pre-L19 upper normal"),
    19: ("DependencyPeak", "Peel/LongChainLite/Lock", "spine_lite_or_rail", "first front20 boss"),
    20: ("RecoveryFlow", "Flow/Peel/Mixed", "curve_or_rail", "release and next theme bridge"),
}


def duty_for(row: dict[str, str]) -> tuple[str, str, str, str]:
    order = parse_int(row.get("order"))
    position = parse_int(row.get("positionIn10"), ((order - 1) % 10) + 1)
    code = parse_int(row.get("difficultyCode"), 1)
    category = safe(row, "category", "normal")
    section10 = parse_int(row.get("section10"), ((order - 1) // 10) + 1)

    if order in FRONT20_SLOT_DUTIES:
        return FRONT20_SLOT_DUTIES[order]

    if category == "shape":
        if code >= 4:
            return (
                "ExtremeMemory",
                "ShapeHosted/LongChain/Hub/Peel",
                "theme_peak",
                "shape hosts a rare memory peak; difficulty must come from read/dependency evidence",
            )
        if code == 3:
            return (
                "StylePeak",
                "ShapeHosted/Peel/LongChainLite/HubLite",
                "theme_peak",
                "shape hosts this section peak as a visual lens, not the sole difficulty source",
            )
        if code == 2:
            return (
                "ReadCheck",
                "ShapeHosted/Peel/Mixed",
                "theme_read",
                "shape read-check with only a mild shape premium",
            )
        return (
            "ShapeAnchor",
            "ShapeHosted/Flow/Peel/Mixed",
            "theme_shape",
            "visual perspective/reward anchor; keep readability first",
        )

    if category == "hole":
        if code >= 4:
            return ("ExtremeMemory", "Hole/LongChain/Hub/Peel", "theme_peak", "hole carries an extreme spatial memory")
        if code == 3:
            return ("StylePeak", "Hole/Peel/LongChainLite", "theme_peak", "hole carries this section peak")
        if code == 2:
            return ("CanvasStep", "Hole/Mixed/Peel", "space_read", "hole raises spatial reading")
        return ("HoleSpatialAnchor", "Hole/Mixed/Peel", "theme_space", "space variation or breath")

    if code >= 4:
        return ("ExtremeMemory", "LongChain/Hub/Peel/Mixed", "spine_rail_patch", "chapter or half-chapter boss")
    if code == 3:
        if section10 >= 20 or position in {9, 10}:
            return ("DependencyPeak", "Peel/LongChain/Hub", "spine_rail_patch", "section peak with dependency consequence")
        return ("StylePeak", "Peel/LongChainLite/Mixed", "rail_patch", "early section peak with readable theme")
    if code == 2:
        if position in {6, 8}:
            return ("ReadCheck", "Peel/HubLite/Mixed", "rail_patch", "hard slot asks for one clear read")
        if position in {5, 7}:
            return ("LocalRunBreaker", "Peel/Mixed/HubLite", "rail_patch", "break same-area clearing")
        return ("CanvasStep", "Mixed/Peel", "mixed_or_rail", "difficulty comes from scale or density")

    by_position = {
        1: ("RecoveryFlow", "Flow/Mixed", "curve_mixed", "section opener or post-peak release"),
        2: ("NormalPractice", "Flow/Peel/Mixed", "curve_rail", "practice current section language"),
        3: ("LanguageVariation", "Mixed/Peel/Flow", "language_shift", "fresh chain language without pressure spike"),
        4: ("LanguageVariation", "Mixed/Peel/Flow", "language_shift", "variation if not occupied by shape/hole"),
        5: ("BridgeRamp", "Peel/Mixed", "rail_patch", "upper normal before read slot"),
        6: ("NormalPractice", "Flow/Peel/Mixed", "curve_rail", "template normal in read-check position stays practice"),
        7: ("RecoveryFlow", "Flow/Peel/Mixed", "different_from_previous", "breath after mid-section pressure"),
        8: ("BridgeRamp", "Peel/Mixed", "rail_patch", "ordinary upper-ramp slot"),
        9: ("SetupOrBreath", "Mixed/Peel/Shape", "variation_first", "peak setup or light release"),
        10: ("SetupOrBreath", "Mixed/Flow/Peel", "variation_first", "end-of-section normal release if no peak code"),
    }
    return by_position.get(position, by_position[2])


def read_demand_for(slot_duty: str, code: int) -> str:
    if slot_duty in {"TutorialRule", "RecoveryFlow"}:
        return "low_smooth"
    if slot_duty in {"NormalPractice", "LanguageVariation", "ShapeAnchor", "HoleSpatialAnchor"}:
        return "low_to_mid"
    if slot_duty in {"BridgeRamp", "CanvasStep", "SetupOrBreath"}:
        return "mid"
    if slot_duty in {"ReadCheck", "LocalRunBreaker"}:
        return "mid_plus_requires_reread"
    if code >= 4 or slot_duty == "ExtremeMemory":
        return "high_but_readable"
    return "high_with_dependency"


def required_evidence(slot_duty: str, code: int, category: str) -> str:
    base = ["solved", "initial_clearable", "nonzero_difficulty"]
    if code == 1:
        base += ["controlled_local_run", "no_directional_sweep_risk"]
    elif code == 2:
        base += ["region_or_axis_switch", "local_run_break", "max_choices_controlled"]
    elif code == 3:
        base += ["cross_region_read", "dependency_or_choke_consequence", "post_peak_release_available"]
    else:
        base += ["strong_read_evidence", "dependency_depth_or_memory_peak", "fair_no_random_hidden_gate"]

    if slot_duty in {"ReadCheck", "LocalRunBreaker"}:
        base += ["forced_reread_or_remote_narrow_frontier"]
    if slot_duty in {"DependencyPeak", "ExtremeMemory"}:
        base += ["meaningful_consequence_after_key_move"]
    if category == "shape":
        base += ["category_theme_visible_first_viewport", "shape_perspective_variation"]
        if code >= 3:
            base += ["underlying_non_shape_read_evidence"]
        else:
            base += ["shape_not_primary_difficulty_source"]
    elif category == "hole":
        base += ["category_theme_visible_first_viewport"]
    return ";".join(dict.fromkeys(base))


def avoid_rules(slot_duty: str, code: int) -> str:
    rules = ["illegal_chain", "unsolved", "zero_difficulty"]
    if slot_duty in {"RecoveryFlow", "NormalPractice", "TutorialRule"}:
        rules += ["surprise_hard_wall", "choice_explosion", "visual_overload"]
    if slot_duty in {"ReadCheck", "LocalRunBreaker", "DependencyPeak", "ExtremeMemory"}:
        rules += ["pure_local_sweep", "same_region_conveyor", "fake_difficulty_only_more_chains"]
    if code >= 3:
        rules += ["peak_not_above_section_normal", "no_post_peak_release"]
    return ";".join(dict.fromkeys(rules))


def priority_for(order: int, code: int, category: str, slot_duty: str) -> str:
    if order <= 20:
        return "P0_front20_manual"
    if order <= 100:
        return "P1_front100_lock"
    if order <= 300:
        return "P1_front300_quality"
    if code >= 3:
        return "P2_late_peak"
    if category in {"shape", "hole"}:
        return "P2_category_anchor"
    if slot_duty in {"ReadCheck", "LocalRunBreaker"}:
        return "P2_late_readcheck"
    return "P3_late_fill"


def production_need(slot_duty: str, code: int, order: int, category: str) -> str:
    if order == 1:
        return "keep_original"
    if category == "shape":
        if code == 1:
            return "use_existing_shape_anchor_pool"
        if code == 2:
            return "partial_supply_need_shape_readcheck"
        if code == 3:
            return "needs_underlying_hard_supply_shape_cameo"
        return "needs_extreme_peak_supply_shape_cameo"
    if category == "hole" and code == 1:
        return "use_existing_hole_anchor_pool"
    if slot_duty in {"RecoveryFlow", "NormalPractice", "LanguageVariation", "BridgeRamp", "SetupOrBreath", "CanvasStep"}:
        if code >= 2:
            return "partial_supply_need_normalplus_readcheck"
        return "normal_pool_available_match_by_band"
    if slot_duty in {"ReadCheck", "LocalRunBreaker"}:
        return "partial_supply_need_normalplus_readcheck"
    if slot_duty in {"DependencyPeak", "StylePeak"}:
        return "needs_hard_or_special_peak_supply"
    if slot_duty == "ExtremeMemory":
        return "needs_extreme_peak_supply"
    return "needs_review"


def effective_chain_note(category: str, slot_duty: str) -> str:
    if slot_duty in {"LongChainRead", "DependencyPeak", "ExtremeMemory"}:
        return "longchain/spine candidates may count below raw-chain target; use effective load"
    if category == "shape":
        return "shape is a visual/perspective wrapper with slight Nutation premium; hard/peak needs non-shape read evidence"
    if category == "hole":
        return "hole carries spatial reading/breath; evaluate with space context"
    return "raw chains are only an anchor; match by effective load and evidence"


def section_style_focus(section10: int) -> str:
    if section10 <= 2:
        return "Flow,Peel,Shape,Hole,LockLite"
    if section10 <= 5:
        return "Flow,Peel,Mixed,Shape,Hole,LongChainLite"
    if section10 <= 10:
        return "Flow,Peel,Mixed,LongChainLite"
    if section10 <= 15:
        return "Peel,Mixed,LongChainLite,Shape"
    if section10 <= 20:
        return "Peel,Mixed,HubLite,LongChain"
    if section10 <= 30:
        return "Peel,Hub,LongChain,Mixed"
    if section10 <= 40:
        return "Hub,LongChain,Peel,MazeLike"
    return "BestOfFlow,Peel,Hub,LongChain,Shape,Hole"


def section_notes(section10: int) -> str:
    if section10 == 1:
        return "No extreme slot. L10 is only a light quiz around effective load 35-42."
    if section10 == 2:
        return "L19 is the first boss around effective load 56-66; do not follow raw template 94 blindly."
    if section10 <= 5:
        return "Rise gently; hard/special only need clear contrast, not late-game density."
    if section10 <= 10:
        return "Form stable loop: normal reaches about 60-80, with visible hard/peak contrast."
    if section10 <= 20:
        return "Stop increasing every normal slot; use style/read variation for freshness."
    if section10 <= 30:
        return "Front300 quality core: reduce old weak candidates, require read evidence for hard+."
    return "Late campaign may keep reasonable old candidates but must preserve release after peaks."


def build_section_plan(rows: list[dict[str, str]]) -> list[dict[str, Any]]:
    by_section: dict[int, list[dict[str, str]]] = defaultdict(list)
    for row in rows:
        by_section[parse_int(row.get("section10"))].append(row)

    output: list[dict[str, Any]] = []
    for section10 in range(1, 51):
        section_rows = by_section[section10]
        counts_code = Counter(parse_int(r.get("difficultyCode"), 1) for r in section_rows)
        counts_cat = Counter(safe(r, "category", "normal") for r in section_rows)
        lo1, mid1, hi1 = target_load_band((section10 - 1) * 10 + 2, section10, 2, 1, "normal")
        lo2, mid2, hi2 = target_load_band((section10 - 1) * 10 + 8, section10, 8, 2, "normal")
        lo3, mid3, hi3 = target_load_band((section10 - 1) * 10 + 10, section10, 10, 3, "normal")
        lo4, mid4, hi4 = target_load_band((section10 - 1) * 10 + 10, section10, 10, 4, "normal")
        peak_band = band_string(lo4, hi4) if counts_code.get(4, 0) else band_string(lo3, hi3)
        output.append(
            {
                "section10": section10,
                "levelRange": f"{(section10 - 1) * 10 + 1}-{section10 * 10}",
                "macroArc": macro_arc_name(section10),
                "sectionRole": section_role(section10),
                "templateNormal": counts_code.get(1, 0),
                "templateHard": counts_code.get(2, 0),
                "templateSpecial": counts_code.get(3, 0),
                "templateExtreme": counts_code.get(4, 0),
                "templateShape": counts_cat.get("shape", 0),
                "templateHole": counts_cat.get("hole", 0),
                "normalEffectiveBand": band_string(lo1, hi1),
                "hardEffectiveBand": band_string(lo2, hi2),
                "peakEffectiveBand": peak_band,
                "styleFocus": section_style_focus(section10),
                "sectionPlanningNote": section_notes(section10),
            }
        )
    return output


def build_slot_spec(template_rows: list[dict[str, str]], front20_rows: list[dict[str, str]]) -> list[dict[str, Any]]:
    front20_by_order = {parse_int(r.get("order")): r for r in front20_rows}
    output: list[dict[str, Any]] = []

    for row in sorted(template_rows, key=lambda r: parse_int(r.get("order"))):
        order = parse_int(row.get("order"))
        section10 = parse_int(row.get("section10"), ((order - 1) // 10) + 1)
        position = ((order - 1) % 10) + 1
        category = safe(row, "category", "normal")
        code = parse_int(row.get("difficultyCode"), 1)
        slot_duty, style, chain_lang, player_goal = duty_for({**row, "positionIn10": str(position)})
        lo, mid, hi = target_load_band(order, section10, position, code, category)
        front20 = front20_by_order.get(order, {})

        output.append(
            {
                "order": order,
                "section10": section10,
                "positionIn10": position,
                "macroArc": macro_arc_name(section10),
                "sectionRole": section_role(section10),
                "levelName": safe(row, "levelName"),
                "category": category,
                "difficultyCode": code,
                "difficultyNameZh": safe(row, "difficultyNameZh"),
                "isShapeSlot": safe(row, "isShapeSlot"),
                "shapeId": safe(row, "shapeId"),
                "shapeNameZh": safe(row, "shapeNameZh"),
                "isHoleSlot": safe(row, "isHoleSlot"),
                "templateTargetChains": safe(row, "targetChains"),
                "templateTargetChainsMin": safe(row, "targetChainsMin"),
                "templateTargetChainsMax": safe(row, "targetChainsMax"),
                "templateArrowTiles": safe(row, "referenceArrowTiles"),
                "targetWidth": safe(row, "targetWidth"),
                "targetHeight": safe(row, "targetHeight"),
                "targetAspect": safe(row, "targetAspect"),
                "slotDutyV2": slot_duty,
                "targetExperience": player_goal,
                "front20DetailedDuty": safe(front20, "detailedDuty"),
                "front20PlayerPurpose": safe(front20, "playerPurpose"),
                "styleTargetV2": style,
                "chainLanguageTargetV2": chain_lang,
                "targetEffectiveLoadMin": lo,
                "targetEffectiveLoad": mid,
                "targetEffectiveLoadMax": hi,
                "effectiveLoadBand": band_string(lo, hi),
                "readDemandTarget": read_demand_for(slot_duty, code),
                "requiredEvidence": required_evidence(slot_duty, code, category),
                "avoidRules": avoid_rules(slot_duty, code),
                "loadInterpretationNote": effective_chain_note(category, slot_duty),
                "replacementPriority": priority_for(order, code, category, slot_duty),
                "productionNeed": production_need(slot_duty, code, order, category),
                "planningStatus": "locked_spec_not_resource_assignment",
                "selectedCandidateId": "",
                "selectedSource": "",
                "reviewStatus": "",
            }
        )
    return output


def build_demand_summary(slot_rows: list[dict[str, Any]]) -> list[dict[str, Any]]:
    buckets: dict[tuple[str, str, str], list[dict[str, Any]]] = defaultdict(list)
    for row in slot_rows:
        phase = "front20" if int(row["order"]) <= 20 else "front100" if int(row["order"]) <= 100 else "front300" if int(row["order"]) <= 300 else "after300"
        key = (phase, str(row["slotDutyV2"]), str(row["productionNeed"]))
        buckets[key].append(row)

    summary: list[dict[str, Any]] = []
    for (phase, duty, need), rows in sorted(buckets.items()):
        loads = [int(r["targetEffectiveLoad"]) for r in rows]
        summary.append(
            {
                "phase": phase,
                "slotDutyV2": duty,
                "productionNeed": need,
                "count": len(rows),
                "minOrder": min(int(r["order"]) for r in rows),
                "maxOrder": max(int(r["order"]) for r in rows),
                "avgTargetEffectiveLoad": round(mean(loads), 1) if loads else 0,
                "minTargetEffectiveLoad": min(loads) if loads else 0,
                "maxTargetEffectiveLoad": max(loads) if loads else 0,
                "priorityMix": ";".join(f"{k}:{v}" for k, v in Counter(str(r["replacementPriority"]) for r in rows).items()),
            }
        )
    return summary


def build_principles_md(slot_rows: list[dict[str, Any]], section_rows: list[dict[str, Any]], demand_rows: list[dict[str, Any]]) -> str:
    code_counts = Counter(str(r["difficultyCode"]) for r in slot_rows)
    duty_counts = Counter(str(r["slotDutyV2"]) for r in slot_rows)
    need_counts = Counter(str(r["productionNeed"]) for r in slot_rows)

    def bullet_counts(counter: Counter[str]) -> str:
        return "\n".join(f"- `{key}`: `{counter[key]}`" for key in sorted(counter))

    return f"""# Campaign500 Locked Planning Principles V2

Generated: 2026-07-02

This is the stable planning layer for Campaign500. It fixes the method before
resource assignment, so future iterations should adjust slot fields or candidate
matching, not restart the campaign logic from scratch.

## Locked Method

1. Keep the original 500-row template skeleton: order, category, difficultyCode,
   shape/hole flags, canvas hints, and source references remain the structural
   baseline.
2. Treat each 10-level group as a micro-loop: opener/release, practice, language
   variation, visual/spatial anchor, bridge ramp, read check, recovery, hard test,
   setup, and peak/memory.
3. Treat each 50-level block as a macro arc. Difficulty rises by read demand,
   region switching, dependency, and style/language variety; raw chain count is
   only an anchor.
4. Use targetEffectiveLoad for planning. LongChain and hole slots may feel
   harder than their raw chain count; shape has only a mild Nutation premium;
   RecoveryFlow may feel easier than raw chain count.
5. Treat shape as a visual/perspective wrapper first. Shape can host reward,
   read-check, hard, or occasional bottleneck duties, but core hard/extreme
   difficulty must be backed by underlying read/dependency evidence.
6. Front20 is hand-authored. L1 stays tutorial; L10 is a light quiz; L19 is the
   first mini-boss around effective load 56-66, not a raw-chain wall.
7. Current choke review candidates are NormalPlus/ReadCheck-lite supply, not
   hard/peak supply. True hard+ requires forced reread, remote narrow frontier,
   and meaningful downstream consequence.

## Output Files

- Slot spec: `{SLOT_SPEC_CSV.relative_to(ROOT).as_posix()}`
- Section plan: `{SECTION_PLAN_CSV.relative_to(ROOT).as_posix()}`
- Front100 preview: `{FRONT100_CSV.relative_to(ROOT).as_posix()}`
- Demand summary: `{DEMAND_SUMMARY_CSV.relative_to(ROOT).as_posix()}`

## Template Difficulty Count

{bullet_counts(code_counts)}

## Slot Duty Count

{bullet_counts(duty_counts)}

## Production Need Count

{bullet_counts(need_counts)}

## What Is Fixed Now

- The campaign rhythm model is fixed: 10-level loop + 50-level arc + effective
  load + slot duty evidence.
- Front20 duties and load bands are fixed unless directly playtest-rejected.
- Shape positioning is fixed as category rhythm and visual perspective, not a
  standalone hard-source. Hole remains spatial rhythm/anchor.
- Flow/Peel/Mixed are production-ready mainly for normal and normal-plus.
- Choke review evidence is currently normal-plus, not hard.

## What Still Needs Work

- Candidate classification must be rebuilt against V2 duties.
- Hard, special, and extreme slots need stronger production lanes than the
  current light choke pool.
- Front100 should be manually reviewed from the V2 preview before resource
  replacement.
- After candidate matching, run RhythmAuditV1 plus Unity playtest every 20/50
  levels.
"""


def build_gap_summary_md(demand_rows: list[dict[str, Any]]) -> str:
    lines = [
        "# Campaign500 Slot Spec V2 Gap Summary",
        "",
        "This summarizes demand implied by the locked V2 spec. It is not a candidate inventory.",
        "",
        "| phase | duty | production need | count | avg load | range |",
        "| --- | --- | --- | ---: | ---: | --- |",
    ]
    for row in demand_rows:
        lines.append(
            f"| {row['phase']} | {row['slotDutyV2']} | {row['productionNeed']} | {row['count']} | "
            f"{row['avgTargetEffectiveLoad']} | {row['minTargetEffectiveLoad']}-{row['maxTargetEffectiveLoad']} |"
        )
    lines.extend(
        [
            "",
            "## Main Reading",
            "",
            "- Normal and bridge slots can mostly use existing Flow/Peel/Mixed pools if they match effective load and repetition rules.",
            "- ReadCheck and LocalRunBreaker have only partial supply. Current choke candidates are useful as NormalPlus examples, not hard peaks.",
            "- DependencyPeak, StylePeak, and ExtremeMemory are the real production gaps. They should be generated from explicit hard-read/choke-rich lanes.",
            "- Shape-hosted peak slots should be supplied by the same hard-read/choke-rich lanes with shape as a theme lens, not by shape geometry alone.",
        ]
    )
    return "\n".join(lines) + "\n"


def main() -> None:
    template_rows = read_csv(TEMPLATE_CSV)
    front20_rows = read_csv(FRONT20_CSV) if FRONT20_CSV.exists() else []

    if len(template_rows) != 500:
        raise SystemExit(f"Expected 500 template rows, got {len(template_rows)}")

    section_rows = build_section_plan(template_rows)
    slot_rows = build_slot_spec(template_rows, front20_rows)
    demand_rows = build_demand_summary(slot_rows)

    section_fields = [
        "section10",
        "levelRange",
        "macroArc",
        "sectionRole",
        "templateNormal",
        "templateHard",
        "templateSpecial",
        "templateExtreme",
        "templateShape",
        "templateHole",
        "normalEffectiveBand",
        "hardEffectiveBand",
        "peakEffectiveBand",
        "styleFocus",
        "sectionPlanningNote",
    ]
    slot_fields = [
        "order",
        "section10",
        "positionIn10",
        "macroArc",
        "sectionRole",
        "levelName",
        "category",
        "difficultyCode",
        "difficultyNameZh",
        "isShapeSlot",
        "shapeId",
        "shapeNameZh",
        "isHoleSlot",
        "templateTargetChains",
        "templateTargetChainsMin",
        "templateTargetChainsMax",
        "templateArrowTiles",
        "targetWidth",
        "targetHeight",
        "targetAspect",
        "slotDutyV2",
        "targetExperience",
        "front20DetailedDuty",
        "front20PlayerPurpose",
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
        "replacementPriority",
        "productionNeed",
        "planningStatus",
        "selectedCandidateId",
        "selectedSource",
        "reviewStatus",
    ]
    demand_fields = [
        "phase",
        "slotDutyV2",
        "productionNeed",
        "count",
        "minOrder",
        "maxOrder",
        "avgTargetEffectiveLoad",
        "minTargetEffectiveLoad",
        "maxTargetEffectiveLoad",
        "priorityMix",
    ]

    write_csv(SECTION_PLAN_CSV, section_rows, section_fields)
    write_csv(SLOT_SPEC_CSV, slot_rows, slot_fields)
    write_csv(FRONT100_CSV, [r for r in slot_rows if int(r["order"]) <= 100], slot_fields)
    write_csv(DEMAND_SUMMARY_CSV, demand_rows, demand_fields)
    write_text(PRINCIPLES_MD, build_principles_md(slot_rows, section_rows, demand_rows))
    write_text(GAP_SUMMARY_MD, build_gap_summary_md(demand_rows))

    print(f"Wrote {SLOT_SPEC_CSV}")
    print(f"Wrote {SECTION_PLAN_CSV}")
    print(f"Wrote {DEMAND_SUMMARY_CSV}")
    print(f"Wrote {PRINCIPLES_MD}")


if __name__ == "__main__":
    main()
