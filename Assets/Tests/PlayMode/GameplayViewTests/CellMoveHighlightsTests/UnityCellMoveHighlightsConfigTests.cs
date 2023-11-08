using NUnit.Framework;
using Src.GameplayView.CellMovesHighlights;
using static Tests.ObjectCreationUtility;
using UnityEngine;

namespace Tests.PlayMode.GameplayViewTests.CellMoveHighlightsTests
{
    public class UnityCellMoveHighlightsConfigTests
    {
        [Test]
        public void GetCellHighlightPrefab_ShouldReturnPrefab_GivenInSerializeField()
        {
            var expectedPrefab = new GameObject().AddComponent<UnityCellMoveHighlight>();
            var config = ScriptableObject.CreateInstance<UnityCellMoveHighlightsConfig>();
            SetPrivateFieldValue(expectedPrefab, config, "cellHighlight");
            
            var actualPrefab = config.GetCellHighlightPrefab();
            
            Assert.AreSame(expectedPrefab, actualPrefab);
        }
    }
}