using System;
using System.Collections.Generic;
using NUnit.Framework;
using Src.GameplayView.PlayerObjectsColor;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.PlayerObjectsColorTests
{
    public class PlayerObjectsColorConfigTests
    {
        private const string RedColorFieldName = "redPlayerColor";
        private const string BlueColorFieldName = "bluePlayerColor";
        
        [Test]
        [TestCaseSource(nameof(GetPlayerColors))]
        public void GetColor_ShouldReturnColor_AccordingToPlayerColor(PlayerColor color)
        {
            var config = ScriptableObject.CreateInstance<PlayerObjectsColorConfig>();
            var expectedColor = UnityEngine.Random.ColorHSV();
            var fieldName = GetFieldNameForColor(color);
            config.SetPrivateField(fieldName, expectedColor);
            
            var actualColor = config.GetColor(color);
            
            Assert.That(actualColor.r, Is.EqualTo(expectedColor.r).Within(0.01f));
            Assert.That(actualColor.g, Is.EqualTo(expectedColor.g).Within(0.01f));
            Assert.That(actualColor.b, Is.EqualTo(expectedColor.b).Within(0.01f));
            Assert.That(actualColor.a, Is.EqualTo(expectedColor.a).Within(0.01f));
        }

        private static string GetFieldNameForColor(PlayerColor color)
        {
            return color switch
            {
                PlayerColor.Blue => BlueColorFieldName,
                PlayerColor.Red => RedColorFieldName,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
        }
        
        public static IEnumerable<PlayerColor> GetPlayerColors()
        {
            var values = System.Enum.GetValues(typeof(PlayerColor));
            foreach (var value in values)
            {
                yield return (PlayerColor)value;
            }
        }
    }
}