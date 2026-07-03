#!/usr/bin/env python3
"""Audit Campaign500 pacing from layout CSV metadata.

This is a static rhythm audit, not a gameplay solver. It checks whether a
500-level arrangement has reasonable local pacing, section contrast, front20
onboarding shape, source/language variety, and post-peak release.
"""

from __future__ import annotations

import argparse
import csv
from collections import Counter, defaultdict
from dataclasses import dataclass
from pathlib import Path
from statistics import mean
from typing import Any


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


def parse_int(value: Any, default: int = 0) -> int:
    if value is None:
        return default
    s = str(value).strip()
    if not s:
        return default
    try:
        return int(float(s))
    except ValueError:
        return default


def parse_float(value: Any, default: float = 0.0) -> float:
    if value is None:
        return default
    s = str(value).strip()
    if not s:
        return default
    try:
        return float(s)
    except ValueError:
        return default


def truthy(value: Any) -> bool:
    return str(value).strip().lower() in {"1", "true", "yes", "y"}


def pick(row: dict[str, str], *names: str, default: str = "") -> str:
    for name in names:
        value = row.get(name)
        if value is not None and str(value).strip() != "":
            return str(value).strip()
    return default


def chain_value(row: dict[str, str]) -> float:
    for name in (
        "finalChain",
        "targetEffectiveLoad",
        "canvasAdjustedChain",
        "projectedChain",
        "targetChains",
        "originalTargetChains",
        "baseChain",
    ):
        if name in row and str(row.get(name, "")).strip() != "":
            return parse_float(row.get(name), 0.0)
    return 0.0


def arrow_value(row: dict[str, str]) -> float:
    for name in ("finalArrow", "projectedArrow", "originalArrowTiles", "referenceArrowTiles"):
        if name in row and str(row.get(name, "")).strip() != "":
            return parse_float(row.get(name), 0.0)
    return 0.0


def aspect_value(row: dict[str, str]) -> float:
    if "targetAspect" in row:
        return parse_float(row.get("targetAspect"), 0.0)
    w = parse_float(row.get("targetWidth"), 0.0)
    h = parse_float(row.get("targetHeight"), 0.0)
    if w > 0 and h > 0:
        return w / h
    return 0.0


def style_token(row: dict[str, str]) -> str:
    parts = [
        pick(row, "rhythmBucket"),
        pick(row, "productionLane"),
        pick(row, "chainLanguage"),
        pick(row, "sourcePool"),
        pick(row, "styleTargetV2"),
        pick(row, "chainLanguageTargetV2"),
    ]
    joined = "|".join(p for p in parts if p)
    return joined or "unknown"


@dataclass
class Finding:
    severity: str
    scope: str
    order: str
    section10: str
    rule: str
    message: str

    def row(self) -> dict[str, str]:
        return {
            "severity": self.severity,
            "scope": self.scope,
            "order": self.order,
            "section10": self.section10,
            "rule": self.rule,
            "message": self.message,
        }


class Audit:
    def __init__(
        self,
        layout_rows: list[dict[str, str]],
        duty_rows: list[dict[str, str]] | None,
        story_rows: list[dict[str, str]] | None,
        expected_count: int = 500,
    ) -> None:
        self.rows = sorted(layout_rows, key=lambda r: parse_int(r.get("order"), 0))
        self.duty_by_order = {parse_int(r.get("order"), 0): r for r in (duty_rows or [])}
        self.story_by_section = {parse_int(r.get("section10"), 0): r for r in (story_rows or [])}
        self.expected_count = expected_count
        self.findings: list[Finding] = []
        self.section_rows: list[dict[str, Any]] = []

    def add(self, severity: str, scope: str, order: Any, section10: Any, rule: str, message: str) -> None:
        self.findings.append(Finding(severity, scope, str(order), str(section10), rule, message))

    def validate_shape(self) -> None:
        if len(self.rows) != self.expected_count:
            self.add("ERROR", "global", "", "", "row_count", f"expected {self.expected_count} rows, got {len(self.rows)}")
        orders = [parse_int(r.get("order"), 0) for r in self.rows]
        missing = [i for i in range(1, self.expected_count + 1) if i not in set(orders)]
        duplicates = [o for o, c in Counter(orders).items() if c > 1]
        if missing:
            self.add("ERROR", "global", "", "", "order_contiguous", f"missing orders: {missing[:20]}")
        if duplicates:
            self.add("ERROR", "global", "", "", "order_unique", f"duplicate orders: {duplicates[:20]}")
        for row in self.rows:
            order = parse_int(row.get("order"), 0)
            expected_section = ((order - 1) // 10) + 1
            actual_section = parse_int(row.get("section10"), expected_section)
            if actual_section != expected_section:
                self.add(
                    "ERROR",
                    "level",
                    order,
                    actual_section,
                    "section10",
                    f"section10 should be {expected_section} for order {order}, got {actual_section}",
                )

    def audit_front20(self) -> None:
        for row in self.rows[:20]:
            order = parse_int(row.get("order"), 0)
            code = parse_int(row.get("difficultyCode", row.get("templateDifficultyCode")), 1)
            category = pick(row, "category")
            ch = chain_value(row)
            changed = truthy(row.get("changedFromOriginalTemplate")) or pick(row, "replacementMark").startswith("V5")
            duty = self.duty_by_order.get(order, {})
            duty_name = pick(duty, "detailedDuty", "slotDutyV2", default="front20 duty")

            if order == 1 and changed:
                self.add("WARN", "level", order, 1, "front20_tutorial_keep", "L1 tutorial was replaced; first level should usually stay fixed or be manually verified.")
            if order == 2 and category != "hole":
                self.add("WARN", "level", order, 1, "front20_theme", "L2 should remain a rescue/hole theme beat.")
            if order == 3 and ch > 35:
                self.add("WARN", "level", order, 1, "front20_scale_intro", f"L3 {duty_name} has chain {ch:.0f}; camera/board-scale guide should stay low-mid.")
            if order == 10 and ch > 65:
                self.add("WARN", "level", order, 1, "front10_quiz", f"L10 light-hard quiz chain {ch:.0f} may be too high for first 10.")
            if order == 19 and ch > 75:
                self.add("WARN", "level", order, 2, "front20_boss", f"L19 first mini-boss chain {ch:.0f} is above the planned effective-load band; verify it is not early hard wall.")
            if order <= 20 and code >= 4:
                self.add("WARN", "level", order, ((order - 1) // 10) + 1, "front20_extreme", "front20 should not contain extreme difficulty unless explicitly hand-authored.")

    def audit_level_local(self) -> None:
        previous: dict[str, str] | None = None
        same_style_run = 1
        previous_style = ""
        for row in self.rows:
            order = parse_int(row.get("order"), 0)
            section = parse_int(row.get("section10"), ((order - 1) // 10) + 1)
            code = parse_int(row.get("difficultyCode", row.get("templateDifficultyCode")), 1)
            category = pick(row, "category")
            ch = chain_value(row)
            arrows = arrow_value(row)
            aspect = aspect_value(row)
            token = style_token(row)

            if token == previous_style and token != "unknown":
                same_style_run += 1
            else:
                same_style_run = 1
            previous_style = token
            if same_style_run >= 4:
                self.add("WARN", "level", order, section, "style_repetition", f"same style/source/language token appears {same_style_run} times in a row.")

            if aspect > 0 and (aspect < 0.45 or aspect > 1.0):
                self.add("INFO", "level", order, section, "aspect_outlier", f"aspect {aspect:.3f} is outside the preferred portrait band; verify visual comfort.")
            if aspect > 0.9 and category == "normal" and order > 20:
                self.add("INFO", "level", order, section, "portrait_band", f"normal level aspect {aspect:.3f} exceeds 0.9 preferred cap.")
            if ch > 145 and code <= 2:
                self.add("WARN", "level", order, section, "chain_cap", f"non-peak chain {ch:.0f} is very high; late difficulty should come from read/region/dependency, not infinite chains.")
            elif ch > 125 and code <= 2:
                self.add("INFO", "level", order, section, "chain_cap", f"non-peak chain {ch:.0f} is high; verify canvas comfort and avoid using count as the only pressure.")
            if arrows > 900 and code <= 2:
                self.add("WARN", "level", order, section, "arrow_cap", f"non-peak arrow count {arrows:.0f} may feel visually heavy.")
            elif arrows > 750 and code <= 2:
                self.add("INFO", "level", order, section, "arrow_cap", f"non-peak arrow count {arrows:.0f} is high; visual comfort should be checked in Unity.")

            if previous is not None:
                prev_order = parse_int(previous.get("order"), 0)
                prev_code = parse_int(previous.get("difficultyCode", previous.get("templateDifficultyCode")), 1)
                prev_ch = chain_value(previous)
                delta = ch - prev_ch
                if order <= 100 and delta > 35:
                    self.add("WARN", "level", order, section, "early_jump", f"chain jumps +{delta:.0f} from L{prev_order}; early sections prefer smoother rise.")
                elif delta > 50:
                    self.add("INFO", "level", order, section, "large_jump", f"chain jumps +{delta:.0f} from L{prev_order}; verify there is a clear reason.")
                if prev_code >= 4 and delta > -8:
                    self.add("WARN", "level", order, section, "post_peak_release", f"previous level was extreme peak code {prev_code}, but chain only changes {delta:.0f}; expected visible release.")
                elif prev_code >= 3 and delta > -8:
                    self.add("INFO", "level", order, section, "post_peak_release", f"previous level was peak code {prev_code}, but chain only changes {delta:.0f}; verify there is a non-count release.")
            previous = row

    def audit_sections(self) -> None:
        grouped: dict[int, list[dict[str, str]]] = defaultdict(list)
        for row in self.rows:
            order = parse_int(row.get("order"), 0)
            section = parse_int(row.get("section10"), ((order - 1) // 10) + 1)
            grouped[section].append(row)

        previous_avg = None
        expected_sections = max(1, (self.expected_count + 9) // 10)
        for section in range(1, expected_sections + 1):
            rows = sorted(grouped.get(section, []), key=lambda r: parse_int(r.get("order"), 0))
            if not rows:
                self.add("ERROR", "section", "", section, "missing_section", "section has no rows")
                continue
            chains = [chain_value(r) for r in rows]
            codes = [parse_int(r.get("difficultyCode", r.get("templateDifficultyCode")), 1) for r in rows]
            categories = [pick(r, "category") for r in rows]
            tokens = [style_token(r) for r in rows if style_token(r) != "unknown"]
            normal = [chain_value(r) for r, c in zip(rows, codes) if c == 1 and pick(r, "category") == "normal"]
            hard = [chain_value(r) for r, c in zip(rows, codes) if c == 2]
            peak = [chain_value(r) for r, c in zip(rows, codes) if c >= 3]
            avg_chain = mean(chains)
            max_chain = max(chains)
            min_chain = min(chains)
            normal_avg = mean(normal) if normal else 0.0
            hard_avg = mean(hard) if hard else 0.0
            peak_avg = mean(peak) if peak else 0.0
            style_diversity = len(set(tokens))
            range_chain = max_chain - min_chain
            warnings_before = len(self.findings)
            story = self.story_by_section.get(section, {})

            if section <= 2 and max_chain > 75:
                self.add("WARN", "section", "", section, "front20_pressure", f"front20 section max chain {max_chain:.0f} may be too high.")
            if section <= 5 and avg_chain > 75:
                self.add("WARN", "section", "", section, "early_avg_pressure", f"early section avg chain {avg_chain:.1f} may rise too fast.")
            if hard and normal and hard_avg < normal_avg + 8:
                self.add("WARN", "section", "", section, "hard_contrast", f"hard avg {hard_avg:.1f} is not clearly above normal avg {normal_avg:.1f}.")
            if peak and normal and peak_avg < normal_avg + 14:
                self.add("WARN", "section", "", section, "peak_contrast", f"peak avg {peak_avg:.1f} is not clearly above normal avg {normal_avg:.1f}.")
            if (hard or peak) and range_chain < 18:
                self.add("INFO", "section", "", section, "section_flatness", f"section chain range {range_chain:.1f} is narrow despite hard/peak slots.")
            if style_diversity < 3 and section > 2:
                self.add("INFO", "section", "", section, "style_diversity", f"only {style_diversity} style/source/language tokens detected.")

            deltas = [chains[i + 1] - chains[i] for i in range(len(chains) - 1)]
            positive = sum(1 for d in deltas if d > 0)
            negative = sum(1 for d in deltas if d < 0)
            if positive >= 8 and negative <= 1:
                self.add("INFO", "section", "", section, "too_linear", "section is almost strictly rising; add intentional retreats if it feels mechanical.")
            if previous_avg is not None and section > 2:
                section_delta = avg_chain - previous_avg
                if section_delta > 18:
                    self.add("INFO", "section", "", section, "section_avg_jump", f"section avg jumps +{section_delta:.1f} from previous section.")
            previous_avg = avg_chain

            warning_count = len(self.findings) - warnings_before
            section_score = max(0, 100 - warning_count * 12)
            self.section_rows.append(
                {
                    "section10": section,
                    "levels": f"{(section - 1) * 10 + 1}-{section * 10}",
                    "score": section_score,
                    "avgChain": round(avg_chain, 1),
                    "minChain": round(min_chain, 1),
                    "maxChain": round(max_chain, 1),
                    "normalAvg": round(normal_avg, 1),
                    "hardAvg": round(hard_avg, 1),
                    "peakAvg": round(peak_avg, 1),
                    "code1": codes.count(1),
                    "code2": codes.count(2),
                    "code3": codes.count(3),
                    "code4": codes.count(4),
                    "shape": categories.count("shape"),
                    "hole": categories.count("hole"),
                    "styleDiversity": style_diversity,
                    "findingCount": warning_count,
                    "sectionRole": pick(story, "sectionRole"),
                    "playerExperience": pick(story, "playerExperience"),
                    "topRisk": "; ".join(f.message for f in self.findings[warnings_before : warnings_before + 2]),
                }
            )

    def run(self) -> tuple[int, dict[str, Any]]:
        self.validate_shape()
        self.audit_front20()
        self.audit_level_local()
        self.audit_sections()
        severity_counts = Counter(f.severity for f in self.findings)
        section_average = mean([parse_float(r["score"], 100.0) for r in self.section_rows]) if self.section_rows else 0.0
        finding_penalty = min(25.0, severity_counts["WARN"] * 0.55 + severity_counts["INFO"] * 0.08)
        score = max(0, round(section_average - severity_counts["ERROR"] * 20 - finding_penalty))
        grade = "A"
        if score < 55:
            grade = "D"
        elif score < 70:
            grade = "C"
        elif score < 84:
            grade = "B"
        summary = {
            "score": score,
            "grade": grade,
            "errors": severity_counts["ERROR"],
            "warnings": severity_counts["WARN"],
            "infos": severity_counts["INFO"],
            "findings": len(self.findings),
            "sectionsBelow80": sum(1 for r in self.section_rows if parse_int(r["score"]) < 80),
        }
        return score, summary


def write_summary(path: Path, summary: dict[str, Any], findings: list[Finding], section_rows: list[dict[str, Any]], layout: Path) -> None:
    top_warns = [f for f in findings if f.severity in {"ERROR", "WARN"}][:15]
    low_sections = sorted(section_rows, key=lambda r: (parse_int(r["score"]), parse_int(r["section10"])))[:12]
    lines = [
        "# Campaign500 Rhythm Audit V1",
        "",
        f"- Layout: `{layout}`",
        f"- Score: **{summary['score']} / 100**",
        f"- Grade: **{summary['grade']}**",
        f"- Findings: {summary['findings']} total, {summary['errors']} errors, {summary['warnings']} warnings, {summary['infos']} infos",
        f"- Sections below 80: {summary['sectionsBelow80']}",
        "",
        "## Interpretation",
        "",
        "This is a static pacing audit. It does not replace Unity playtest or solver trace, but it catches obvious rhythm issues: early pressure spikes, weak hard/peak contrast, missing post-peak release, source/style repetition, and chain-count overgrowth.",
        "",
        "## Lowest Section Scores",
        "",
        "| section | levels | score | avg | normal | hard | peak | top risk |",
        "| --- | --- | ---: | ---: | ---: | ---: | ---: | --- |",
    ]
    for row in low_sections:
        lines.append(
            f"| {row['section10']} | {row['levels']} | {row['score']} | {row['avgChain']} | {row['normalAvg']} | {row['hardAvg']} | {row['peakAvg']} | {row['topRisk']} |"
        )
    lines.extend(["", "## Top Warnings", ""])
    if top_warns:
        for f in top_warns:
            loc = f"L{f.order}" if f.order else f"section {f.section10}" if f.section10 else f.scope
            lines.append(f"- **{f.severity} {loc} `{f.rule}`**: {f.message}")
    else:
        lines.append("- No ERROR/WARN findings.")
    lines.extend(
        [
            "",
            "## Suggested Use",
            "",
            "Use this report before assembling a pack. A section with low score should be manually reviewed against its 10-level script before candidate replacement continues.",
        ]
    )
    path.write_text("\n".join(lines) + "\n", encoding="utf-8")


def main() -> int:
    parser = argparse.ArgumentParser(description="Audit Campaign500 rhythm layout CSV.")
    parser.add_argument("--layout", required=True, type=Path)
    parser.add_argument("--duty", type=Path, default=None)
    parser.add_argument("--story", type=Path, default=None)
    parser.add_argument("--out-dir", required=True, type=Path)
    parser.add_argument("--expected-count", type=int, default=500)
    args = parser.parse_args()

    layout_rows = read_csv(args.layout)
    duty_rows = read_csv(args.duty) if args.duty and args.duty.exists() else None
    story_rows = read_csv(args.story) if args.story and args.story.exists() else None

    audit = Audit(layout_rows, duty_rows, story_rows, expected_count=args.expected_count)
    _, summary = audit.run()
    args.out_dir.mkdir(parents=True, exist_ok=True)

    write_csv(
        args.out_dir / "campaign500_rhythm_audit_v1_findings.csv",
        [f.row() for f in audit.findings],
        ["severity", "scope", "order", "section10", "rule", "message"],
    )
    write_csv(
        args.out_dir / "campaign500_rhythm_audit_v1_sections.csv",
        audit.section_rows,
        [
            "section10",
            "levels",
            "score",
            "avgChain",
            "minChain",
            "maxChain",
            "normalAvg",
            "hardAvg",
            "peakAvg",
            "code1",
            "code2",
            "code3",
            "code4",
            "shape",
            "hole",
            "styleDiversity",
            "findingCount",
            "sectionRole",
            "playerExperience",
            "topRisk",
        ],
    )
    write_summary(args.out_dir / "campaign500_rhythm_audit_v1_summary.md", summary, audit.findings, audit.section_rows, args.layout)
    write_csv(
        args.out_dir / "campaign500_rhythm_audit_v1_score.csv",
        [summary],
        ["score", "grade", "errors", "warnings", "infos", "findings", "sectionsBelow80"],
    )
    print(f"score={summary['score']} grade={summary['grade']} findings={summary['findings']}")
    print(args.out_dir)
    return 0 if summary["errors"] == 0 else 2


if __name__ == "__main__":
    raise SystemExit(main())
