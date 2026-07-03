#!/usr/bin/env python3
"""Direct-yaml build for the first50 V2 existing-resource review pack."""

from __future__ import annotations

import csv
import re
import uuid
from pathlib import Path
from typing import Any


ROOT = Path(__file__).resolve().parents[2]
LAYOUT_CSV = ROOT / "Exports" / "Campaign500_First50_V2Existing_20260702" / "campaign500_first50_v2_existing_layout_v1.csv"
PACK_PATH = ROOT / "Assets" / "ArrowMagic" / "SOData" / "Packs" / "Campaign500" / "Campaign500First50V2ExistingReviewPack.asset"
DEMO_SCENE = ROOT / "Assets" / "ArrowMagic" / "Scenes" / "Demo.unity"
REPORT_CSV = ROOT / "Exports" / "Campaign500_First50_V2Existing_20260702" / "campaign500_first50_v2_existing_pack_build_report.csv"
SUMMARY_MD = ROOT / "Exports" / "Campaign500_First50_V2Existing_20260702" / "campaign500_first50_v2_existing_pack_build_summary.md"

LEVEL_PACK_SCRIPT_GUID = "d436460122dc5424e9baa0dad136f725"
PACK_ID = "campaign500_first50_v2_existing_review"
DISPLAY_NAME = "Campaign500 First50 V2 Existing Review (50)"


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


def rel_path(path_value: str) -> Path:
    return ROOT / path_value.replace("/", "\\")


def guid_from_meta(asset_path: str) -> str:
    meta_path = rel_path(asset_path + ".meta")
    if not meta_path.exists():
        raise FileNotFoundError(f"Missing meta: {meta_path}")
    text = meta_path.read_text(encoding="utf-8-sig", errors="replace")
    match = re.search(r"^guid:\s*([0-9a-fA-F]{32})\s*$", text, flags=re.MULTILINE)
    if not match:
        raise ValueError(f"Missing guid in meta: {meta_path}")
    return match.group(1).lower()


def pack_guid() -> str:
    meta_path = PACK_PATH.with_suffix(PACK_PATH.suffix + ".meta")
    if meta_path.exists():
        text = meta_path.read_text(encoding="utf-8-sig", errors="replace")
        match = re.search(r"^guid:\s*([0-9a-fA-F]{32})\s*$", text, flags=re.MULTILINE)
        if match:
            return match.group(1).lower()
    return uuid.uuid4().hex


def write_pack(rows: list[dict[str, str]], guids: list[str], guid: str) -> None:
    PACK_PATH.parent.mkdir(parents=True, exist_ok=True)
    lines = [
        "%YAML 1.1",
        "%TAG !u! tag:unity3d.com,2011:",
        "--- !u!114 &11400000",
        "MonoBehaviour:",
        "  m_ObjectHideFlags: 0",
        "  m_CorrespondingSourceObject: {fileID: 0}",
        "  m_PrefabInstance: {fileID: 0}",
        "  m_PrefabAsset: {fileID: 0}",
        "  m_GameObject: {fileID: 0}",
        "  m_Enabled: 1",
        "  m_EditorHideFlags: 0",
        f"  m_Script: {{fileID: 11500000, guid: {LEVEL_PACK_SCRIPT_GUID}, type: 3}}",
        "  m_Name: Campaign500First50V2ExistingReviewPack",
        "  m_EditorClassIdentifier: ",
        f"  packId: {PACK_ID}",
        f"  displayName: {DISPLAY_NAME}",
        "  levels:",
    ]
    for level_guid in guids:
        lines.append(f"  - {{fileID: 11400000, guid: {level_guid}, type: 2}}")
    PACK_PATH.write_text("\n".join(lines) + "\n", encoding="utf-8")

    meta = [
        "fileFormatVersion: 2",
        f"guid: {guid}",
        "NativeFormatImporter:",
        "  externalObjects: {}",
        "  mainObjectFileID: 11400000",
        "  userData: ",
        "  assetBundleName: ",
        "  assetBundleVariant: ",
    ]
    PACK_PATH.with_suffix(PACK_PATH.suffix + ".meta").write_text("\n".join(meta) + "\n", encoding="utf-8")


def attach_demo(guid: str) -> None:
    text = DEMO_SCENE.read_text(encoding="utf-8-sig", errors="replace")
    pattern = r"activePack:\s*\{fileID:\s*11400000,\s*guid:\s*[0-9a-fA-F]{32},\s*type:\s*2\}"
    replacement = f"activePack: {{fileID: 11400000, guid: {guid}, type: 2}}"
    new_text, count = re.subn(pattern, replacement, text, count=1)
    if count != 1:
        raise ValueError("Could not find activePack reference in Demo.unity")
    DEMO_SCENE.write_text(new_text, encoding="utf-8")


def main() -> None:
    rows = read_csv(LAYOUT_CSV)
    if len(rows) != 50:
        raise SystemExit(f"Expected 50 layout rows, got {len(rows)}")
    rows = sorted(rows, key=lambda r: int(float(r["order"])))

    report: list[dict[str, Any]] = []
    guids: list[str] = []
    for row in rows:
        asset = row["finalAssetPath"]
        asset_exists = rel_path(asset).exists()
        meta_exists = rel_path(asset + ".meta").exists()
        if not asset_exists or not meta_exists:
            raise FileNotFoundError(f"Missing asset/meta for order {row['order']}: {asset}")
        guid = guid_from_meta(asset)
        guids.append(guid)
        report.append(
            {
                "order": row["order"],
                "category": row["category"],
                "difficultyCode": row["difficultyCode"],
                "slotDutyV2": row["slotDutyV2"],
                "finalLevelId": row["finalLevelId"],
                "finalAssetPath": asset,
                "levelGuid": guid,
                "sourceOrder": row["sourceOrder"],
                "finalChain": row["finalChain"],
                "candidateRawChains": row["candidateRawChains"],
                "bandFit": row["bandFit"],
            }
        )

    pguid = pack_guid()
    write_pack(rows, guids, pguid)
    attach_demo(pguid)
    write_csv(
        REPORT_CSV,
        report,
        [
            "order",
            "category",
            "difficultyCode",
            "slotDutyV2",
            "finalLevelId",
            "finalAssetPath",
            "levelGuid",
            "sourceOrder",
            "finalChain",
            "candidateRawChains",
            "bandFit",
        ],
    )
    SUMMARY_MD.write_text(
        "\n".join(
            [
                "# Campaign500 First50 V2 Existing Review Pack",
                "",
                f"- Pack: `{PACK_PATH.relative_to(ROOT).as_posix()}`",
                f"- PackGuid: `{pguid}`",
                f"- Levels: `{len(rows)}`",
                f"- Layout: `{LAYOUT_CSV.relative_to(ROOT).as_posix()}`",
                f"- DemoScene: `{DEMO_SCENE.relative_to(ROOT).as_posix()}`",
                "- Build mode: direct-yaml because the main Unity project may already be open.",
            ]
        )
        + "\n",
        encoding="utf-8",
    )
    print(f"Built {PACK_PATH}")
    print(f"Attached Demo activePack guid {pguid}")


if __name__ == "__main__":
    main()
