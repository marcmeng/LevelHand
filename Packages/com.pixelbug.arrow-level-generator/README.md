# PixelBug Arrow Level Generator

Portable generation contracts and quality tools for arrow-clearing puzzle levels.

This package is intentionally split from a concrete Unity project:

- `Runtime/Model` contains the neutral level DTO (`ArrowLevelData`).
- `Runtime/Validation` contains geometry validation and a deterministic greedy escape solver.
- `Runtime/Scoring` contains coverage, outer-band, opener, short-edge, and straight-run quality checks.
- `Runtime/Planning` selects campaign packs by difficulty and family limits.
- `Runtime/Generation` contains the first portable direct rectangle generator.

The current ArrowMagic project should keep its existing `LevelDefinition` and `LevelPack` assets. A thin adapter should convert between `ArrowLevelData` and project-specific assets.

## Chain Format

`ArrowChainData.indices` is stored from head to tail.

The head direction is inferred from the first two cells:

```text
head - second = escape direction
```

For example, if the head cell is one grid cell above the second cell, the chain escapes upward.

## First Production Path

1. Generate or import candidate `ArrowLevelData`.
2. Run `ArrowLevelValidator`.
3. Run `GreedyEscapeSolver`.
4. Run `ArrowLevelQualityEvaluator`.
5. Use `ArrowCampaignPlanner` to choose a balanced pack.
6. Convert selected levels to the host project's level asset format.

## Direct Rectangle Generator

`DirectRectangleArrowLevelGenerator` is a pure C# generator. It creates layered portrait/square rectangle levels where outer chains can clear first and inner chains are unlocked by those outer removals.

```csharp
var generator = new DirectRectangleArrowLevelGenerator();
var request = new ArrowGenerationRequest
{
    levelIdPrefix = "daily",
    seed = 12345,
    count = 20,
    minWidth = 15,
    maxWidth = 22,
    minHeight = 22,
    maxHeight = 32,
    minChains = 40,
    maxChains = 90,
    targetCoverage = 0.94f,
    difficulty = ArrowDifficultyBand.Normal,
    requireGreedySolvable = true
};

IReadOnlyList<ArrowGenerationResult> results = generator.Generate(request);
```

The package does not write Unity assets by itself. Asset saving, demo scene wiring, and CSV reports belong in a Unity adapter/editor layer.
