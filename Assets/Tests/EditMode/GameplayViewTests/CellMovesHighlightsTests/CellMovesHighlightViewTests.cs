using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayView.CellMovesHighlights;

namespace Tests.EditMode.GameplayViewTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightViewTests
    {
        [TestCaseSource(nameof(CellMovesListCases))]
        public void HighlightCellMoves_ShouldCallShowHighlightWithAppropriateParameter_ForEachGivenCellMove(
            List<CellMove> cellMoves)
        {
            var dictionary = new Dictionary<Vector2Int, ICellMoveHighlight>();
            var highlightMocks = new List<Mock<ICellMoveHighlight>>();
            foreach (var cellMove in cellMoves)
            {
                var highlightMock = new Mock<ICellMoveHighlight>();
                dictionary.Add(cellMove.Cell.Position, highlightMock.Object);
                highlightMocks.Add(highlightMock);
            }

            var cellHighlightsPlacerMock = new Mock<ICellHighlightsPlacer>();
            cellHighlightsPlacerMock.Setup(c => c.PlaceHighlights()).Returns(dictionary);
            var cellMovesHighlightView = new CellMovesHighlightView(cellHighlightsPlacerMock.Object);

            cellMovesHighlightView.HighlightCellMoves(cellMoves);

            for (int i = 0; i < highlightMocks.Count; i++)
            {
                var mock = highlightMocks[i];
                mock.Verify(h => h.ShowHighlight(cellMoves[i].MoveType), Times.Once);
            }
        }

        [Test]
        public void HighlightCelLMoves_ShouldThrowArgumentException_IfGivenCellMoveHasUnknownPosition()
        {
            var cellHighlightsPlacerMock = new Mock<ICellHighlightsPlacer>();
            cellHighlightsPlacerMock.Setup(c => c.PlaceHighlights()).Returns(new Dictionary<Vector2Int, ICellMoveHighlight>());
            var cellMovesHighlightView = new CellMovesHighlightView(cellHighlightsPlacerMock.Object);
            var list = new List<CellMove> {new CellMove(new Cell((1, 1)), MoveType.Capture)};
            
            Assert.Throws<ArgumentException>(() => cellMovesHighlightView.HighlightCellMoves(list));
        }

        [Test]
        public void HideHighlights_ShouldCallHideAllHighlights_OnEachCellHighlightObject([Values(1, 2, 3, 4, 5)] int highlightCount)
        {
            var dictionary = new Dictionary<Vector2Int, ICellMoveHighlight>();
            var highlightMocks = new List<Mock<ICellMoveHighlight>>();
            for (int i = 0; i < highlightCount; i++)
            {
                var highlightMock = new Mock<ICellMoveHighlight>();
                highlightMocks.Add(highlightMock);
                dictionary.Add(new Vector2Int(i, i), highlightMock.Object);    
            }
            var cellHighlightsPlacerMock = new Mock<ICellHighlightsPlacer>();
            cellHighlightsPlacerMock.Setup(c => c.PlaceHighlights()).Returns(dictionary);
            var cellMovesHighlightView = new CellMovesHighlightView(cellHighlightsPlacerMock.Object);
            
            cellMovesHighlightView.HideHighlights();
            
            foreach (var highlightMock in highlightMocks)
            {
                highlightMock.Verify(h => h.HideAllHighlights(), Times.Once);
            }
        }

        public static object[] CellMovesListCases =
        {
            new object[]
            {
                new List<CellMove>
                {
                    new CellMove(new Cell(new Vector2Int(1, 1)), MoveType.Place),
                    new CellMove(new Cell(new Vector2Int(2, 2)), MoveType.Upgrade),
                    new CellMove(new Cell(new Vector2Int(3, 3)), MoveType.Remove),
                    new CellMove(new Cell(new Vector2Int(3, 1)), MoveType.Capture),
                    new CellMove(new Cell(new Vector2Int(1, 3)), MoveType.Replace)
                }
            }
        };
    }
}