#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace PixelBug.ArrowMagic.EditorTools
{
    public static class ShapeExperimentMaskBuilder
    {
        private const string MaskFolder = "Assets/ArrowMagic/Masks/ShapeExperiment";
        private const string ReadableMaskFolder = "Assets/ArrowMagic/Masks/ShapeReadablePreview";
        private const string AnimalMaskFolder = "Assets/ArrowMagic/Masks/ShapeAnimalPreview";
        private const string AnimalBestMaskFolder = "Assets/ArrowMagic/Masks/ShapeAnimalBestPreview";
        private const string TallFitMaskFolder = "Assets/ArrowMagic/Masks/ShapeTallFitPreview";
        private const string ReportFolder = "Assets/ArrowMagic/SOData/Reports/ShapeExperiment";
        private const string CatalogPath = ReportFolder + "/shape_experiment_mask_catalog.csv";
        private const string ContactSheetPath = ReportFolder + "/shape_experiment_mask_contact_sheet.png";
        private const string ReadableCatalogPath = ReportFolder + "/shape_readable_preview_mask_catalog.csv";
        private const string ReadableContactSheetPath = ReportFolder + "/shape_readable_preview_mask_contact_sheet.png";
        private const string AnimalCatalogPath = ReportFolder + "/shape_animal_preview_mask_catalog.csv";
        private const string AnimalContactSheetPath = ReportFolder + "/shape_animal_preview_mask_contact_sheet.png";
        private const string AnimalBestCatalogPath = ReportFolder + "/shape_animal_best_preview_mask_catalog.csv";
        private const string AnimalBestContactSheetPath = ReportFolder + "/shape_animal_best_preview_mask_contact_sheet.png";
        private const string TallFitCatalogPath = ReportFolder + "/shape_tall_fit_preview_mask_catalog.csv";
        private const string TallFitContactSheetPath = ReportFolder + "/shape_tall_fit_preview_mask_contact_sheet.png";

        private static readonly Color32 Clear = new Color32(0, 0, 0, 0);
        private static readonly Color32 Fill = new Color32(255, 255, 255, 255);

        private enum ShapeStage
        {
            Early,
            Mid,
            MidLate,
            Late
        }

        private sealed class ShapeMaskSpec
        {
            public string Name;
            public int Width;
            public int Height;
            public ShapeStage Stage;
            public Action<bool[], int, int> Draw;
            public string Notes;

            public string AssetPath => $"{MaskFolder}/MaskShape_{Width}x{Height}-{Name}.png";
        }

        [MenuItem("Tools/ArrowMagic/Shape Experiment/Create Shape Experiment Masks")]
        public static void CreateShapeExperimentMasks()
        {
            EnsureFolder(MaskFolder);
            EnsureFolder(ReportFolder);

            var specs = BuildShapeSpecs();
            var rows = new List<string>
            {
                "index,stage,name,width,height,area,coverage,path,notes"
            };
            var previews = new List<Texture2D>(specs.Count);

            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                var cells = new bool[Mathf.Max(1, spec.Width * spec.Height)];
                spec.Draw(cells, spec.Width, spec.Height);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                EnsureMinimumCoverage(cells, spec.Width, spec.Height, 0.34f, 4);

                int area = CountCells(cells);
                float coverage = area / (float)Mathf.Max(1, spec.Width * spec.Height);
                WriteMask(spec.AssetPath, spec.Width, spec.Height, cells);
                previews.Add(BuildPreview(spec.Width, spec.Height, cells));

                rows.Add(string.Join(",",
                    (i + 1).ToString(CultureInfo.InvariantCulture),
                    spec.Stage.ToString(),
                    EscapeCsv(spec.Name),
                    spec.Width.ToString(CultureInfo.InvariantCulture),
                    spec.Height.ToString(CultureInfo.InvariantCulture),
                    area.ToString(CultureInfo.InvariantCulture),
                    coverage.ToString("0.000", CultureInfo.InvariantCulture),
                    EscapeCsv(spec.AssetPath),
                    EscapeCsv(spec.Notes)));
            }

            File.WriteAllText(ToFullPath(CatalogPath), string.Join("\n", rows), Encoding.UTF8);
            WriteContactSheet(previews, specs, ContactSheetPath);

            for (int i = 0; i < specs.Count; i++)
                AssetDatabase.ImportAsset(specs[i].AssetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(CatalogPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(ContactSheetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();

            for (int i = 0; i < previews.Count; i++)
                UnityEngine.Object.DestroyImmediate(previews[i]);

            Debug.Log($"[ShapeExperiment] Created {specs.Count} solid shape masks. Catalog={CatalogPath}, sheet={ContactSheetPath}");
        }

        [MenuItem("Tools/ArrowMagic/Shape Experiment/Create Readable Shape Preview Masks")]
        public static void CreateReadableShapePreviewMasks()
        {
            EnsureFolder(ReadableMaskFolder);
            EnsureFolder(ReportFolder);

            var specs = BuildReadableShapeSpecs();
            var rows = new List<string>
            {
                "index,stage,name,width,height,area,coverage,path,notes"
            };
            var previews = new List<Texture2D>(specs.Count);

            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                var cells = new bool[Mathf.Max(1, spec.Width * spec.Height)];
                spec.Draw(cells, spec.Width, spec.Height);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                EnsureMinimumCoverage(cells, spec.Width, spec.Height, 0.40f, 4);

                int area = CountCells(cells);
                float coverage = area / (float)Mathf.Max(1, spec.Width * spec.Height);
                string assetPath = ReadableMaskPath(spec);
                WriteMask(assetPath, spec.Width, spec.Height, cells);
                previews.Add(BuildPreview(spec.Width, spec.Height, cells));

                rows.Add(string.Join(",",
                    (i + 1).ToString(CultureInfo.InvariantCulture),
                    spec.Stage.ToString(),
                    EscapeCsv(spec.Name),
                    spec.Width.ToString(CultureInfo.InvariantCulture),
                    spec.Height.ToString(CultureInfo.InvariantCulture),
                    area.ToString(CultureInfo.InvariantCulture),
                    coverage.ToString("0.000", CultureInfo.InvariantCulture),
                    EscapeCsv(assetPath),
                    EscapeCsv(spec.Notes)));
            }

            File.WriteAllText(ToFullPath(ReadableCatalogPath), string.Join("\n", rows), Encoding.UTF8);
            WriteContactSheet(previews, specs, ReadableContactSheetPath);

            for (int i = 0; i < specs.Count; i++)
                AssetDatabase.ImportAsset(ReadableMaskPath(specs[i]), ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(ReadableCatalogPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(ReadableContactSheetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();

            for (int i = 0; i < previews.Count; i++)
                UnityEngine.Object.DestroyImmediate(previews[i]);

            Debug.Log($"[ShapeExperiment] Created {specs.Count} readable preview masks. Catalog={ReadableCatalogPath}, sheet={ReadableContactSheetPath}");
        }

        [MenuItem("Tools/ArrowMagic/Shape Experiment/Create Animal Shape Preview Masks")]
        public static void CreateAnimalShapePreviewMasks()
        {
            EnsureFolder(AnimalMaskFolder);
            EnsureFolder(ReportFolder);

            var specs = BuildAnimalShapeSpecs();
            var rows = new List<string>
            {
                "index,stage,name,width,height,area,coverage,path,notes"
            };
            var previews = new List<Texture2D>(specs.Count);

            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                var cells = new bool[Mathf.Max(1, spec.Width * spec.Height)];
                spec.Draw(cells, spec.Width, spec.Height);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                FitContentToCanvas(cells, spec.Width, spec.Height, 1);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                EnsureMinimumCoverage(cells, spec.Width, spec.Height, 0.48f, 4);

                int area = CountCells(cells);
                float coverage = area / (float)Mathf.Max(1, spec.Width * spec.Height);
                string assetPath = AnimalMaskPath(spec);
                WriteMask(assetPath, spec.Width, spec.Height, cells);
                previews.Add(BuildPreview(spec.Width, spec.Height, cells));

                rows.Add(string.Join(",",
                    (i + 1).ToString(CultureInfo.InvariantCulture),
                    spec.Stage.ToString(),
                    EscapeCsv(spec.Name),
                    spec.Width.ToString(CultureInfo.InvariantCulture),
                    spec.Height.ToString(CultureInfo.InvariantCulture),
                    area.ToString(CultureInfo.InvariantCulture),
                    coverage.ToString("0.000", CultureInfo.InvariantCulture),
                    EscapeCsv(assetPath),
                    EscapeCsv(spec.Notes)));
            }

            File.WriteAllText(ToFullPath(AnimalCatalogPath), string.Join("\n", rows), Encoding.UTF8);
            WriteContactSheet(previews, specs, AnimalContactSheetPath);

            for (int i = 0; i < specs.Count; i++)
                AssetDatabase.ImportAsset(AnimalMaskPath(specs[i]), ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(AnimalCatalogPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(AnimalContactSheetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();

            for (int i = 0; i < previews.Count; i++)
                UnityEngine.Object.DestroyImmediate(previews[i]);

            Debug.Log($"[ShapeExperiment] Created {specs.Count} animal preview masks. Catalog={AnimalCatalogPath}, sheet={AnimalContactSheetPath}");
        }

        [MenuItem("Tools/ArrowMagic/Shape Experiment/Create Animal Best Preview Masks")]
        public static void CreateAnimalBestPreviewMasks()
        {
            EnsureFolder(AnimalBestMaskFolder);
            EnsureFolder(ReportFolder);

            var specs = BuildAnimalBestShapeSpecs();
            var rows = new List<string>
            {
                "index,stage,name,width,height,area,coverage,path,notes"
            };
            var previews = new List<Texture2D>(specs.Count);
            var assetPaths = new List<string>(specs.Count);

            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                var cells = new bool[Mathf.Max(1, spec.Width * spec.Height)];
                spec.Draw(cells, spec.Width, spec.Height);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                EnsureMinimumCoverage(cells, spec.Width, spec.Height, 0.46f, 2);
                CropContentToCanvas(cells, spec.Width, spec.Height, 1, out cells, out int outputWidth, out int outputHeight);

                int area = CountCells(cells);
                float coverage = area / (float)Mathf.Max(1, outputWidth * outputHeight);
                string assetPath = AnimalBestMaskPath(spec, outputWidth, outputHeight);
                assetPaths.Add(assetPath);
                WriteMask(assetPath, outputWidth, outputHeight, cells);
                previews.Add(BuildPreview(outputWidth, outputHeight, cells));

                rows.Add(string.Join(",",
                    (i + 1).ToString(CultureInfo.InvariantCulture),
                    spec.Stage.ToString(),
                    EscapeCsv(spec.Name),
                    outputWidth.ToString(CultureInfo.InvariantCulture),
                    outputHeight.ToString(CultureInfo.InvariantCulture),
                    area.ToString(CultureInfo.InvariantCulture),
                    coverage.ToString("0.000", CultureInfo.InvariantCulture),
                    EscapeCsv(assetPath),
                    EscapeCsv($"{spec.Notes}; sourceCanvas={spec.Width}x{spec.Height}; croppedWithPadding=1")));
            }

            File.WriteAllText(ToFullPath(AnimalBestCatalogPath), string.Join("\n", rows), Encoding.UTF8);
            WriteContactSheet(previews, specs, AnimalBestContactSheetPath);

            for (int i = 0; i < assetPaths.Count; i++)
                AssetDatabase.ImportAsset(assetPaths[i], ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(AnimalBestCatalogPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(AnimalBestContactSheetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();

            for (int i = 0; i < previews.Count; i++)
                UnityEngine.Object.DestroyImmediate(previews[i]);

            Debug.Log($"[ShapeExperiment] Created {specs.Count} animal best preview masks. Catalog={AnimalBestCatalogPath}, sheet={AnimalBestContactSheetPath}");
        }

        [MenuItem("Tools/ArrowMagic/Shape Experiment/Create Tall Fit Shape Preview Masks")]
        public static void CreateTallFitShapePreviewMasks()
        {
            EnsureFolder(TallFitMaskFolder);
            EnsureFolder(ReportFolder);

            var specs = BuildTallFitShapeSpecs();
            var rows = new List<string>
            {
                "index,stage,name,width,height,area,coverage,path,notes"
            };
            var previews = new List<Texture2D>(specs.Count);

            for (int i = 0; i < specs.Count; i++)
            {
                var spec = specs[i];
                var cells = new bool[Mathf.Max(1, spec.Width * spec.Height)];
                spec.Draw(cells, spec.Width, spec.Height);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                FitContentToCanvas(cells, spec.Width, spec.Height, 1);
                CleanTinyArtifacts(cells, spec.Width, spec.Height);
                EnsureMinimumCoverage(cells, spec.Width, spec.Height, 0.44f, 2);

                int area = CountCells(cells);
                float coverage = area / (float)Mathf.Max(1, spec.Width * spec.Height);
                string assetPath = TallFitMaskPath(spec);
                WriteMask(assetPath, spec.Width, spec.Height, cells);
                previews.Add(BuildPreview(spec.Width, spec.Height, cells));

                rows.Add(string.Join(",",
                    (i + 1).ToString(CultureInfo.InvariantCulture),
                    spec.Stage.ToString(),
                    EscapeCsv(spec.Name),
                    spec.Width.ToString(CultureInfo.InvariantCulture),
                    spec.Height.ToString(CultureInfo.InvariantCulture),
                    area.ToString(CultureInfo.InvariantCulture),
                    coverage.ToString("0.000", CultureInfo.InvariantCulture),
                    EscapeCsv(assetPath),
                    EscapeCsv($"{spec.Notes}; fitCanvasPadding=1")));
            }

            File.WriteAllText(ToFullPath(TallFitCatalogPath), string.Join("\n", rows), Encoding.UTF8);
            WriteContactSheet(previews, specs, TallFitContactSheetPath);

            for (int i = 0; i < specs.Count; i++)
                AssetDatabase.ImportAsset(TallFitMaskPath(specs[i]), ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(TallFitCatalogPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.ImportAsset(TallFitContactSheetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();

            for (int i = 0; i < previews.Count; i++)
                UnityEngine.Object.DestroyImmediate(previews[i]);

            Debug.Log($"[ShapeExperiment] Created {specs.Count} tall-fit preview masks. Catalog={TallFitCatalogPath}, sheet={TallFitContactSheetPath}");
        }

        private static List<ShapeMaskSpec> BuildShapeSpecs()
        {
            return new List<ShapeMaskSpec>
            {
                Spec(ShapeStage.Early, "CatHead", 16, 16, DrawCatHead, "early; solid silhouette"),
                Spec(ShapeStage.Early, "Fish", 16, 16, DrawFish, "early; simple animal"),
                Spec(ShapeStage.Early, "Tree", 16, 16, DrawTree, "early; natural icon"),
                Spec(ShapeStage.Early, "Cloud", 16, 16, DrawCloud, "early; soft outline"),
                Spec(ShapeStage.Early, "Crown", 16, 16, DrawCrown, "early; readable object"),
                Spec(ShapeStage.Early, "Shield", 16, 16, DrawShield, "early; solid emblem"),
                Spec(ShapeStage.Early, "Balloon", 14, 20, DrawBalloon, "early; vertical small mask"),
                Spec(ShapeStage.Early, "Duck", 18, 18, DrawDuck, "early; simple animal"),

                Spec(ShapeStage.Mid, "Bunny", 22, 24, DrawBunny, "mid; bigger animal"),
                Spec(ShapeStage.Mid, "Bear", 22, 20, DrawBear, "mid; rounded animal"),
                Spec(ShapeStage.Mid, "Turtle", 24, 20, DrawTurtle, "mid; horizontal animal"),
                Spec(ShapeStage.Mid, "Flower", 24, 24, DrawFlower, "mid; no hollow center"),
                Spec(ShapeStage.Mid, "Mushroom", 22, 24, DrawMushroom, "mid; simple icon"),
                Spec(ShapeStage.Mid, "Umbrella", 24, 20, DrawUmbrella, "mid; thick handle"),
                Spec(ShapeStage.Mid, "Gift", 22, 22, DrawGift, "mid; solid box and bow"),
                Spec(ShapeStage.Mid, "Boat", 26, 20, DrawBoat, "mid; thick hull and sail"),
                Spec(ShapeStage.Mid, "Plane", 28, 20, DrawPlane, "mid; compact horizontal"),
                Spec(ShapeStage.Mid, "MusicNote", 20, 24, DrawMusicNote, "mid; solid note"),

                Spec(ShapeStage.MidLate, "Castle", 32, 36, DrawCastle, "mid-late; blocky silhouette"),
                Spec(ShapeStage.MidLate, "Bus", 36, 28, DrawBus, "mid-late; solid vehicle"),
                Spec(ShapeStage.MidLate, "TrainFront", 30, 36, DrawTrainFront, "mid-late; vertical vehicle"),
                Spec(ShapeStage.MidLate, "Ufo", 36, 28, DrawUfo, "mid-late; simple saucer"),
                Spec(ShapeStage.MidLate, "Sun", 32, 32, DrawSun, "mid-late; filled rays"),
                Spec(ShapeStage.MidLate, "Snowman", 28, 36, DrawSnowman, "mid-late; stacked circles"),
                Spec(ShapeStage.MidLate, "Lightning", 24, 36, DrawLightning, "mid-late; angular solid"),
                Spec(ShapeStage.MidLate, "Cup", 28, 30, DrawCup, "mid-late; solid cup silhouette"),

                Spec(ShapeStage.Late, "Lighthouse", 44, 52, DrawLighthouse, "late; tall simple landmark"),
                Spec(ShapeStage.Late, "BigCastle", 48, 52, DrawBigCastle, "late; large blocky landmark"),
                Spec(ShapeStage.Late, "Whale", 52, 40, DrawWhale, "late; large animal silhouette"),
                Spec(ShapeStage.Late, "TallFlower", 48, 60, DrawTallFlower, "late; large natural icon")
            };
        }

        private static List<ShapeMaskSpec> BuildReadableShapeSpecs()
        {
            return new List<ShapeMaskSpec>
            {
                Spec(ShapeStage.Early, "PlusBold", 19, 19, DrawPlusBold, "early; super symbol; block plus"),
                Spec(ShapeStage.Early, "XBold", 21, 21, DrawXBold, "early; super symbol; thick x"),
                Spec(ShapeStage.Early, "LightningSmall", 20, 22, DrawLightningSmall, "early; super symbol; block lightning"),
                Spec(ShapeStage.Early, "HeartBold", 20, 20, DrawHeartBold, "early; around 19; strong readable silhouette"),
                Spec(ShapeStage.Early, "GemBold", 21, 21, DrawGemBold, "early; around 19; angular readable silhouette"),
                Spec(ShapeStage.Mid, "StarBoldLarge", 28, 28, DrawStarBold, "recommended; same motif at safer size"),
                Spec(ShapeStage.Mid, "HouseBold", 28, 30, DrawHouseBold, "recommended; simple object silhouette"),
                Spec(ShapeStage.Mid, "RocketBold", 30, 36, DrawRocketBold, "recommended; vertical shape silhouette"),
                Spec(ShapeStage.Mid, "CarBold", 36, 28, DrawCarBold, "recommended; horizontal shape silhouette")
            };
        }

        private static List<ShapeMaskSpec> BuildAnimalShapeSpecs()
        {
            return new List<ShapeMaskSpec>
            {
                Spec(ShapeStage.Mid, "DogSitSide", 42, 42, DrawDogSitSide, "animal; side-view sitting dog; strong snout, ear, tail"),
                Spec(ShapeStage.Mid, "CatSitSide", 40, 44, DrawCatSitSide, "animal; side-view sitting cat; strong tail and ears"),
                Spec(ShapeStage.Mid, "BunnyTall", 34, 46, DrawBunnyTall, "animal; front bunny; ears face up in game"),
                Spec(ShapeStage.Mid, "DuckSide", 42, 34, DrawDuckSideLarge, "animal; side-view duck; beak and tail silhouette"),
                Spec(ShapeStage.Mid, "FishSideLarge", 44, 30, DrawFishSideLarge, "animal; side-view fish; strong tail and fins"),
                Spec(ShapeStage.Mid, "ElephantSide", 44, 38, DrawElephantSide, "animal; side-view elephant; trunk and ear silhouette")
            };
        }

        private static List<ShapeMaskSpec> BuildAnimalBestShapeSpecs()
        {
            return new List<ShapeMaskSpec>
            {
                Spec(ShapeStage.Mid, "WhaleBoldSide", 32, 28, DrawWhaleBoldSide, "animal best; compact whale; broad body, tail, fin, no face details"),
                Spec(ShapeStage.Mid, "TurtleBoldSide", 32, 28, DrawTurtleBoldSide, "animal best; compact turtle; shell, head, chunky feet"),
                Spec(ShapeStage.Mid, "SnailBoldSide", 30, 28, DrawSnailBoldSide, "animal best; compact snail; shell and body, very low detail")
            };
        }

        private static List<ShapeMaskSpec> BuildTallFitShapeSpecs()
        {
            return new List<ShapeMaskSpec>
            {
                Spec(ShapeStage.Mid, "RocketMini", 22, 30, DrawRocketMini, "tall-fit v3; smaller rocket icon; low-solid crop-friendly silhouette"),
                Spec(ShapeStage.Mid, "ShieldTall", 22, 30, DrawShieldTall, "tall-fit v3; tall shield badge; simple readable outline"),
                Spec(ShapeStage.Mid, "BottleMini", 20, 30, DrawBottleMini, "tall-fit v3; small bottle/vase silhouette; narrow portrait canvas"),
                Spec(ShapeStage.Mid, "TorchTall", 22, 32, DrawTorchTall, "tall-fit v3; torch/candle icon; flame plus chunky stem")
            };
        }

        private static string ReadableMaskPath(ShapeMaskSpec spec)
        {
            return $"{ReadableMaskFolder}/MaskReadable_{spec.Width}x{spec.Height}-{spec.Name}.png";
        }

        private static string AnimalMaskPath(ShapeMaskSpec spec)
        {
            return $"{AnimalMaskFolder}/MaskAnimal_{spec.Width}x{spec.Height}-{spec.Name}.png";
        }

        private static string AnimalBestMaskPath(ShapeMaskSpec spec)
        {
            return $"{AnimalBestMaskFolder}/MaskAnimalBest_{spec.Width}x{spec.Height}-{spec.Name}.png";
        }

        private static string AnimalBestMaskPath(ShapeMaskSpec spec, int width, int height)
        {
            return $"{AnimalBestMaskFolder}/MaskAnimalBest_{width}x{height}-{spec.Name}.png";
        }

        private static string TallFitMaskPath(ShapeMaskSpec spec)
        {
            return $"{TallFitMaskFolder}/MaskTallFit_{spec.Width}x{spec.Height}-{spec.Name}.png";
        }

        private static ShapeMaskSpec Spec(ShapeStage stage, string name, int width, int height, Action<bool[], int, int> draw, string notes)
        {
            return new ShapeMaskSpec
            {
                Stage = stage,
                Name = name,
                Width = width,
                Height = height,
                Draw = draw,
                Notes = notes
            };
        }

        private static void DrawTotemTall(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.34f, 0.02f),
                new Vector2(0.66f, 0.02f),
                new Vector2(0.78f, 0.12f),
                new Vector2(0.78f, 0.23f),
                new Vector2(0.91f, 0.30f),
                new Vector2(0.91f, 0.44f),
                new Vector2(0.78f, 0.50f),
                new Vector2(0.78f, 0.66f),
                new Vector2(0.88f, 0.75f),
                new Vector2(0.88f, 0.90f),
                new Vector2(0.66f, 0.98f),
                new Vector2(0.34f, 0.98f),
                new Vector2(0.12f, 0.90f),
                new Vector2(0.12f, 0.75f),
                new Vector2(0.22f, 0.66f),
                new Vector2(0.22f, 0.50f),
                new Vector2(0.09f, 0.44f),
                new Vector2(0.09f, 0.30f),
                new Vector2(0.22f, 0.23f),
                new Vector2(0.22f, 0.12f)
            });
            FillRect(c, w, h, 0.24f, 0.04f, 0.76f, 0.17f);
            FillRect(c, w, h, 0.22f, 0.82f, 0.78f, 0.94f);
        }

        private static void DrawRocketPillar(bool[] c, int w, int h)
        {
            FillTriangle(c, w, h, 0.50f, 0.99f, 0.20f, 0.76f, 0.80f, 0.76f);
            FillRect(c, w, h, 0.23f, 0.18f, 0.77f, 0.78f);
            FillEllipse(c, w, h, 0.50f, 0.76f, 0.27f, 0.08f);
            FillTriangle(c, w, h, 0.24f, 0.30f, 0.04f, 0.03f, 0.38f, 0.18f);
            FillTriangle(c, w, h, 0.76f, 0.30f, 0.96f, 0.03f, 0.62f, 0.18f);
            FillTriangle(c, w, h, 0.40f, 0.18f, 0.50f, 0.02f, 0.60f, 0.18f);
            FillRect(c, w, h, 0.34f, 0.04f, 0.66f, 0.22f);
        }

        private static void DrawRocketMini(bool[] c, int w, int h)
        {
            FillTriangle(c, w, h, 0.30f, 0.72f, 0.50f, 0.99f, 0.70f, 0.72f);
            FillRect(c, w, h, 0.30f, 0.22f, 0.70f, 0.74f);
            FillEllipse(c, w, h, 0.50f, 0.72f, 0.21f, 0.06f);
            FillTriangle(c, w, h, 0.30f, 0.30f, 0.06f, 0.03f, 0.42f, 0.22f);
            FillTriangle(c, w, h, 0.70f, 0.30f, 0.94f, 0.03f, 0.58f, 0.22f);
            FillTriangle(c, w, h, 0.40f, 0.22f, 0.50f, 0.02f, 0.60f, 0.22f);
        }

        private static void DrawShieldTall(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.14f, 0.86f),
                new Vector2(0.86f, 0.86f),
                new Vector2(0.78f, 0.42f),
                new Vector2(0.50f, 0.02f),
                new Vector2(0.22f, 0.42f)
            });
            FillRect(c, w, h, 0.22f, 0.76f, 0.78f, 0.96f);
            FillRect(c, w, h, 0.34f, 0.28f, 0.66f, 0.72f);
        }

        private static void DrawBottleMini(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.38f, 0.70f, 0.62f, 0.94f);
            FillRect(c, w, h, 0.28f, 0.86f, 0.72f, 0.98f);
            FillEllipse(c, w, h, 0.50f, 0.42f, 0.30f, 0.27f);
            FillRect(c, w, h, 0.24f, 0.18f, 0.76f, 0.48f);
            FillEllipse(c, w, h, 0.50f, 0.18f, 0.25f, 0.10f);
            FillRect(c, w, h, 0.22f, 0.04f, 0.78f, 0.16f);
        }

        private static void DrawTorchTall(bool[] c, int w, int h)
        {
            FillTriangle(c, w, h, 0.50f, 0.99f, 0.26f, 0.66f, 0.74f, 0.66f);
            FillEllipse(c, w, h, 0.50f, 0.68f, 0.22f, 0.14f);
            FillRect(c, w, h, 0.30f, 0.56f, 0.70f, 0.68f);
            FillRect(c, w, h, 0.40f, 0.12f, 0.60f, 0.58f);
            FillRect(c, w, h, 0.24f, 0.04f, 0.76f, 0.14f);
            FillEllipse(c, w, h, 0.32f, 0.42f, 0.14f, 0.08f);
            FillEllipse(c, w, h, 0.68f, 0.32f, 0.14f, 0.08f);
        }

        private static void DrawLanternTall(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.22f, 0.88f, 0.78f, 0.98f);
            FillRect(c, w, h, 0.34f, 0.78f, 0.66f, 0.90f);
            FillEllipse(c, w, h, 0.50f, 0.52f, 0.39f, 0.30f);
            FillRect(c, w, h, 0.18f, 0.30f, 0.82f, 0.66f);
            FillEllipse(c, w, h, 0.50f, 0.28f, 0.34f, 0.12f);
            FillRect(c, w, h, 0.36f, 0.14f, 0.64f, 0.28f);
            FillRect(c, w, h, 0.14f, 0.02f, 0.86f, 0.14f);
        }

        private static void DrawLanternSlim(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.24f, 0.88f, 0.76f, 0.98f);
            FillRect(c, w, h, 0.36f, 0.78f, 0.64f, 0.90f);
            FillEllipse(c, w, h, 0.50f, 0.54f, 0.31f, 0.26f);
            FillRect(c, w, h, 0.24f, 0.34f, 0.76f, 0.64f);
            FillEllipse(c, w, h, 0.50f, 0.30f, 0.27f, 0.10f);
            FillRect(c, w, h, 0.38f, 0.15f, 0.62f, 0.30f);
            FillRect(c, w, h, 0.18f, 0.02f, 0.82f, 0.15f);
        }

        private static void DrawVaseTall(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.38f, 0.98f),
                new Vector2(0.62f, 0.98f),
                new Vector2(0.66f, 0.82f),
                new Vector2(0.80f, 0.70f),
                new Vector2(0.70f, 0.54f),
                new Vector2(0.58f, 0.45f),
                new Vector2(0.76f, 0.24f),
                new Vector2(0.72f, 0.08f),
                new Vector2(0.62f, 0.02f),
                new Vector2(0.38f, 0.02f),
                new Vector2(0.28f, 0.08f),
                new Vector2(0.24f, 0.24f),
                new Vector2(0.42f, 0.45f),
                new Vector2(0.30f, 0.54f),
                new Vector2(0.20f, 0.70f),
                new Vector2(0.34f, 0.82f)
            });
            FillRect(c, w, h, 0.30f, 0.88f, 0.70f, 0.98f);
            FillRect(c, w, h, 0.24f, 0.02f, 0.76f, 0.11f);
        }

        private static void DrawBeaconTall(bool[] c, int w, int h)
        {
            FillTriangle(c, w, h, 0.50f, 0.99f, 0.28f, 0.82f, 0.72f, 0.82f);
            FillRect(c, w, h, 0.18f, 0.74f, 0.82f, 0.84f);
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.36f, 0.74f),
                new Vector2(0.64f, 0.74f),
                new Vector2(0.70f, 0.16f),
                new Vector2(0.30f, 0.16f)
            });
            FillRect(c, w, h, 0.24f, 0.04f, 0.76f, 0.18f);
            FillRect(c, w, h, 0.12f, 0.02f, 0.88f, 0.08f);
            FillRect(c, w, h, 0.40f, 0.46f, 0.60f, 0.64f);
        }

        private static void DrawTrophyTall(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.12f, 0.80f),
                new Vector2(0.88f, 0.80f),
                new Vector2(0.74f, 0.48f),
                new Vector2(0.26f, 0.48f)
            });
            FillEllipse(c, w, h, 0.16f, 0.66f, 0.14f, 0.18f);
            FillEllipse(c, w, h, 0.84f, 0.66f, 0.14f, 0.18f);
            FillRect(c, w, h, 0.22f, 0.58f, 0.78f, 0.76f);
            FillRect(c, w, h, 0.38f, 0.26f, 0.62f, 0.50f);
            FillEllipse(c, w, h, 0.50f, 0.25f, 0.18f, 0.08f);
            FillRect(c, w, h, 0.22f, 0.11f, 0.78f, 0.25f);
            FillRect(c, w, h, 0.08f, 0.02f, 0.92f, 0.12f);
        }

        private static void DrawCatHeadLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.55f, 0.34f, 0.30f);
            FillTriangle(c, w, h, 0.16f, 0.42f, 0.28f, 0.08f, 0.42f, 0.36f);
            FillTriangle(c, w, h, 0.58f, 0.36f, 0.72f, 0.08f, 0.84f, 0.42f);
            FillRect(c, w, h, 0.30f, 0.70f, 0.70f, 0.84f);
        }

        private static void DrawDogHeadLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.55f, 0.32f, 0.29f);
            FillEllipse(c, w, h, 0.22f, 0.50f, 0.17f, 0.25f);
            FillEllipse(c, w, h, 0.78f, 0.50f, 0.17f, 0.25f);
            FillEllipse(c, w, h, 0.50f, 0.68f, 0.18f, 0.15f);
            FillRect(c, w, h, 0.34f, 0.72f, 0.66f, 0.86f);
        }

        private static void DrawBunnyHeadLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.45f, 0.30f, 0.24f);
            FillEllipse(c, w, h, 0.34f, 0.80f, 0.13f, 0.27f);
            FillEllipse(c, w, h, 0.66f, 0.80f, 0.13f, 0.27f);
            FillRect(c, w, h, 0.28f, 0.31f, 0.72f, 0.55f);
        }

        private static void DrawDogSittingTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.70f, 0.25f, 0.18f);
            FillEllipse(c, w, h, 0.25f, 0.66f, 0.14f, 0.20f);
            FillEllipse(c, w, h, 0.75f, 0.66f, 0.14f, 0.20f);
            FillEllipse(c, w, h, 0.50f, 0.54f, 0.17f, 0.12f);
            FillEllipse(c, w, h, 0.50f, 0.34f, 0.28f, 0.26f);
            FillRect(c, w, h, 0.34f, 0.12f, 0.66f, 0.36f);
            FillEllipse(c, w, h, 0.32f, 0.16f, 0.12f, 0.08f);
            FillEllipse(c, w, h, 0.68f, 0.16f, 0.12f, 0.08f);
        }

        private static void DrawDogSitSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.45f, 0.34f, 0.28f, 0.23f);
            FillEllipse(c, w, h, 0.67f, 0.61f, 0.18f, 0.15f);
            FillEllipse(c, w, h, 0.84f, 0.58f, 0.12f, 0.08f);
            FillRect(c, w, h, 0.58f, 0.43f, 0.72f, 0.58f);
            FillEllipse(c, w, h, 0.56f, 0.57f, 0.10f, 0.19f);
            FillEllipse(c, w, h, 0.58f, 0.41f, 0.08f, 0.08f);
            FillTriangle(c, w, h, 0.25f, 0.47f, 0.04f, 0.63f, 0.27f, 0.58f);
            FillRect(c, w, h, 0.60f, 0.08f, 0.72f, 0.34f);
            FillRect(c, w, h, 0.34f, 0.08f, 0.50f, 0.28f);
            FillEllipse(c, w, h, 0.66f, 0.09f, 0.12f, 0.06f);
            FillEllipse(c, w, h, 0.42f, 0.09f, 0.13f, 0.06f);
        }

        private static void DrawCatSitSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.45f, 0.34f, 0.25f, 0.24f);
            FillEllipse(c, w, h, 0.66f, 0.61f, 0.17f, 0.15f);
            FillEllipse(c, w, h, 0.80f, 0.58f, 0.09f, 0.06f);
            FillTriangle(c, w, h, 0.53f, 0.72f, 0.57f, 0.94f, 0.67f, 0.73f);
            FillTriangle(c, w, h, 0.66f, 0.73f, 0.80f, 0.91f, 0.78f, 0.68f);
            FillRect(c, w, h, 0.56f, 0.43f, 0.68f, 0.58f);
            FillEllipse(c, w, h, 0.18f, 0.49f, 0.09f, 0.25f);
            FillEllipse(c, w, h, 0.15f, 0.72f, 0.09f, 0.11f);
            FillRect(c, w, h, 0.20f, 0.22f, 0.28f, 0.54f);
            FillRect(c, w, h, 0.54f, 0.08f, 0.66f, 0.31f);
            FillRect(c, w, h, 0.34f, 0.08f, 0.48f, 0.25f);
            FillEllipse(c, w, h, 0.60f, 0.08f, 0.12f, 0.05f);
            FillEllipse(c, w, h, 0.42f, 0.08f, 0.12f, 0.05f);
        }

        private static void DrawBunnyTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.36f, 0.82f, 0.11f, 0.20f);
            FillEllipse(c, w, h, 0.64f, 0.82f, 0.11f, 0.20f);
            FillRect(c, w, h, 0.28f, 0.58f, 0.72f, 0.76f);
            FillEllipse(c, w, h, 0.50f, 0.58f, 0.27f, 0.20f);
            FillEllipse(c, w, h, 0.50f, 0.32f, 0.30f, 0.24f);
            FillEllipse(c, w, h, 0.33f, 0.12f, 0.12f, 0.08f);
            FillEllipse(c, w, h, 0.67f, 0.12f, 0.12f, 0.08f);
        }

        private static void DrawPenguinTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.42f, 0.30f, 0.36f);
            FillEllipse(c, w, h, 0.50f, 0.75f, 0.22f, 0.18f);
            FillTriangle(c, w, h, 0.28f, 0.46f, 0.04f, 0.34f, 0.24f, 0.26f);
            FillTriangle(c, w, h, 0.72f, 0.46f, 0.96f, 0.34f, 0.76f, 0.26f);
            FillTriangle(c, w, h, 0.42f, 0.14f, 0.30f, 0.04f, 0.52f, 0.08f);
            FillTriangle(c, w, h, 0.58f, 0.14f, 0.70f, 0.04f, 0.48f, 0.08f);
        }

        private static void DrawGiraffeTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.45f, 0.24f, 0.28f, 0.16f);
            FillRect(c, w, h, 0.42f, 0.28f, 0.58f, 0.74f);
            FillEllipse(c, w, h, 0.58f, 0.78f, 0.23f, 0.14f);
            FillEllipse(c, w, h, 0.78f, 0.78f, 0.11f, 0.09f);
            FillRect(c, w, h, 0.48f, 0.86f, 0.54f, 0.96f);
            FillRect(c, w, h, 0.66f, 0.86f, 0.72f, 0.96f);
            FillRect(c, w, h, 0.28f, 0.06f, 0.36f, 0.24f);
            FillRect(c, w, h, 0.54f, 0.06f, 0.62f, 0.24f);
        }

        private static void DrawHorseHeadTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.48f, 0.58f, 0.26f, 0.28f);
            FillEllipse(c, w, h, 0.50f, 0.30f, 0.20f, 0.20f);
            FillTriangle(c, w, h, 0.32f, 0.82f, 0.24f, 0.98f, 0.44f, 0.82f);
            FillTriangle(c, w, h, 0.58f, 0.82f, 0.72f, 0.96f, 0.68f, 0.76f);
            FillRect(c, w, h, 0.62f, 0.36f, 0.78f, 0.78f);
            FillEllipse(c, w, h, 0.42f, 0.12f, 0.14f, 0.08f);
            FillEllipse(c, w, h, 0.58f, 0.12f, 0.14f, 0.08f);
        }

        private static void DrawFoxSittingTall(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.67f, 0.24f, 0.18f);
            FillTriangle(c, w, h, 0.24f, 0.76f, 0.18f, 0.98f, 0.42f, 0.80f);
            FillTriangle(c, w, h, 0.58f, 0.80f, 0.82f, 0.98f, 0.76f, 0.76f);
            FillEllipse(c, w, h, 0.50f, 0.34f, 0.27f, 0.26f);
            FillTriangle(c, w, h, 0.72f, 0.30f, 0.98f, 0.54f, 0.78f, 0.12f);
            FillEllipse(c, w, h, 0.34f, 0.12f, 0.12f, 0.08f);
            FillEllipse(c, w, h, 0.64f, 0.12f, 0.12f, 0.08f);
        }

        private static void DrawDuckSideLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.45f, 0.35f, 0.28f, 0.20f);
            FillEllipse(c, w, h, 0.65f, 0.58f, 0.15f, 0.13f);
            FillRect(c, w, h, 0.56f, 0.43f, 0.66f, 0.56f);
            FillTriangle(c, w, h, 0.78f, 0.58f, 0.98f, 0.50f, 0.78f, 0.66f);
            FillTriangle(c, w, h, 0.20f, 0.40f, 0.04f, 0.55f, 0.22f, 0.50f);
            FillRect(c, w, h, 0.42f, 0.08f, 0.50f, 0.20f);
            FillRect(c, w, h, 0.56f, 0.08f, 0.64f, 0.20f);
            FillEllipse(c, w, h, 0.46f, 0.08f, 0.10f, 0.04f);
            FillEllipse(c, w, h, 0.60f, 0.08f, 0.10f, 0.04f);
        }

        private static void DrawFishSideLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.44f, 0.50f, 0.31f, 0.25f);
            FillTriangle(c, w, h, 0.72f, 0.50f, 0.96f, 0.22f, 0.96f, 0.78f);
            FillTriangle(c, w, h, 0.33f, 0.67f, 0.50f, 0.94f, 0.56f, 0.62f);
            FillTriangle(c, w, h, 0.34f, 0.34f, 0.52f, 0.06f, 0.56f, 0.38f);
            FillTriangle(c, w, h, 0.16f, 0.50f, 0.04f, 0.42f, 0.04f, 0.58f);
        }

        private static void DrawElephantSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.43f, 0.38f, 0.29f, 0.23f);
            FillEllipse(c, w, h, 0.68f, 0.55f, 0.18f, 0.17f);
            FillEllipse(c, w, h, 0.56f, 0.56f, 0.13f, 0.18f);
            FillRect(c, w, h, 0.78f, 0.24f, 0.90f, 0.56f);
            FillEllipse(c, w, h, 0.86f, 0.22f, 0.10f, 0.08f);
            FillTriangle(c, w, h, 0.22f, 0.42f, 0.04f, 0.55f, 0.24f, 0.52f);
            FillRect(c, w, h, 0.28f, 0.06f, 0.40f, 0.28f);
            FillRect(c, w, h, 0.52f, 0.06f, 0.64f, 0.28f);
            FillEllipse(c, w, h, 0.34f, 0.06f, 0.11f, 0.05f);
            FillEllipse(c, w, h, 0.58f, 0.06f, 0.11f, 0.05f);
        }

        private static void DrawFishLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.46f, 0.52f, 0.33f, 0.25f);
            FillTriangle(c, w, h, 0.74f, 0.52f, 0.96f, 0.22f, 0.96f, 0.82f);
            FillTriangle(c, w, h, 0.36f, 0.32f, 0.52f, 0.08f, 0.58f, 0.38f);
            FillTriangle(c, w, h, 0.36f, 0.72f, 0.52f, 0.96f, 0.58f, 0.66f);
        }

        private static void DrawTurtleLarge(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.46f, 0.56f, 0.34f, 0.24f);
            FillEllipse(c, w, h, 0.80f, 0.51f, 0.13f, 0.13f);
            FillEllipse(c, w, h, 0.22f, 0.42f, 0.10f, 0.09f);
            FillEllipse(c, w, h, 0.22f, 0.70f, 0.10f, 0.09f);
            FillEllipse(c, w, h, 0.58f, 0.78f, 0.11f, 0.08f);
            FillTriangle(c, w, h, 0.12f, 0.54f, 0.02f, 0.42f, 0.02f, 0.66f);
        }

        private static void DrawPawBold(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.66f, 0.26f, 0.20f);
            FillEllipse(c, w, h, 0.26f, 0.40f, 0.12f, 0.13f);
            FillEllipse(c, w, h, 0.42f, 0.32f, 0.12f, 0.14f);
            FillEllipse(c, w, h, 0.58f, 0.32f, 0.12f, 0.14f);
            FillEllipse(c, w, h, 0.74f, 0.40f, 0.12f, 0.13f);
            FillRect(c, w, h, 0.34f, 0.46f, 0.66f, 0.66f);
        }

        private static void DrawHeartBold(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.34f, 0.36f, 0.22f, 0.20f);
            FillEllipse(c, w, h, 0.66f, 0.36f, 0.22f, 0.20f);
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.16f, 0.40f),
                new Vector2(0.84f, 0.40f),
                new Vector2(0.50f, 0.92f)
            });
            FillRect(c, w, h, 0.26f, 0.36f, 0.74f, 0.56f);
        }

        private static void DrawPlusBold(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.36f, 0.08f, 0.64f, 0.92f);
            FillRect(c, w, h, 0.08f, 0.36f, 0.92f, 0.64f);
            FillRect(c, w, h, 0.28f, 0.28f, 0.72f, 0.72f);
        }

        private static void DrawXBold(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.12f, 0.04f),
                new Vector2(0.30f, 0.04f),
                new Vector2(0.94f, 0.72f),
                new Vector2(0.94f, 0.94f),
                new Vector2(0.76f, 0.94f),
                new Vector2(0.06f, 0.22f),
                new Vector2(0.06f, 0.06f)
            });
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.70f, 0.04f),
                new Vector2(0.88f, 0.04f),
                new Vector2(0.94f, 0.06f),
                new Vector2(0.94f, 0.22f),
                new Vector2(0.24f, 0.94f),
                new Vector2(0.06f, 0.94f),
                new Vector2(0.06f, 0.72f)
            });
        }

        private static void DrawLightningSmall(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.58f, 0.04f),
                new Vector2(0.18f, 0.52f),
                new Vector2(0.43f, 0.52f),
                new Vector2(0.30f, 0.96f),
                new Vector2(0.84f, 0.38f),
                new Vector2(0.58f, 0.38f),
                new Vector2(0.72f, 0.04f)
            });
        }

        private static void DrawGemBold(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.50f, 0.06f),
                new Vector2(0.88f, 0.28f),
                new Vector2(0.78f, 0.68f),
                new Vector2(0.50f, 0.94f),
                new Vector2(0.22f, 0.68f),
                new Vector2(0.12f, 0.28f)
            });
            FillRect(c, w, h, 0.28f, 0.24f, 0.72f, 0.40f);
        }

        private static void DrawStarBold(bool[] c, int w, int h)
        {
            FillStar(c, w, h, 0.50f, 0.52f, 0.46f, 0.23f, -90f);
        }

        private static void DrawHouseBold(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.20f, 0.46f, 0.80f, 0.92f);
            FillTriangle(c, w, h, 0.10f, 0.50f, 0.50f, 0.12f, 0.90f, 0.50f);
            FillRect(c, w, h, 0.42f, 0.68f, 0.58f, 0.92f);
            FillRect(c, w, h, 0.62f, 0.24f, 0.72f, 0.44f);
        }

        private static void DrawRocketBold(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.50f, 0.04f),
                new Vector2(0.68f, 0.26f),
                new Vector2(0.68f, 0.76f),
                new Vector2(0.56f, 0.90f),
                new Vector2(0.44f, 0.90f),
                new Vector2(0.32f, 0.76f),
                new Vector2(0.32f, 0.26f)
            });
            FillEllipse(c, w, h, 0.50f, 0.42f, 0.13f, 0.12f);
            FillTriangle(c, w, h, 0.32f, 0.62f, 0.12f, 0.90f, 0.34f, 0.82f);
            FillTriangle(c, w, h, 0.68f, 0.62f, 0.88f, 0.90f, 0.66f, 0.82f);
            FillTriangle(c, w, h, 0.44f, 0.86f, 0.50f, 0.98f, 0.56f, 0.86f);
        }

        private static void DrawCarBold(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.12f, 0.46f, 0.88f, 0.76f);
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.28f, 0.46f),
                new Vector2(0.42f, 0.28f),
                new Vector2(0.68f, 0.28f),
                new Vector2(0.80f, 0.46f)
            });
            FillEllipse(c, w, h, 0.30f, 0.77f, 0.12f, 0.10f);
            FillEllipse(c, w, h, 0.70f, 0.77f, 0.12f, 0.10f);
            FillRect(c, w, h, 0.08f, 0.56f, 0.16f, 0.70f);
            FillRect(c, w, h, 0.84f, 0.56f, 0.92f, 0.70f);
        }

        private static void DrawCatHead(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.47f, 0.38f, 0.33f);
            FillTriangle(c, w, h, 0.16f, 0.70f, 0.30f, 0.98f, 0.44f, 0.70f);
            FillTriangle(c, w, h, 0.56f, 0.70f, 0.70f, 0.98f, 0.84f, 0.70f);
            FillRect(c, w, h, 0.30f, 0.22f, 0.70f, 0.34f);
        }

        private static void DrawFish(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.47f, 0.50f, 0.34f, 0.25f);
            FillTriangle(c, w, h, 0.74f, 0.50f, 0.96f, 0.25f, 0.96f, 0.75f);
            FillTriangle(c, w, h, 0.35f, 0.30f, 0.50f, 0.10f, 0.57f, 0.35f);
            FillTriangle(c, w, h, 0.35f, 0.70f, 0.50f, 0.90f, 0.57f, 0.65f);
        }

        private static void DrawTree(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.40f, 0.55f, 0.60f, 0.95f);
            FillEllipse(c, w, h, 0.50f, 0.34f, 0.36f, 0.27f);
            FillEllipse(c, w, h, 0.35f, 0.42f, 0.22f, 0.20f);
            FillEllipse(c, w, h, 0.65f, 0.42f, 0.22f, 0.20f);
        }

        private static void DrawCloud(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.18f, 0.48f, 0.86f, 0.72f);
            FillEllipse(c, w, h, 0.30f, 0.48f, 0.22f, 0.19f);
            FillEllipse(c, w, h, 0.48f, 0.39f, 0.25f, 0.22f);
            FillEllipse(c, w, h, 0.68f, 0.48f, 0.23f, 0.20f);
        }

        private static void DrawCrown(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.18f, 0.58f, 0.82f, 0.82f);
            FillTriangle(c, w, h, 0.16f, 0.58f, 0.26f, 0.18f, 0.40f, 0.58f);
            FillTriangle(c, w, h, 0.34f, 0.58f, 0.50f, 0.10f, 0.66f, 0.58f);
            FillTriangle(c, w, h, 0.60f, 0.58f, 0.74f, 0.18f, 0.84f, 0.58f);
        }

        private static void DrawShield(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.12f, 0.14f),
                new Vector2(0.88f, 0.14f),
                new Vector2(0.84f, 0.58f),
                new Vector2(0.50f, 0.92f),
                new Vector2(0.16f, 0.58f)
            });
        }

        private static void DrawBalloon(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.34f, 0.36f, 0.25f);
            FillTriangle(c, w, h, 0.42f, 0.58f, 0.58f, 0.58f, 0.50f, 0.70f);
            FillRect(c, w, h, 0.44f, 0.66f, 0.56f, 0.96f);
        }

        private static void DrawDuck(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.46f, 0.60f, 0.34f, 0.23f);
            FillEllipse(c, w, h, 0.33f, 0.38f, 0.20f, 0.18f);
            FillTriangle(c, w, h, 0.16f, 0.38f, 0.04f, 0.32f, 0.16f, 0.48f);
            FillTriangle(c, w, h, 0.65f, 0.50f, 0.88f, 0.38f, 0.72f, 0.64f);
        }

        private static void DrawBunny(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.58f, 0.32f, 0.27f);
            FillEllipse(c, w, h, 0.35f, 0.24f, 0.12f, 0.23f);
            FillEllipse(c, w, h, 0.65f, 0.24f, 0.12f, 0.23f);
            FillRect(c, w, h, 0.28f, 0.72f, 0.72f, 0.86f);
        }

        private static void DrawBear(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.54f, 0.34f, 0.30f);
            FillEllipse(c, w, h, 0.26f, 0.30f, 0.15f, 0.15f);
            FillEllipse(c, w, h, 0.74f, 0.30f, 0.15f, 0.15f);
            FillRect(c, w, h, 0.34f, 0.76f, 0.66f, 0.88f);
        }

        private static void DrawTurtle(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.48f, 0.55f, 0.34f, 0.23f);
            FillEllipse(c, w, h, 0.78f, 0.50f, 0.13f, 0.13f);
            FillEllipse(c, w, h, 0.22f, 0.45f, 0.10f, 0.09f);
            FillEllipse(c, w, h, 0.22f, 0.68f, 0.10f, 0.09f);
            FillEllipse(c, w, h, 0.56f, 0.76f, 0.10f, 0.08f);
        }

        private static void DrawFlower(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.44f, 0.14f, 0.14f);
            FillEllipse(c, w, h, 0.50f, 0.24f, 0.15f, 0.17f);
            FillEllipse(c, w, h, 0.50f, 0.64f, 0.15f, 0.17f);
            FillEllipse(c, w, h, 0.30f, 0.44f, 0.17f, 0.15f);
            FillEllipse(c, w, h, 0.70f, 0.44f, 0.17f, 0.15f);
            FillRect(c, w, h, 0.45f, 0.56f, 0.55f, 0.96f);
            FillEllipse(c, w, h, 0.36f, 0.78f, 0.15f, 0.08f);
        }

        private static void DrawMushroom(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.36f, 0.38f, 0.22f);
            FillRect(c, w, h, 0.20f, 0.36f, 0.80f, 0.48f);
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.37f, 0.45f),
                new Vector2(0.63f, 0.45f),
                new Vector2(0.70f, 0.92f),
                new Vector2(0.30f, 0.92f)
            });
        }

        private static void DrawUmbrella(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.38f, 0.42f, 0.25f);
            FillRect(c, w, h, 0.12f, 0.38f, 0.88f, 0.52f);
            FillRect(c, w, h, 0.46f, 0.48f, 0.56f, 0.90f);
            FillRect(c, w, h, 0.36f, 0.82f, 0.56f, 0.94f);
        }

        private static void DrawGift(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.20f, 0.42f, 0.80f, 0.88f);
            FillRect(c, w, h, 0.44f, 0.30f, 0.56f, 0.88f);
            FillRect(c, w, h, 0.20f, 0.55f, 0.80f, 0.66f);
            FillEllipse(c, w, h, 0.40f, 0.28f, 0.13f, 0.10f);
            FillEllipse(c, w, h, 0.60f, 0.28f, 0.13f, 0.10f);
        }

        private static void DrawBoat(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.12f, 0.62f),
                new Vector2(0.90f, 0.62f),
                new Vector2(0.76f, 0.84f),
                new Vector2(0.24f, 0.84f)
            });
            FillTriangle(c, w, h, 0.45f, 0.14f, 0.45f, 0.60f, 0.18f, 0.60f);
            FillTriangle(c, w, h, 0.50f, 0.20f, 0.78f, 0.60f, 0.50f, 0.60f);
            FillRect(c, w, h, 0.44f, 0.16f, 0.52f, 0.64f);
        }

        private static void DrawPlane(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.16f, 0.42f, 0.84f, 0.58f);
            FillEllipse(c, w, h, 0.82f, 0.50f, 0.10f, 0.10f);
            FillTriangle(c, w, h, 0.42f, 0.42f, 0.62f, 0.12f, 0.58f, 0.42f);
            FillTriangle(c, w, h, 0.42f, 0.58f, 0.62f, 0.88f, 0.58f, 0.58f);
            FillTriangle(c, w, h, 0.18f, 0.42f, 0.06f, 0.26f, 0.28f, 0.46f);
            FillTriangle(c, w, h, 0.18f, 0.58f, 0.06f, 0.74f, 0.28f, 0.54f);
        }

        private static void DrawMusicNote(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.48f, 0.14f, 0.68f, 0.74f);
            FillRect(c, w, h, 0.48f, 0.14f, 0.86f, 0.32f);
            FillEllipse(c, w, h, 0.36f, 0.78f, 0.21f, 0.16f);
            FillEllipse(c, w, h, 0.70f, 0.66f, 0.20f, 0.15f);
            FillRect(c, w, h, 0.66f, 0.28f, 0.86f, 0.64f);
        }

        private static void DrawCastle(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.18f, 0.44f, 0.82f, 0.92f);
            FillRect(c, w, h, 0.10f, 0.30f, 0.30f, 0.92f);
            FillRect(c, w, h, 0.70f, 0.30f, 0.90f, 0.92f);
            FillRect(c, w, h, 0.40f, 0.22f, 0.60f, 0.92f);
            FillTriangle(c, w, h, 0.08f, 0.30f, 0.20f, 0.12f, 0.32f, 0.30f);
            FillTriangle(c, w, h, 0.38f, 0.22f, 0.50f, 0.06f, 0.62f, 0.22f);
            FillTriangle(c, w, h, 0.68f, 0.30f, 0.80f, 0.12f, 0.92f, 0.30f);
        }

        private static void DrawBus(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.12f, 0.28f, 0.88f, 0.76f);
            FillRect(c, w, h, 0.22f, 0.18f, 0.78f, 0.32f);
            FillEllipse(c, w, h, 0.28f, 0.78f, 0.12f, 0.10f);
            FillEllipse(c, w, h, 0.72f, 0.78f, 0.12f, 0.10f);
        }

        private static void DrawTrainFront(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.24f, 0.28f, 0.76f, 0.84f);
            FillRect(c, w, h, 0.34f, 0.14f, 0.66f, 0.32f);
            FillRect(c, w, h, 0.42f, 0.06f, 0.58f, 0.16f);
            FillEllipse(c, w, h, 0.36f, 0.84f, 0.13f, 0.10f);
            FillEllipse(c, w, h, 0.64f, 0.84f, 0.13f, 0.10f);
            FillRect(c, w, h, 0.16f, 0.72f, 0.84f, 0.84f);
        }

        private static void DrawUfo(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.61f, 0.44f, 0.20f);
            FillEllipse(c, w, h, 0.50f, 0.43f, 0.26f, 0.18f);
            FillRect(c, w, h, 0.14f, 0.55f, 0.86f, 0.72f);
        }

        private static void DrawSun(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.50f, 0.26f, 0.26f);
            for (int i = 0; i < 8; i++)
            {
                float angle = i * Mathf.PI * 0.25f;
                FillRayTriangle(c, w, h, angle, 0.30f, 0.45f, 0.08f);
            }
        }

        private static void DrawSnowman(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.50f, 0.74f, 0.30f, 0.21f);
            FillEllipse(c, w, h, 0.50f, 0.50f, 0.24f, 0.18f);
            FillEllipse(c, w, h, 0.50f, 0.28f, 0.18f, 0.15f);
            FillRect(c, w, h, 0.34f, 0.14f, 0.66f, 0.22f);
            FillRect(c, w, h, 0.40f, 0.06f, 0.60f, 0.16f);
        }

        private static void DrawLightning(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.58f, 0.05f),
                new Vector2(0.28f, 0.48f),
                new Vector2(0.48f, 0.48f),
                new Vector2(0.32f, 0.94f),
                new Vector2(0.76f, 0.38f),
                new Vector2(0.54f, 0.38f)
            });
        }

        private static void DrawCup(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.18f, 0.14f),
                new Vector2(0.82f, 0.14f),
                new Vector2(0.72f, 0.76f),
                new Vector2(0.28f, 0.76f)
            });
            FillRect(c, w, h, 0.34f, 0.76f, 0.66f, 0.90f);
            FillRect(c, w, h, 0.24f, 0.88f, 0.76f, 0.96f);
        }

        private static void DrawLighthouse(bool[] c, int w, int h)
        {
            FillPolygon(c, w, h, new[]
            {
                new Vector2(0.40f, 0.24f),
                new Vector2(0.60f, 0.24f),
                new Vector2(0.70f, 0.94f),
                new Vector2(0.30f, 0.94f)
            });
            FillRect(c, w, h, 0.34f, 0.14f, 0.66f, 0.28f);
            FillTriangle(c, w, h, 0.30f, 0.14f, 0.50f, 0.04f, 0.70f, 0.14f);
            FillRect(c, w, h, 0.24f, 0.88f, 0.76f, 0.96f);
        }

        private static void DrawBigCastle(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.16f, 0.46f, 0.84f, 0.92f);
            FillRect(c, w, h, 0.08f, 0.32f, 0.28f, 0.92f);
            FillRect(c, w, h, 0.72f, 0.32f, 0.92f, 0.92f);
            FillRect(c, w, h, 0.38f, 0.22f, 0.62f, 0.92f);
            FillTriangle(c, w, h, 0.06f, 0.32f, 0.18f, 0.12f, 0.30f, 0.32f);
            FillTriangle(c, w, h, 0.36f, 0.22f, 0.50f, 0.04f, 0.64f, 0.22f);
            FillTriangle(c, w, h, 0.70f, 0.32f, 0.82f, 0.12f, 0.94f, 0.32f);
            FillRect(c, w, h, 0.24f, 0.38f, 0.34f, 0.52f);
            FillRect(c, w, h, 0.66f, 0.38f, 0.76f, 0.52f);
        }

        private static void DrawWhale(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.46f, 0.56f, 0.35f, 0.22f);
            FillTriangle(c, w, h, 0.78f, 0.52f, 0.96f, 0.28f, 0.92f, 0.56f);
            FillTriangle(c, w, h, 0.78f, 0.60f, 0.96f, 0.84f, 0.92f, 0.56f);
            FillTriangle(c, w, h, 0.40f, 0.70f, 0.56f, 0.90f, 0.58f, 0.66f);
        }

        private static void DrawWhaleBoldSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.42f, 0.50f, 0.38f, 0.27f);
            FillEllipse(c, w, h, 0.20f, 0.50f, 0.18f, 0.20f);
            FillTriangle(c, w, h, 0.75f, 0.50f, 0.98f, 0.20f, 0.94f, 0.52f);
            FillTriangle(c, w, h, 0.75f, 0.50f, 0.98f, 0.80f, 0.94f, 0.48f);
            FillTriangle(c, w, h, 0.36f, 0.68f, 0.56f, 0.92f, 0.58f, 0.62f);
            FillTriangle(c, w, h, 0.40f, 0.32f, 0.58f, 0.10f, 0.60f, 0.38f);
            FillRect(c, w, h, 0.08f, 0.44f, 0.72f, 0.62f);
        }

        private static void DrawTurtleBoldSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.44f, 0.56f, 0.34f, 0.25f);
            FillRect(c, w, h, 0.18f, 0.48f, 0.70f, 0.68f);
            FillEllipse(c, w, h, 0.78f, 0.56f, 0.15f, 0.13f);
            FillEllipse(c, w, h, 0.26f, 0.28f, 0.12f, 0.09f);
            FillEllipse(c, w, h, 0.58f, 0.28f, 0.12f, 0.09f);
            FillEllipse(c, w, h, 0.28f, 0.80f, 0.11f, 0.08f);
            FillEllipse(c, w, h, 0.60f, 0.80f, 0.11f, 0.08f);
            FillTriangle(c, w, h, 0.12f, 0.55f, 0.02f, 0.43f, 0.02f, 0.67f);
            FillRect(c, w, h, 0.68f, 0.50f, 0.86f, 0.62f);
        }

        private static void DrawSnailBoldSide(bool[] c, int w, int h)
        {
            FillEllipse(c, w, h, 0.36f, 0.56f, 0.25f, 0.29f);
            FillEllipse(c, w, h, 0.36f, 0.56f, 0.14f, 0.16f);
            FillEllipse(c, w, h, 0.63f, 0.36f, 0.27f, 0.16f);
            FillRect(c, w, h, 0.26f, 0.24f, 0.84f, 0.42f);
            FillEllipse(c, w, h, 0.83f, 0.42f, 0.13f, 0.13f);
            FillRect(c, w, h, 0.78f, 0.50f, 0.84f, 0.82f);
            FillRect(c, w, h, 0.88f, 0.48f, 0.94f, 0.78f);
            FillEllipse(c, w, h, 0.81f, 0.82f, 0.07f, 0.05f);
            FillEllipse(c, w, h, 0.91f, 0.78f, 0.07f, 0.05f);
        }

        private static void DrawTallFlower(bool[] c, int w, int h)
        {
            FillRect(c, w, h, 0.46f, 0.42f, 0.54f, 0.96f);
            FillEllipse(c, w, h, 0.50f, 0.28f, 0.13f, 0.10f);
            FillEllipse(c, w, h, 0.50f, 0.12f, 0.15f, 0.12f);
            FillEllipse(c, w, h, 0.50f, 0.44f, 0.15f, 0.12f);
            FillEllipse(c, w, h, 0.32f, 0.28f, 0.15f, 0.12f);
            FillEllipse(c, w, h, 0.68f, 0.28f, 0.15f, 0.12f);
            FillEllipse(c, w, h, 0.36f, 0.72f, 0.18f, 0.10f);
            FillEllipse(c, w, h, 0.64f, 0.62f, 0.18f, 0.10f);
        }

        private static void FillRayTriangle(bool[] cells, int w, int h, float angle, float innerR, float outerR, float halfWidth)
        {
            var center = new Vector2(0.5f, 0.5f);
            var dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var normal = new Vector2(-dir.y, dir.x);
            var p0 = center + dir * innerR + normal * halfWidth;
            var p1 = center + dir * outerR;
            var p2 = center + dir * innerR - normal * halfWidth;
            FillTriangle(cells, w, h, p0.x, p0.y, p1.x, p1.y, p2.x, p2.y);
        }

        private static void FillStar(bool[] cells, int w, int h, float cx, float cy, float outerR, float innerR, float angleOffsetDeg)
        {
            var points = new Vector2[10];
            float angleOffset = angleOffsetDeg * Mathf.Deg2Rad;
            for (int i = 0; i < points.Length; i++)
            {
                float r = i % 2 == 0 ? outerR : innerR;
                float angle = angleOffset + i * Mathf.PI / 5f;
                points[i] = new Vector2(cx + Mathf.Cos(angle) * r, cy + Mathf.Sin(angle) * r);
            }

            FillPolygon(cells, w, h, points);
        }

        private static void FillRect(bool[] cells, int w, int h, float x0, float y0, float x1, float y1)
        {
            int minX = Mathf.Clamp(Mathf.FloorToInt(Mathf.Min(x0, x1) * w), 0, w - 1);
            int maxX = Mathf.Clamp(Mathf.CeilToInt(Mathf.Max(x0, x1) * w) - 1, 0, w - 1);
            int minY = Mathf.Clamp(Mathf.FloorToInt(Mathf.Min(y0, y1) * h), 0, h - 1);
            int maxY = Mathf.Clamp(Mathf.CeilToInt(Mathf.Max(y0, y1) * h) - 1, 0, h - 1);
            for (int y = minY; y <= maxY; y++)
            for (int x = minX; x <= maxX; x++)
                cells[x + y * w] = true;
        }

        private static void FillEllipse(bool[] cells, int w, int h, float cx, float cy, float rx, float ry)
        {
            if (rx <= 0f || ry <= 0f)
                return;

            for (int y = 0; y < h; y++)
            {
                float py = (y + 0.5f) / h;
                float dy = (py - cy) / ry;
                for (int x = 0; x < w; x++)
                {
                    float px = (x + 0.5f) / w;
                    float dx = (px - cx) / rx;
                    if (dx * dx + dy * dy <= 1f)
                        cells[x + y * w] = true;
                }
            }
        }

        private static void FillTriangle(bool[] cells, int w, int h, float ax, float ay, float bx, float by, float cx, float cy)
        {
            FillPolygon(cells, w, h, new[] { new Vector2(ax, ay), new Vector2(bx, by), new Vector2(cx, cy) });
        }

        private static void FillPolygon(bool[] cells, int w, int h, Vector2[] points)
        {
            if (points == null || points.Length < 3)
                return;

            for (int y = 0; y < h; y++)
            {
                float py = (y + 0.5f) / h;
                for (int x = 0; x < w; x++)
                {
                    float px = (x + 0.5f) / w;
                    if (PointInPolygon(px, py, points))
                        cells[x + y * w] = true;
                }
            }
        }

        private static bool PointInPolygon(float x, float y, Vector2[] points)
        {
            bool inside = false;
            int j = points.Length - 1;
            for (int i = 0; i < points.Length; i++)
            {
                bool crosses = (points[i].y > y) != (points[j].y > y);
                if (crosses)
                {
                    float denom = points[j].y - points[i].y;
                    if (Mathf.Abs(denom) < 0.0001f)
                    {
                        j = i;
                        continue;
                    }

                    float atX = (points[j].x - points[i].x) * (y - points[i].y) / denom + points[i].x;
                    if (x < atX)
                        inside = !inside;
                }
                j = i;
            }
            return inside;
        }

        private static void CleanTinyArtifacts(bool[] cells, int w, int h)
        {
            var copy = (bool[])cells.Clone();
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                int idx = x + y * w;
                if (!copy[idx])
                    continue;

                int neighbors = 0;
                for (int oy = -1; oy <= 1; oy++)
                for (int ox = -1; ox <= 1; ox++)
                {
                    if (ox == 0 && oy == 0)
                        continue;
                    int nx = x + ox;
                    int ny = y + oy;
                    if (nx >= 0 && nx < w && ny >= 0 && ny < h && copy[nx + ny * w])
                        neighbors++;
                }

                if (neighbors <= 1)
                    cells[idx] = false;
            }
        }

        private static void EnsureMinimumCoverage(bool[] cells, int w, int h, float minCoverage, int maxPasses)
        {
            int total = Mathf.Max(1, w * h);
            for (int pass = 0; pass < maxPasses; pass++)
            {
                float coverage = CountCells(cells) / (float)total;
                if (coverage >= minCoverage)
                    return;

                Dilate(cells, w, h);
            }
        }

        private static void FitContentToCanvas(bool[] cells, int w, int h, int padding)
        {
            if (cells == null || cells.Length != w * h || w <= 2 || h <= 2)
                return;

            int minX = w;
            int maxX = -1;
            int minY = h;
            int maxY = -1;
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                if (!cells[x + y * w])
                    continue;

                minX = Mathf.Min(minX, x);
                maxX = Mathf.Max(maxX, x);
                minY = Mathf.Min(minY, y);
                maxY = Mathf.Max(maxY, y);
            }

            if (maxX < minX || maxY < minY)
                return;

            padding = Mathf.Clamp(padding, 0, Mathf.Min(w, h) / 4);
            int targetW = Mathf.Max(1, w - padding * 2);
            int targetH = Mathf.Max(1, h - padding * 2);
            int sourceW = Mathf.Max(1, maxX - minX + 1);
            int sourceH = Mathf.Max(1, maxY - minY + 1);
            var copy = (bool[])cells.Clone();
            Array.Clear(cells, 0, cells.Length);

            for (int y = 0; y < targetH; y++)
            for (int x = 0; x < targetW; x++)
            {
                int sx = minX + Mathf.Clamp(Mathf.FloorToInt((x + 0.5f) * sourceW / targetW), 0, sourceW - 1);
                int sy = minY + Mathf.Clamp(Mathf.FloorToInt((y + 0.5f) * sourceH / targetH), 0, sourceH - 1);
                if (copy[sx + sy * w])
                    cells[(x + padding) + (y + padding) * w] = true;
            }
        }

        private static void CropContentToCanvas(bool[] source, int w, int h, int padding, out bool[] cropped, out int croppedW, out int croppedH)
        {
            cropped = source;
            croppedW = w;
            croppedH = h;
            if (source == null || source.Length != w * h || w <= 0 || h <= 0)
                return;

            int minX = w;
            int maxX = -1;
            int minY = h;
            int maxY = -1;
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                if (!source[x + y * w])
                    continue;

                minX = Mathf.Min(minX, x);
                maxX = Mathf.Max(maxX, x);
                minY = Mathf.Min(minY, y);
                maxY = Mathf.Max(maxY, y);
            }

            if (maxX < minX || maxY < minY)
                return;

            padding = Mathf.Max(0, padding);
            minX = Mathf.Max(0, minX - padding);
            maxX = Mathf.Min(w - 1, maxX + padding);
            minY = Mathf.Max(0, minY - padding);
            maxY = Mathf.Min(h - 1, maxY + padding);

            croppedW = Mathf.Max(1, maxX - minX + 1);
            croppedH = Mathf.Max(1, maxY - minY + 1);
            cropped = new bool[croppedW * croppedH];

            for (int y = 0; y < croppedH; y++)
            for (int x = 0; x < croppedW; x++)
                cropped[x + y * croppedW] = source[(x + minX) + (y + minY) * w];
        }

        private static void Dilate(bool[] cells, int w, int h)
        {
            var copy = (bool[])cells.Clone();
            for (int y = 0; y < h; y++)
            for (int x = 0; x < w; x++)
            {
                int idx = x + y * w;
                if (copy[idx])
                    continue;

                bool near = false;
                for (int oy = -1; oy <= 1 && !near; oy++)
                for (int ox = -1; ox <= 1 && !near; ox++)
                {
                    if (Mathf.Abs(ox) + Mathf.Abs(oy) != 1)
                        continue;
                    int nx = x + ox;
                    int ny = y + oy;
                    if (nx >= 0 && nx < w && ny >= 0 && ny < h && copy[nx + ny * w])
                        near = true;
                }

                if (near)
                    cells[idx] = true;
            }
        }

        private static int CountCells(bool[] cells)
        {
            int count = 0;
            if (cells == null)
                return count;
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i])
                    count++;
            }
            return count;
        }

        private static void WriteMask(string assetPath, int width, int height, bool[] cells)
        {
            EnsureFolder(Path.GetDirectoryName(assetPath)?.Replace("\\", "/"));
            var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                tex.SetPixel(x, y, cells[x + y * width] ? Fill : Clear);

            tex.Apply(false, false);
            File.WriteAllBytes(ToFullPath(assetPath), tex.EncodeToPNG());
            UnityEngine.Object.DestroyImmediate(tex);
        }

        private static Texture2D BuildPreview(int width, int height, bool[] cells)
        {
            const int cell = 8;
            var tex = new Texture2D(width * cell, height * cell, TextureFormat.RGBA32, false);
            var bg = new Color32(28, 32, 44, 255);
            var fill = new Color32(245, 246, 250, 255);
            var grid = new Color32(54, 60, 76, 255);

            for (int y = 0; y < tex.height; y++)
            for (int x = 0; x < tex.width; x++)
                tex.SetPixel(x, y, (x % cell == 0 || y % cell == 0) ? grid : bg);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (!cells[x + y * width])
                    continue;

                for (int py = 1; py < cell; py++)
                for (int px = 1; px < cell; px++)
                    tex.SetPixel(x * cell + px, y * cell + py, fill);
            }

            tex.Apply(false, false);
            return tex;
        }

        private static void WriteContactSheet(List<Texture2D> previews, List<ShapeMaskSpec> specs, string assetPath)
        {
            EnsureFolder(Path.GetDirectoryName(assetPath)?.Replace("\\", "/"));
            const int thumbW = 160;
            const int thumbH = 180;
            const int cols = 5;
            int rows = Mathf.CeilToInt(previews.Count / (float)cols);
            var sheet = new Texture2D(cols * thumbW, rows * thumbH, TextureFormat.RGBA32, false);
            var bg = new Color32(18, 22, 32, 255);

            for (int y = 0; y < sheet.height; y++)
            for (int x = 0; x < sheet.width; x++)
                sheet.SetPixel(x, y, bg);

            for (int i = 0; i < previews.Count; i++)
            {
                int col = i % cols;
                int row = i / cols;
                BlitFit(sheet, previews[i], col * thumbW + 12, row * thumbH + 10, thumbW - 24, thumbH - 42);
                DrawIndexLabel(sheet, col * thumbW + 12, row * thumbH + thumbH - 28, i + 1);
            }

            sheet.Apply(false, false);
            File.WriteAllBytes(ToFullPath(assetPath), sheet.EncodeToPNG());
            UnityEngine.Object.DestroyImmediate(sheet);
        }

        private static void BlitFit(Texture2D dst, Texture2D src, int x, int y, int w, int h)
        {
            float scale = Mathf.Min(w / (float)src.width, h / (float)src.height);
            int outW = Mathf.Max(1, Mathf.RoundToInt(src.width * scale));
            int outH = Mathf.Max(1, Mathf.RoundToInt(src.height * scale));
            int startX = x + (w - outW) / 2;
            int startY = y + (h - outH) / 2;
            for (int yy = 0; yy < outH; yy++)
            for (int xx = 0; xx < outW; xx++)
            {
                int sx = Mathf.Clamp(Mathf.FloorToInt(xx / scale), 0, src.width - 1);
                int sy = Mathf.Clamp(Mathf.FloorToInt(yy / scale), 0, src.height - 1);
                dst.SetPixel(startX + xx, startY + yy, src.GetPixel(sx, sy));
            }
        }

        private static void DrawIndexLabel(Texture2D tex, int x, int y, int index)
        {
            var color = new Color32(220, 224, 235, 255);
            string text = index.ToString("00", CultureInfo.InvariantCulture);
            for (int i = 0; i < text.Length; i++)
                DrawDigit(tex, x + i * 10, y, text[i] - '0', color);
        }

        private static void DrawDigit(Texture2D tex, int x, int y, int digit, Color32 color)
        {
            string[] rows = DigitRows(digit);
            for (int yy = 0; yy < rows.Length; yy++)
            for (int xx = 0; xx < rows[yy].Length; xx++)
            {
                if (rows[yy][xx] != '1')
                    continue;

                FillPixelBlock(tex, x + xx * 2, y + yy * 2, 2, 2, color);
            }
        }

        private static string[] DigitRows(int digit)
        {
            switch (Mathf.Clamp(digit, 0, 9))
            {
                case 0: return new[] { "111", "101", "101", "101", "111" };
                case 1: return new[] { "010", "110", "010", "010", "111" };
                case 2: return new[] { "111", "001", "111", "100", "111" };
                case 3: return new[] { "111", "001", "111", "001", "111" };
                case 4: return new[] { "101", "101", "111", "001", "001" };
                case 5: return new[] { "111", "100", "111", "001", "111" };
                case 6: return new[] { "111", "100", "111", "101", "111" };
                case 7: return new[] { "111", "001", "010", "010", "010" };
                case 8: return new[] { "111", "101", "111", "101", "111" };
                default: return new[] { "111", "101", "111", "001", "111" };
            }
        }

        private static void FillPixelBlock(Texture2D tex, int x, int y, int w, int h, Color32 color)
        {
            for (int yy = 0; yy < h; yy++)
            for (int xx = 0; xx < w; xx++)
            {
                int px = x + xx;
                int py = y + yy;
                if (px >= 0 && px < tex.width && py >= 0 && py < tex.height)
                    tex.SetPixel(px, py, color);
            }
        }

        private static void EnsureFolder(string assetFolder)
        {
            if (string.IsNullOrWhiteSpace(assetFolder))
                return;

            string normalized = assetFolder.Replace("\\", "/").TrimEnd('/');
            if (AssetDatabase.IsValidFolder(normalized))
                return;

            string[] parts = normalized.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                    AssetDatabase.CreateFolder(current, parts[i]);
                current = next;
            }
        }

        private static string ToFullPath(string assetPath)
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, "..", assetPath));
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            if (value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0)
                return value;
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
#endif
