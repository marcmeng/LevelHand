using System;
using PixelBug.ArrowLevelGenerator;
using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class ArrowLevelGeneratorAdapter
    {
        public static ArrowLevelData ToPortable(LevelDefinition definition)
        {
            if (definition == null || definition.authoredLevel == null)
                return null;

            return ToPortable(
                definition.levelId,
                definition.authoredLevel,
                ArrowDifficultyBand.Normal,
                ArrowLevelFamily.Unknown);
        }

        public static ArrowLevelData ToPortable(
            string levelId,
            AuthoredLevelData authored,
            ArrowDifficultyBand difficulty,
            ArrowLevelFamily family)
        {
            if (authored == null)
                return null;

            var portable = new ArrowLevelData(levelId, authored.width, authored.height)
            {
                difficulty = difficulty,
                family = family
            };

            if (authored.arrows != null)
            {
                for (int i = 0; i < authored.arrows.Count; i++)
                {
                    AuthoredArrowData arrow = authored.arrows[i];
                    if (arrow == null)
                        continue;

                    portable.chains.Add(new ArrowChainData(arrow.indices, arrow.colorIndex));
                }
            }

            if (authored.blockIndices != null)
                portable.blockIndices.AddRange(authored.blockIndices);

            return portable;
        }

        public static LevelDefinition CreateLevelDefinition(ArrowLevelData portable, string levelId = null)
        {
            if (portable == null)
                return null;

            var definition = ScriptableObject.CreateInstance<LevelDefinition>();
            FillLevelDefinition(definition, portable, levelId);
            return definition;
        }

        public static void FillLevelDefinition(LevelDefinition definition, ArrowLevelData portable, string levelId = null)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));
            if (portable == null)
                throw new ArgumentNullException(nameof(portable));

            definition.levelId = string.IsNullOrWhiteSpace(levelId) ? portable.levelId : levelId;
            definition.source = LevelDefinition.LevelSource.Authored;
            definition.board.width = Mathf.Max(1, portable.width);
            definition.board.height = Mathf.Max(1, portable.height);
            definition.generation.arrowCoverage = 1f;
            definition.generation.validateWithGreedy = true;
            definition.authoredLevel = ToAuthoredLevelData(portable);
        }

        public static AuthoredLevelData ToAuthoredLevelData(ArrowLevelData portable)
        {
            if (portable == null)
                return null;

            var authored = new AuthoredLevelData
            {
                width = Mathf.Max(1, portable.width),
                height = Mathf.Max(1, portable.height)
            };

            if (portable.blockIndices != null)
                authored.blockIndices.AddRange(portable.blockIndices);

            if (portable.chains != null)
            {
                for (int i = 0; i < portable.chains.Count; i++)
                {
                    ArrowChainData chain = portable.chains[i];
                    if (chain == null)
                        continue;

                    var arrow = new AuthoredArrowData
                    {
                        colorIndex = chain.colorIndex
                    };
                    arrow.indices.AddRange(chain.indices);
                    authored.arrows.Add(arrow);
                }
            }

            return authored;
        }
    }
}
