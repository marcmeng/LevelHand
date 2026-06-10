using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace PixelBug.ArrowMagic
{
    [Serializable]
    public class TileSprites
    {
        public Texture2D straight;   // base: connects Left+Right
        public Texture2D corner;     // base: connects Up+Right
        public Texture2D arrowhead;  // base: points Right
        public Texture2D entry;      // base: points Right (or symmetric dot), optional
        public Texture2D block;      // optional (or leave null to use color)
    }

    public sealed class UIToolkitTileRenderer
    {
        public struct TileUI
        {
            public Button button;
            public VisualElement root;
            public VisualElement pipe;
            public VisualElement arrowhead;
            public VisualElement entry;
            public VisualElement block;
            
            public Label debugLabel;
        }

        readonly TileSprites _sprites;
        readonly bool _showEntryMarker;
        readonly bool _showDebugText;

        public UIToolkitTileRenderer(TileSprites sprites, bool showEntryMarker = true, bool showDebugText = false)
        {
            _sprites = sprites;
            _showEntryMarker = showEntryMarker;
            _showDebugText = showDebugText;
        }

        public TileUI CreateTileButton(Action onClick)
        {
            var btn = new Button(onClick);
            btn.AddToClassList("tile-btn");

            var root = new VisualElement();
            root.AddToClassList("tile-root");
            btn.Add(root);

            var pipe = new VisualElement();  pipe.AddToClassList("tile-layer");
            var arrow = new VisualElement(); arrow.AddToClassList("tile-layer"); arrow.AddToClassList("tile-arrowhead");
            var entry = new VisualElement(); entry.AddToClassList("tile-layer"); entry.AddToClassList("tile-entry");
            var block = new VisualElement(); block.AddToClassList("tile-layer"); block.AddToClassList("tile-block");

            root.Add(pipe);
            root.Add(arrow);
            root.Add(entry);
            root.Add(block);
            
            Label dbg = null;
            if (_showDebugText)
            {
                dbg = new Label();
                dbg.AddToClassList("tile-debug");
                root.Add(dbg);
            }

            return new TileUI
            {
                button = btn,
                root = root,
                pipe = pipe,
                arrowhead = arrow,
                entry = entry,
                block = block,
                debugLabel = dbg
            };
        }

        public void Render(TileUI ui, TileState t)
        {
            ui.pipe.style.display = DisplayStyle.None;
            ui.arrowhead.style.display = DisplayStyle.None;
            ui.entry.style.display = DisplayStyle.None;
            ui.block.style.display = DisplayStyle.None;

            if (ui.debugLabel != null)
            {
                ui.debugLabel.style.display = DisplayStyle.None;
                ui.debugLabel.text = "";
            }
            
            switch (t.type)
            {
                case TileType.Empty:
                    ui.button.SetEnabled(false);
                    ui.button.style.opacity = 0.20f;
                    return;

                case TileType.Block:
                    ui.button.SetEnabled(false);
                    ui.button.style.opacity = 1f;

                    ui.block.style.display = DisplayStyle.Flex;
                    if (_sprites.block != null)
                        ui.block.style.backgroundImage = new StyleBackground(_sprites.block);
                    else
                        ui.block.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.35f));

                    if (ui.debugLabel != null)
                    {
                        ui.debugLabel.style.display = DisplayStyle.Flex;
                        ui.debugLabel.text = "■";
                    }
                    return;

                case TileType.Arrow:
                    ui.button.SetEnabled(true);
                    ui.button.style.opacity = 1f;

                    // Pipe base (straight/corner)
                    ui.pipe.style.display = DisplayStyle.Flex;

                    bool isStraight = (t.arrow.inDir == Opposite(t.arrow.outDir));
                    if (isStraight)
                    {
                        ui.pipe.style.backgroundImage = new StyleBackground(_sprites.straight);
                        SetRotation(ui.pipe, IsVertical(t.arrow) ? 90f : 0f);
                    }
                    else
                    {
                        ui.pipe.style.backgroundImage = new StyleBackground(_sprites.corner);
                        SetRotation(ui.pipe, CornerRotationDeg(t.arrow.inDir, t.arrow.outDir));
                    }

                    // Arrowhead (out dir)
                    ui.arrowhead.style.display = DisplayStyle.Flex;
                    ui.arrowhead.style.backgroundImage = new StyleBackground(_sprites.arrowhead);
                    SetRotation(ui.arrowhead, DirRotationDeg(t.arrow.outDir));

                    // Entry marker (in dir), optional
                    if (_showEntryMarker && _sprites.entry != null)
                    {
                        ui.entry.style.display = DisplayStyle.Flex;
                        ui.entry.style.backgroundImage = new StyleBackground(_sprites.entry);
                        SetRotation(ui.entry, DirRotationDeg(t.arrow.inDir));
                    }
                    
                    // Debug overlay text (in/out)
                    if (ui.debugLabel != null)
                    {
                        ui.debugLabel.style.display = DisplayStyle.Flex;
                        ui.debugLabel.text = $"{DirShort(t.arrow.inDir)}→{DirShort(t.arrow.outDir)}";
                    }

                    return;
            }
        }
        
        static string DirShort(Dir d) => d switch
        {
            Dir.Up => "U",
            Dir.Right => "R",
            Dir.Down => "D",
            _ => "L",
        };

        static bool IsVertical(Arrow a) =>
            a.inDir == Dir.Up || a.inDir == Dir.Down ||
            a.outDir == Dir.Up || a.outDir == Dir.Down;

        static float CornerRotationDeg(Dir inDir, Dir outDir)
        {
            bool Has(Dir a, Dir b, Dir s1, Dir s2) =>
                (a == s1 && b == s2) || (a == s2 && b == s1);

            if (Has(inDir, outDir, Dir.Up, Dir.Right)) return 0f;
            if (Has(inDir, outDir, Dir.Left, Dir.Up)) return 90f;
            if (Has(inDir, outDir, Dir.Down, Dir.Left)) return 180f;
            return 270f; // Right + Down
        }

        static float DirRotationDeg(Dir d) => d switch
        {
            Dir.Right => 0f,
            Dir.Up => 90f,
            Dir.Left => 180f,
            _ => 270f, // Down
        };

        static void SetRotation(VisualElement ve, float degrees)
        {
            ve.style.rotate = new StyleRotate(new Rotate(new Angle(degrees, AngleUnit.Degree)));
        }

        static Dir Opposite(Dir d) => (Dir)(((int)d + 2) & 3);
    }
}
