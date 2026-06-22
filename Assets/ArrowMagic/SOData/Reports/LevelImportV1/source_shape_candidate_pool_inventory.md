# Shape 候选池汇总台账

Updated: 2026-06-11

Scope: Shape mask candidate pools under `Assets/ArrowMagic/Masks/Shape*` and their reports/packs under `Assets/ArrowMagic/SOData/Reports/ShapeExperiment` and `Assets/ArrowMagic/SOData/Packs/ShapeExperiment`.

Important rule: hole/blocker-style masks are not included here. These pools are visible-shape masks for arrow placement/fill. Hole 类型后续单独成体系，不混进 Shape 正式候选池。

## Current Production View

| Layer | Pool / Pack | Count | Status | Use |
|---|---|---:|---|---|
| Beginner | `EarlyPropSatisfyingPack.asset` | 4 | 新手专用预览 | Front-20 satisfying prop levels |
| Main final | `ProceduralMaskFillQualityTrimPack.asset` | 133 | Current trimmed Shape base | Main Shape production base |
| Final supplement | `ProceduralMaskFillFinalSupplementPack.asset` | 14 | Approved supplement | Space / music / tool UI diversity |
| Combined final | `ProceduralMaskFillFinalWithSupplementPack.asset` | 147 | Current combined final Shape pack | Final Shape review pack |
| Historical preview | `EarlySymbolPreviewPack.asset` | 4 | Early experiment | Small symbol baseline |
| Historical preview | `OriginalStarMaskPreviewPack.asset` | 1 | Early experiment | Original star baseline |
| Historical preview | Animal / readable / tall-fit packs | 3-9 each | Archived reference | Mask readability and sizing experiments |

## Candidate Pool Directory

| Pool Folder / Source | Main Series | Masks / Levels | Current Status | Main Pack / Report |
|---|---|---:|---|---|
| `ShapeEarlyPropPreview` | 新手专用 | 4 masks / 4 levels | 新增预览池；水晶球、橡皮擦、标尺、魔法棒；橡皮擦和魔法棒仍需美术复核 | `EarlyPropSatisfyingPack.asset`, `shape_early_prop_mask_catalog.csv` |
| `ShapeProceduralVerticalPreview` | 主力 Shape 源库 | 532 raw rows / 150 unique masks / 133 kept after trim | 当前主力候选源；正式使用以 quality trim 后的 133 为准 | `ProceduralMaskFillQualityTrimPack.asset`, `shape_procedural_vertical_preview_mask_catalog.csv` |
| `ShapeFinalSupplementPreview` | 最终补充 | 14 masks / 14 levels | 已作为最终补充并入 combined final；补 Space / Music / ToolUI 差异化 | `ProceduralMaskFillFinalSupplementPack.asset` |
| Combined final pack | 正式 Shape 评审包 | 147 levels | 当前最接近正式版本的 Shape 汇总包 | `ProceduralMaskFillFinalWithSupplementPack.asset` |
| `ShapeReadablePreview` | 早期符号 / 可读性验证 | 9 masks | 历史实验池；可作为新手符号参考，但不是当前主力 | `ReadableShapePreviewPack.asset`, `EarlySymbolPreviewPack.asset` |
| Original star mask | 原始 Star 验证 | 1 mask / 1 level | Star mask 跑通基准；保留作对照 | `OriginalStarMaskPreviewPack.asset` |
| `ShapeAnimalPreview` | 动物早期验证 | 6 masks | 历史动物池；部分方向/识别问题已被后续方案替代 | `AnimalShapePreviewPack.asset` |
| `ShapeAnimalBestPreview` | 动物精选验证 | 3 masks | 历史精选动物池；可作为后续动物主题参考 | `AnimalBestPreviewPack.asset` |
| `ShapeTallFitPreview` | 竖屏适配验证 | 4 masks | 早期尺寸/画布适配实验 | `tall_fit_shape_preview_pack_report.txt` |
| `ShapeExperiment` | 第一批手工形状实验 | 30 masks | 早期探索池；用于复盘题材和尺寸，不直接当正式池 | `shape_experiment_mask_catalog.csv` |

## Series: 新手专用

Pool: `ShapeEarlyPropPreview`

Pack: `Assets/ArrowMagic/SOData/Packs/ShapeExperiment/EarlyPropSatisfyingPack.asset`

Catalog: `shape_early_prop_mask_catalog.csv`

Purpose: very early levels, roughly first 20 levels, low-chain and satisfying clear. This is a separate beginner-only series and should not be mixed into the main Shape final pack unless explicitly approved.

| Order | Series | Mask | Size | Theme | Latest Chains | Notes |
|---:|---|---|---|---|---:|---|
| 1 | 新手专用 | `EarlyProp01CrystalBall` | 20x24 | Magic / alchemy | 30 | Accepted for now; low-chain priority |
| 2 | 新手专用 | `EarlyProp02Eraser` | 26x18 | Object / tools-items | 32 | Visual still needs review before final release; edge/seam readability is weak |
| 3 | 新手专用 | `EarlyProp03Ruler` | 22x22 | Object / tools-items | 32 | Accepted for now |
| 4 | 新手专用 | `EarlyProp04MagicWand` | 24x24 | Magic / academy | 37 | Visual still needs review before final release; star tip needs stronger five-point shape |

Latest selected outputs are recorded in `early_prop_satisfying_pack_report.txt`.

## Series: Main Shape Base

Pool: `ShapeProceduralVerticalPreview`

Catalog: `shape_procedural_vertical_preview_mask_catalog.csv`

Notes:
- Raw catalog rows: 532.
- De-duplicated unique base set: 150.
- First production trim kept 133 and removed 17 weaker/samey entries.
- This is the main Shape production base.

### Kept Theme Counts After Quality Trim

| Series | Kept | Role |
|---|---:|---|
| Art | 3 | portrait / palette style shapes |
| Character | 7 | readable character silhouettes |
| Landmark | 22 | architecture and monument silhouettes |
| Magic | 27 | fantasy props, characters, and adventure symbols |
| Music | 3 | original music silhouettes before supplement |
| Nature | 14 | plants, creatures, and natural forms |
| Object | 12 | everyday props and tools |
| Ocean | 23 | undersea and nautical shapes |
| Space | 4 | original space / sci-fi seed shapes |
| Symbol | 10 | early-readable symbols, letters, digits |
| Vehicle | 8 | transport silhouettes |

Trimmed from the 150 unique base:
`MainOceanCoralTall`, `MainMagicWandTall`, `MainLeaf01Tall`, `MainLeaf02Tall`, `MainLeaf05Tall`, `MainLeaf07Tall`, `MainLeaf10Tall`, `TransitionWideLandmark02Tall`, `TransitionWideLandmark04Tall`, `TransitionWideLandmark13Tall`, `TransitionWideLandmark14Tall`, `TransitionWideLandmark15Tall`, `TransitionWideLandmark18Tall`, `BigMegaMonument04Tall`, `BigMegaMonument17Tall`, `BigCarrier05Tall`, `BigGrove05Tall`.

### Main Shape Theme Families

These are the main unique-series families used to organize the larger raw catalog.

| Series | Representative / Explicit Masks | Generated Families |
|---|---|---|
| Art | `MainClassicPortraitTall`, `MainPainterBustTall`, `MainPaintPaletteTall` | none |
| Character | `MainSnowmanTall`, `MainBeanMascotTall`, `MainRaisedHandMascotTall`, `MainOwlTall`, `MainPenguinTall`, `MainBunnyTall`, `MainFoxTall` | `MainAnimalSide01-18Tall`, `BigCreature01-18Tall` in raw pool |
| Landmark | `MainLighthouseTall`, `MainTowerTall`, `MainIronTowerTall`, `MainLibertyStatueTall`, `MainTriumphArchTall`, `MainLeaningTowerTall`, `MainPyramidTall`, `MainClockTowerTall`, `MainWindmillTall`, `MainTempleColumnsTall`, `TransitionPagodaTall`, `TransitionDomePalaceTall`, `TransitionBridgeTowerTall`, `TransitionObeliskTall`, `BigFortressTall` | `TransitionWideLandmark01-34Tall`, `BigMegaMonument01-24Tall` in raw pool |
| Magic | `MainShieldTall`, `MainKeyTall`, `MainCandleTall`, `MainGemTall`, `MainWizardMascotTall`, `MainKnightMascotTall`, `MainTreasureChestTall`, `MainBookTall`, `TransitionCastleGateTall`, `BigCathedralTall`, `MainMagicHatTall`, `MainMagicScrollTall`, `MainMagicQuillTall`, `MainMagicRuneStoneTall`, `MainMagicBatTall`, `MainMagicBlackCatTall`, `MainMagicDragonEggTall`, `TransitionMagicOpenBookTall`, `TransitionMagicCrystalBallTall`, `TransitionMagicCauldronTall`, `TransitionMagicBroomTall`, `TransitionMagicPortalTall`, `TransitionMagicKnightHelmetTall`, `BigMagicDragonClawTall`, `BigMagicTowerDoorTall`, `MainDistinctGhostTall`, `TransitionDistinctChessKnightTall` | `MainMagicWandTall` was trimmed from base quality pass |
| Music | `MainGuitarTall`, `MainMicrophoneTall`, `MainDistinctMusicNoteTall` | supplemented later |
| Nature | `MainPineTreeTall`, `MainCactusTall`, `MainMushroomTall`, `MainDistinctSnakeTall`, `MainDistinctSnailTall`, `TransitionDistinctScorpionTall`, `TransitionDistinctButterflyTall` | `MainLeaf01-18Tall`, `MainFlame01-16Tall`, `MainNature01-18Tall`, `TransitionWildform01-24Tall`, `BigGrove01-20Tall` in raw pool; several leaf/grove variants trimmed |
| Object | `MainTrophyTall`, `MainBellTall`, `MainHouseTall`, `MainCupTall`, `MainBackpackTall`, `MainHourglassTall`, `MainDistinctMagnetTall`, `MainDistinctBootTall`, `MainDistinctMittenTall`, `MainDistinctPlugTall`, `MainDistinctToothTall`, `TransitionDistinctScissorsTall` | `MainTool01-18Tall`, `MainObject01-22Tall`, `TransitionRelic01-28Tall`, `BigRelic01-20Tall` in raw pool |
| Ocean | `MainWhaleTall`, `MainAnchorTall`, `MainWaterDropTall`, `TransitionSailboatTall`, `MainOceanShellTall`, `MainOceanStarfishTall`, `MainOceanLifeBuoyTall`, `MainOceanFlippersTall`, `MainOceanSeaweedTall`, `MainOceanPufferfishTall`, `MainOceanSharkFinTall`, `MainOceanDriftBottleTall`, `TransitionOceanDivingMaskTall`, `TransitionOceanOxygenTankTall`, `TransitionOceanDivingHelmetTall`, `TransitionOceanSeaTurtleTall`, `TransitionOceanLobsterTall`, `TransitionOceanPearlClamTall`, `BigOceanShipwreckTall`, `MainDistinctOctopusTall`, `MainDistinctJellyfishTall`, `TransitionDistinctSeahorseTall`, `TransitionDistinctCrabTall` | `MainOceanCoralTall` was trimmed |
| Space | `MainRocketTall`, `MainRobotMascotTall`, `MainAstronautMascotTall`, `BigGiantRobotTall` | expanded by final supplement |
| Symbol | `MainHeartTall`, `MainLightningTall`, `MainStarTall`, `MainDistinctCrescentTall`, `MainDistinctQuestionTall`, `MainDistinctPuzzleTall`, `MainDistinctLetterMTall`, `MainDistinctLetterXTall`, `MainDistinctDigit2Tall`, `MainDistinctDigit5Tall` | `MainSymbol01-20Tall`, `MainEmblem01-16Tall` in raw pool |
| Vehicle | `MainTrainFrontTall`, `MainHotAirBalloonTall`, `TransitionAirshipTall` | `MainVehicle01-17Tall`, `TransitionCarrier01-24Tall`, `BigCarrier01-18Tall` in raw pool |

## Series: Final Supplement

Pool: `ShapeFinalSupplementPreview`

Pack: `ProceduralMaskFillFinalSupplementPack.asset`

Purpose: add differentiated silhouettes without re-running the 150-mask base set.

| Series | Masks |
|---|---|
| Space / sci-fi | `MainSpaceSaturnIconTall`, `MainSpaceUfoIconTall`, `MainSpaceSatelliteIconTall`, `MainSpaceCometIconTall` |
| Music / stage | `MainMusicHeadphonesTall`, `MainMusicVinylRecordTall`, `MainMusicSpeakerTall`, `MainMusicDrumTall` |
| Tool UI / digital tools | `MainToolGearTall`, `MainToolBatteryTall`, `MainToolWrenchTall`, `MainUiWifiTall`, `MainUiCameraTall`, `MainUiChatBubbleTall` |

Selected style counts in supplement run: `LongChain=5`, `Onion=9`.

## Series: Early Symbols

Pool: `ShapeReadablePreview` plus original `Mask_19x19-Star`.

Pack: `EarlySymbolPreviewPack.asset`

Purpose: old early symbol experiment. Kept as reference, not current beginner-prop series.

| Series | Masks | Latest Generated Chains |
|---|---|---|
| Early Symbol | `Mask_19x19-Star`, `PlusBold`, `XBold`, `LightningSmall` | 25, 22, 43, 30 |
| Readable extra masks | `HeartBold`, `GemBold`, `StarBoldLarge`, `HouseBold`, `RocketBold`, `CarBold` | catalog only / not in the 4-level early symbol pack |

## Series: Animal Experiments

These are historical readability experiments. Do not treat them as current final Shape production unless promoted later.

| Pool | Masks |
|---|---|
| `ShapeAnimalPreview` | `DogSitSide`, `CatSitSide`, `BunnyTall`, `DuckSide`, `FishSideLarge`, `ElephantSide` |
| `ShapeAnimalBestPreview` | `WhaleBoldSide`, `TurtleBoldSide`, `SnailBoldSide` |

## Series: Tall Fit / First Shape Experiments

These are earlier sizing and recognizability checks.

| Pool | Masks |
|---|---|
| `ShapeTallFitPreview` | `RocketMini`, `ShieldTall`, `BottleMini`, `TorchTall` |
| `ShapeExperiment` | `CatHead`, `Fish`, `Tree`, `Cloud`, `Crown`, `Shield`, `Balloon`, `Duck`, `Bunny`, `Bear`, `Turtle`, `Flower`, `Mushroom`, `Umbrella`, `Gift`, `Boat`, `Plane`, `MusicNote`, `Castle`, `Bus`, `TrainFront`, `Ufo`, `Sun`, `Snowman`, `Lightning`, `Cup`, `Lighthouse`, `BigCastle`, `Whale`, `TallFlower` |

## Recommendation

1. Treat `ProceduralMaskFillFinalWithSupplementPack.asset` as the current formal Shape candidate review pack.
2. Treat `EarlyPropSatisfyingPack.asset` as a separate beginner-only preview pack.
3. Keep `ShapeProceduralVerticalPreview` as the large source library, but use the quality-trim report as the production gate.
4. Keep animal/readable/tall-fit/first-shape pools as historical references unless a mask is explicitly promoted into the main or beginner series.
5. For future production, prefer adding masks into a named theme series first, then generating 2-3 style candidates per mask, then selecting one final per mask.
