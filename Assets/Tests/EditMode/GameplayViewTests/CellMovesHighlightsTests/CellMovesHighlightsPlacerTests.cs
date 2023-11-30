using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellMovesHighlights;
using Tests.Utils.Mocks;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightsPlacerTests
    {
        [Test]
        public void PlaceHighlights_ShouldPlaceObjectsFromGivenFactory_ToEveryCellOnGrid([Values(3, 10, 15, 23, 11)]int cellsCount)
        {
            var factoryMock = new Mock<IUnityCellMoveHighlightsFactory>();
            var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
            factoryMock.Setup(f => f.GetCellMoveHighlight()).Returns(highlight);
            var grid = new GridForTests();
            for (int i = 0; i < cellsCount; i++)
            {
                grid.AddCell((i, i), Vector3.zero);
            }
            var placer = new CellMovesHighlightsPlacer(grid, factoryMock.Object);
            
            placer.PlaceHighlights();
            
            foreach (var cell in grid)
            {
                var testCell = cell as GridCellForTests;
                Assert.Contains(highlight.gameObject, testCell.Children);
            }
        }

        [TestCaseSource(nameof(PositionsListCases))]
        public void PlaceHighlights_ShouldReturnDictionaryOfHighlights_WithAppropriatePositions(
            List<Vector2Int> positions)
        {
            var factoryMock = new Mock<IUnityCellMoveHighlightsFactory>();
            var highlight = new GameObject().AddComponent<UnityCellMoveHighlight>();
            factoryMock.Setup(f => f.GetCellMoveHighlight()).Returns(highlight);
            var grid = new GridForTests();
            foreach (var position in positions)
            {
                grid.AddCell(position, Vector3.zero);
            }
            var placer = new CellMovesHighlightsPlacer(grid, factoryMock.Object);
            
            var highlights = placer.PlaceHighlights();
            
            foreach (var position in positions)
            {
                Assert.IsTrue(highlights.ContainsKey(position));
            }
        }

        public static object[] PositionsListCases =
        {
            new object[]
            {
                new List<Vector2Int>
                {
                    (1, 2), (3, 4), (5, 6)
                }
            }, 
            new object[]
            {
                new List<Vector2Int>
                {
                    (3, 4), (5, 6)
                }
            },
            new object[]
            {
                new List<Vector2Int>
                {
                    (1, 2), (3, 4), (5, 6), (7, 8), (9, 10)
                }
            }
        };
    }
}