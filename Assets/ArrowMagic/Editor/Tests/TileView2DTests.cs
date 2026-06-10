using NUnit.Framework;
using System.Reflection;
using UnityEngine;

namespace PixelBug.ArrowMagic.Tests
{
    public sealed class TileView2DTests
    {
        [Test]
        public void Render_KeepsInitialDotAfterArrowIsRemoved()
        {
            var tileObject = new GameObject("tile");
            var dotObject = new GameObject("dot");

            try
            {
                dotObject.transform.SetParent(tileObject.transform);
                var dotRenderer = dotObject.AddComponent<SpriteRenderer>();
                var tile = tileObject.AddComponent<TileView2D>();

                typeof(TileView2D)
                    .GetField("emptyDot", BindingFlags.Instance | BindingFlags.NonPublic)
                    .SetValue(tile, dotRenderer);

                tile.SetInitialDotVisible(true);

                tile.Render(TileState.Arrow(Dir.Left, Dir.Right), Color.white);
                Assert.That(dotObject.activeSelf, Is.True, "Initial arrow cell should show a dot.");

                tile.Render(TileState.Empty(), Color.white);
                Assert.That(dotObject.activeSelf, Is.True, "Initial dot should remain after the arrow is removed.");

                tile.SetInitialDotVisible(false);
                tile.Render(TileState.Arrow(Dir.Left, Dir.Right), Color.white);
                Assert.That(dotObject.activeSelf, Is.False, "Cell without an initial arrow should not show a dot.");
            }
            finally
            {
                Object.DestroyImmediate(tileObject);
            }
        }
    }
}
