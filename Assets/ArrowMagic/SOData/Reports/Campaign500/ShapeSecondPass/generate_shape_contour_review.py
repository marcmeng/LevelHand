import csv
import html
import re
from collections import Counter, defaultdict
from pathlib import Path


ROOT = Path(r"F:/Unityproject/ArrowLevel-Hand")
SHAPE_CSV = ROOT / "Exports/Campaign500SingleLevels_Hole10V4_20260613_095022/ShapeUsage/campaign500_shape_usage.csv"
OUT_DIR = ROOT / "Assets/ArrowMagic/SOData/Reports/Campaign500/ShapeSecondPass"


ZH_NAMES = {
    "shape_main_magic_quill_tall": "魔法羽毛笔",
    "shape_main_hourglass_tall": "沙漏",
    "shape_main_shield_tall": "盾牌",
    "shape_main_key_tall": "钥匙",
    "shape_main_magic_bat_tall": "蝙蝠",
    "shape_main_magic_black_cat_tall": "黑猫",
    "shape_main_rocket_tall": "火箭",
    "shape_main_distinct_puzzle_tall": "拼图",
    "shape_main_leaf09_tall": "叶子09",
    "shape_main_star_tall": "星星",
    "shape_main_distinct_crescent_tall": "月牙",
    "shape_main_distinct_music_note_tall": "音符",
    "shape_main_heart_tall": "爱心",
    "shape_main_paint_palette_tall": "调色盘",
    "shape_main_lightning_tall": "闪电",
    "shape_main_microphone_tall": "麦克风",
    "shape_main_ocean_drift_bottle_tall": "漂流瓶",
    "shape_main_distinct_digit2_tall": "数字2",
    "shape_main_magic_rune_stone_tall": "符文石",
    "shape_main_distinct_jellyfish_tall": "水母",
    "shape_main_magic_dragon_egg_tall": "龙蛋",
    "shape_main_owl_tall": "猫头鹰",
    "shape_main_knight_mascot_tall": "骑士吉祥物",
    "shape_main_space_comet_icon_tall": "彗星",
    "shape_main_magic_hat_tall": "魔法帽",
    "shape_main_liberty_statue_tall": "自由女神像",
    "shape_main_raised_hand_mascot_tall": "举手吉祥物",
    "shape_main_distinct_letter_mtall": "字母M",
    "shape_main_ocean_pufferfish_tall": "河豚",
    "shape_main_distinct_boot_tall": "靴子",
    "shape_main_candle_tall": "蜡烛",
    "shape_main_painter_bust_tall": "画家半身像",
    "shape_main_ocean_shell_tall": "贝壳",
    "shape_main_clock_tower_tall": "钟楼",
    "shape_main_ocean_starfish_tall": "海星",
    "shape_main_distinct_plug_tall": "插头",
    "shape_main_ocean_flippers_tall": "脚蹼",
    "shape_main_music_headphones_tall": "耳机",
    "shape_main_cactus_tall": "仙人掌",
    "shape_main_train_front_tall": "火车头",
    "shape_main_music_drum_tall": "鼓",
    "shape_main_distinct_ghost_tall": "幽灵",
    "shape_transition_distinct_scissors_tall": "剪刀",
    "shape_main_snowman_tall": "雪人",
    "shape_main_astronaut_mascot_tall": "宇航员",
    "shape_main_distinct_mitten_tall": "手套",
    "shape_main_space_saturn_icon_tall": "土星",
    "shape_main_ocean_seaweed_tall": "海草",
    "shape_main_ui_camera_tall": "相机",
    "shape_transition_magic_broom_tall": "魔法扫帚",
    "shape_main_ui_chat_bubble_tall": "聊天气泡",
    "shape_main_distinct_octopus_tall": "章鱼",
    "shape_main_tool_battery_tall": "电池",
    "shape_transition_ocean_diving_mask_tall": "潜水面罩",
    "shape_transition_wide_landmark01_tall": "地标01（需人工确认）",
    "shape_main_treasure_chest_tall": "宝箱",
    "shape_main_leaning_tower_tall": "斜塔",
    "shape_transition_ocean_sea_turtle_tall": "海龟",
    "shape_main_music_vinyl_record_tall": "黑胶唱片",
    "shape_main_bunny_tall": "兔子",
    "shape_transition_distinct_crab_tall": "螃蟹",
    "shape_transition_wide_landmark06_tall": "地标06（需人工确认）",
    "shape_transition_ocean_oxygen_tank_tall": "氧气瓶",
    "shape_transition_wide_landmark05_tall": "地标05（需人工确认）",
    "shape_main_classic_portrait_tall": "经典肖像",
    "shape_transition_pagoda_tall": "宝塔",
    "shape_transition_magic_knight_helmet_tall": "骑士头盔",
    "shape_transition_ocean_lobster_tall": "龙虾",
    "shape_transition_obelisk_tall": "方尖碑",
    "shape_transition_magic_cauldron_tall": "坩埚",
    "shape_transition_ocean_pearl_clam_tall": "珍珠贝",
    "shape_transition_bridge_tower_tall": "桥塔",
    "shape_transition_sailboat_tall": "帆船",
    "shape_transition_carrier02_tall": "航母02",
    "shape_transition_castle_gate_tall": "城堡门",
    "shape_transition_dome_palace_tall": "穹顶宫殿",
    "shape_transition_carrier03_tall": "航母03",
    "shape_transition_distinct_butterfly_tall": "蝴蝶",
    "shape_big_mega_monument05_tall": "大型纪念碑05",
    "shape_big_giant_robot_tall": "巨型机器人",
    "shape_big_carrier04_tall": "大型航母04",
    "shape_big_grove02_tall": "树林02",
    "shape_big_carrier06_tall": "大型航母06",
    "shape_big_grove04_tall": "树林04",
    "shape_main_distinct_digit5_tall": "数字5",
    "shape_main_distinct_tooth_tall": "牙齿",
    "shape_main_bean_mascot_tall": "豆豆吉祥物",
    "shape_main_pine_tree_tall": "松树",
    "shape_main_iron_tower_tall": "铁塔",
    "shape_main_tool_wrench_tall": "扳手",
    "shape_main_pyramid_tall": "金字塔",
    "shape_main_space_ufo_icon_tall": "UFO",
    "shape_main_windmill_tall": "风车",
    "shape_main_trophy_tall": "奖杯",
    "shape_main_fox_tall": "狐狸",
    "shape_main_leaf03_tall": "叶子03",
}


STRONG_SHAPE_TOKENS = [
    "star", "heart", "crescent", "lightning", "key", "rocket", "shield", "digit", "letter",
    "music_note", "microphone", "boot", "candle", "shell", "starfish", "plug", "scissors",
    "snowman", "saturn", "camera", "broom", "octopus", "battery", "bunny", "crab", "pagoda",
    "lobster", "obelisk", "cauldron", "clam", "sailboat", "butterfly", "tooth", "tree",
    "wrench", "pyramid", "ufo", "windmill", "trophy", "fox", "leaf",
]


COMPLEX_TOKENS = [
    "mascot", "landmark01", "landmark05", "landmark06", "carrier02", "carrier03",
    "carrier04", "carrier06", "grove02", "grove04", "monument05",
]


def read_level_cells(asset_rel: str):
    p = ROOT / asset_rel
    text = p.read_text(encoding="utf-8", errors="ignore")
    m = re.search(r"authoredLevel:\s*\n\s*width:\s*(\d+)\s*\n\s*height:\s*(\d+)", text)
    if not m:
        m = re.search(r"board:\s*\n\s*width:\s*(\d+)\s*\n\s*height:\s*(\d+)", text)
    width = int(m.group(1))
    height = int(m.group(2))
    cells = set()
    chains = 0
    for match in re.finditer(r"\n\s*- indices:\s*([0-9a-fA-F]+)", text):
        chains += 1
        data = bytes.fromhex(match.group(1))
        for i in range(0, len(data), 4):
            idx = int.from_bytes(data[i:i + 4], "little", signed=False)
            if 0 <= idx < width * height:
                cells.add(idx)
    return width, height, cells, chains


def runs_svg(width: int, height: int, cells: set[int]) -> str:
    runs = []
    for y in range(height):
        screen_y = height - 1 - y
        x = 0
        while x < width:
            if x + y * width not in cells:
                x += 1
                continue
            start = x
            while x < width and x + y * width in cells:
                x += 1
            runs.append((start, screen_y, x - start))
    rects = "".join(
        f'<rect x="{x}" y="{y}" width="{run_width}" height="1"/>'
        for x, y, run_width in runs
    )
    return (
        f'<svg class="shapeSvg" viewBox="0 0 {width} {height}" role="img" '
        f'aria-label="level contour"><rect class="boardBg" x="0" y="0" '
        f'width="{width}" height="{height}"/><g class="occupied">{rects}</g>'
        f'<rect class="boardBorder" x="0" y="0" width="{width}" height="{height}"/></svg>'
    )


def priority(row: dict, chinese_name: str):
    display = row["displayShapeId"].lower()
    mask_coverage = float(row["maskCoverage"] or 0)
    mask_fill = float(row["maskFill"] or 0)
    mask_area = int(float(row["maskArea"] or 0))
    score = 0
    reasons = []

    if "需人工确认" in chinese_name:
        score += 3
        reasons.append("中文名泛化")
    if any(token in display for token in COMPLEX_TOKENS):
        score += 2
        reasons.append("形象复杂或命名不够具体")
    if mask_coverage < 0.52:
        score += 2
        reasons.append("mask覆盖偏低")
    elif mask_coverage < 0.58:
        score += 1
        reasons.append("mask覆盖中等")
    if mask_fill < 0.97:
        score += 1
        reasons.append("实际填充略低")
    if mask_area < 650:
        score += 1
        reasons.append("轮廓面积偏小")
    if any(token in display for token in STRONG_SHAPE_TOKENS):
        score -= 1
        reasons.append("强轮廓物体")

    if score <= 0:
        return "A_一眼可读候选", ";".join(reasons)
    if score <= 2:
        return "B_需要肉眼确认", ";".join(reasons)
    return "C_重点复核", ";".join(reasons)


def main():
    OUT_DIR.mkdir(parents=True, exist_ok=True)
    rows = []
    with SHAPE_CSV.open(newline="", encoding="utf-8-sig") as f:
        for row in csv.DictReader(f):
            order = int(row["order"])
            if order <= 20:
                continue
            width, height, cells, asset_chains = read_level_cells(row["sourceAsset"])
            chinese_name = ZH_NAMES.get(row["displayShapeId"], row["displayShapeId"])
            review_priority, risk_reasons = priority(row, chinese_name)
            rows.append({
                "order": order,
                "中文名": chinese_name,
                "displayShapeId": row["displayShapeId"],
                "maskName": row["maskName"],
                "theme": row["theme"],
                "shapeRole": row["shapeRole"],
                "chains": row["chains"],
                "assetChains": asset_chains,
                "width": width,
                "height": height,
                "occupiedCells": len(cells),
                "occupiedCoverage": round(len(cells) / (width * height), 3) if width * height else 0,
                "maskCoverage": row["maskCoverage"],
                "maskFill": row["maskFill"],
                "reviewPriority": review_priority,
                "riskReasons": risk_reasons,
                "sourceAsset": row["sourceAsset"],
                "suggestedRevealPng": row["suggestedRevealPng"],
                "人工判断_是否一眼可认": "",
                "人工备注": "",
                "_svg": runs_svg(width, height, cells),
            })

    out_csv = OUT_DIR / "campaign500_shape_contour_review_post20.csv"
    out_html = OUT_DIR / "campaign500_shape_contour_review_post20.html"
    out_md = OUT_DIR / "campaign500_shape_contour_review_post20_summary.md"

    fieldnames = [key for key in rows[0].keys() if key != "_svg"]
    with out_csv.open("w", newline="", encoding="utf-8-sig") as f:
        writer = csv.DictWriter(f, fieldnames=fieldnames)
        writer.writeheader()
        writer.writerows([{k: v for k, v in row.items() if k != "_svg"} for row in rows])

    counts = Counter(row["reviewPriority"] for row in rows)
    theme_counts = Counter(row["theme"] for row in rows)
    segment_counts = defaultdict(Counter)
    for row in rows:
        segment = f"{((int(row['order']) - 1) // 100) * 100 + 1:03d}-{((int(row['order']) - 1) // 100 + 1) * 100:03d}"
        segment_counts[segment][row["reviewPriority"]] += 1

    cards = []
    for row in rows:
        priority_class = row["reviewPriority"].split("_")[0]
        cards.append(f"""
        <article class="card {priority_class}">
          <div class="metaTop"><span class="level">L{int(row['order']):03d}</span><span class="badge">{html.escape(row['reviewPriority'])}</span></div>
          <h2>{html.escape(row['中文名'])}</h2>
          <div class="sub">{html.escape(row['maskName'])}</div>
          <div class="svgWrap">{row['_svg']}</div>
          <dl>
            <dt>尺寸</dt><dd>{row['width']}x{row['height']}</dd>
            <dt>链条</dt><dd>{row['chains']}</dd>
            <dt>占用</dt><dd>{row['occupiedCoverage']}</dd>
            <dt>Mask</dt><dd>{float(row['maskCoverage']):.3f} / fill {float(row['maskFill']):.3f}</dd>
            <dt>原因</dt><dd>{html.escape(row['riskReasons'] or "-")}</dd>
          </dl>
        </article>
        """)

    out_html.write_text(f"""<!doctype html>
<html lang="zh-CN">
<head>
<meta charset="utf-8" />
<title>Campaign500 Shape 关卡轮廓二轮检查</title>
<style>
:root {{ --bg:#f6f1e2; --ink:#1d2433; --muted:#687084; --card:#fffdf5; }}
body {{ margin:0; font-family:"Microsoft YaHei", "Noto Sans SC", Arial, sans-serif; background:var(--bg); color:var(--ink); }}
header {{ position:sticky; top:0; z-index:2; background:#fff8e7; border-bottom:1px solid #e1d8c3; padding:14px 22px; }}
h1 {{ margin:0 0 6px; font-size:22px; }}
.summary {{ display:flex; flex-wrap:wrap; gap:10px 18px; color:var(--muted); font-size:13px; }}
main {{ padding:18px; display:grid; grid-template-columns:repeat(auto-fill, minmax(260px, 1fr)); gap:14px; }}
.card {{ background:var(--card); border:1px solid #ded6c4; border-radius:8px; padding:12px; box-shadow:0 1px 2px rgba(0,0,0,.04); }}
.card.B {{ border-color:#dfb36b; }} .card.C {{ border-color:#dc7777; }}
.metaTop {{ display:flex; justify-content:space-between; align-items:center; gap:8px; }}
.level {{ font-weight:800; font-size:16px; }}
.badge {{ font-size:12px; padding:3px 6px; border-radius:999px; background:#ece6d8; color:#5f594e; }}
.B .badge {{ background:#ffe7bd; color:#86530a; }} .C .badge {{ background:#ffd3d3; color:#9b2424; }}
h2 {{ margin:8px 0 2px; font-size:21px; line-height:1.15; }}
.sub {{ color:var(--muted); font-size:12px; overflow:hidden; text-overflow:ellipsis; white-space:nowrap; }}
.svgWrap {{ margin:10px auto; height:230px; display:flex; align-items:center; justify-content:center; background:#faf6ec; border:1px solid #ece3cf; border-radius:6px; }}
.shapeSvg {{ max-width:95%; max-height:95%; shape-rendering:crispEdges; }}
.boardBg {{ fill:#fffaf0; }} .occupied {{ fill:#24304a; }} .boardBorder {{ fill:none; stroke:#cfc4ad; stroke-width:.35; vector-effect:non-scaling-stroke; }}
dl {{ display:grid; grid-template-columns:52px 1fr; gap:3px 8px; margin:0; font-size:12px; }}
dt {{ color:#7a715f; }} dd {{ margin:0; }}
@media print {{ header {{ position:static; }} main {{ grid-template-columns:repeat(3, 1fr); }} .card {{ break-inside:avoid; }} }}
</style>
</head>
<body>
<header>
<h1>Campaign500 Shape 关卡轮廓二轮检查（20关后）</h1>
<div class="summary">
<span>数量：{len(rows)}</span>
<span>A 一眼可读候选：{counts.get("A_一眼可读候选", 0)}</span>
<span>B 需要肉眼确认：{counts.get("B_需要肉眼确认", 0)}</span>
<span>C 重点复核：{counts.get("C_重点复核", 0)}</span>
<span>判断方法：中文名 + 实际关卡占用轮廓</span>
</div>
</header>
<main>
{''.join(cards)}
</main>
</body>
</html>""", encoding="utf-8")

    lines = [
        "# Campaign500 Shape 关卡轮廓二轮检查（20关后）",
        "",
        f"- Shape 数量：{len(rows)}",
        f"- A_一眼可读候选：{counts.get('A_一眼可读候选', 0)}",
        f"- B_需要肉眼确认：{counts.get('B_需要肉眼确认', 0)}",
        f"- C_重点复核：{counts.get('C_重点复核', 0)}",
        "",
        "## 主题数量",
    ]
    for key, value in sorted(theme_counts.items()):
        lines.append(f"- {key}: {value}")

    lines += [
        "",
        "## 每100关分布",
        "| 区间 | A | B | C |",
        "|---|---:|---:|---:|",
    ]
    for segment, counter in sorted(segment_counts.items()):
        lines.append(
            f"| {segment} | {counter.get('A_一眼可读候选', 0)} | "
            f"{counter.get('B_需要肉眼确认', 0)} | {counter.get('C_重点复核', 0)} |"
        )

    lines += [
        "",
        "## B/C 复核列表",
        "| 关卡 | 中文名 | MaskName | 链条 | 尺寸 | 占用率 | Mask覆盖 | 优先级 | 原因 |",
        "|---:|---|---|---:|---|---:|---:|---|---|",
    ]
    for row in rows:
        if not str(row["reviewPriority"]).startswith("A_"):
            lines.append(
                f"| {row['order']} | {row['中文名']} | `{row['maskName']}` | {row['chains']} | "
                f"{row['width']}x{row['height']} | {row['occupiedCoverage']} | "
                f"{float(row['maskCoverage']):.3f} | {row['reviewPriority']} | {row['riskReasons']} |"
            )
    out_md.write_text("\n".join(lines), encoding="utf-8")

    print(f"rows={len(rows)}")
    print(f"A={counts.get('A_一眼可读候选', 0)}")
    print(f"B={counts.get('B_需要肉眼确认', 0)}")
    print(f"C={counts.get('C_重点复核', 0)}")
    print(out_csv.as_posix())
    print(out_html.as_posix())
    print(out_md.as_posix())


if __name__ == "__main__":
    main()
