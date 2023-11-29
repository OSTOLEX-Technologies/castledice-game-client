using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.CellsContentTests.ContentViewsCreationTests.CastleViewCreationTests
{
    public class CastleModelPrefabConfigTests
    {
        [Test]
        [TestCaseSource(nameof(GetPlayerColors))]
        public void GetCastleModelPrefab_ShouldReturnPrefab_AccordingToColor(PlayerColor playerColor)
        {
            var prefab = new GameObject();
            var config = ScriptableObject.CreateInstance<CastleModelPrefabConfig>();
            SetPrivateFieldValue(prefab, config, GetPropertyNameForColor(playerColor));
            
            var result = config.GetCastleModelPrefab(playerColor);
            
            Assert.AreSame(prefab, result);
        }

        private static string GetPropertyNameForColor(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Blue:
                    return "blueCastleModelPrefab";
                case PlayerColor.Red:
                    return "redCastleModelPrefab";
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
        
        public static IEnumerable<PlayerColor> GetPlayerColors()
        {
            var colors = Enum.GetValues(typeof(PlayerColor));
            foreach (var color in colors)
            {
                yield return (PlayerColor) color;
            }
        }
    }
}