using NUnit.Framework;
using Src.GameplayView.CellMovesHighlights;
using UnityEngine;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayViewTests.CellMovesHighlightsTests
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