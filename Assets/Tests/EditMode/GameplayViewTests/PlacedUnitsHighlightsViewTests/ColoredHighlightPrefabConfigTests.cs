using NUnit.Framework;
using Src.GameplayView.Highlights;
using Tests.Utils.Mocks;
using UnityEngine;

namespace Tests.EditMode.GameplayViewTests.PlacedUnitsHighlightsViewTests
{
    public class ColoredHighlightPrefabConfigTests
    {
        [Test]
        public void GetHighlightPrefab_ShouldReturnHighlightPrefab()
        {
            var underlinePrefab = new GameObject().AddComponent<ColoredHighlightForTests>();
            var config = ScriptableObject.CreateInstance<ColoredHighlightPrefabConfig>();
            config.SetPrivateField("coloredHighlightPrefab", underlinePrefab);
            
            var prefab = config.GetHighlightPrefab();
            
            Assert.AreSame(underlinePrefab, prefab);
        }
    }
}