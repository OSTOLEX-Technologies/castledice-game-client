using System.Collections.Generic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using static Tests.Utils.ObjectCreationUtility;
using static Tests.Utils.ContentMocksCreationUtility;

namespace Tests.EditMode.PVETests
{
    public class OccupiedPositionsCalculatorTests
    {
        [Test]
        public void GetOccupiedPositions_ShouldReturnPositions_OfContentOwnedByPlayer()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var contentList = new List<Content>
            {
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player)
            };
            var expectedPositions = new List<Vector2Int>
            {
                new(0, 0),
                new(1, 1),
                new(2, 2)
            };
            for (var i = 0; i < contentList.Count; i++)
            {
                board[expectedPositions[i]].AddContent(contentList[i]);
            }
            var lostPositionsCalculator = new Mock<ILostPositionsCalculator>().Object;
            var calculator = new OccupiedPositionsCalculator(board, lostPositionsCalculator);
            
            var actualPositions = calculator.GetOccupiedPositions(player);
            
            Assert.AreEqual(expectedPositions, actualPositions);
        }
        
        [Test]
        public void GetOccupiedPositions_ShouldNotReturnPositions_OfContentNotOwnedByPlayer()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var contentList = new List<Content>
            {
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player),
                GetCellContent(),
                GetCellContent(),
                GetCellContent()
            };
            var allPositions = new List<Vector2Int>
            {
                new(0, 0),
                new(1, 1),
                new(2, 2),
                new(3, 3),
                new(4, 4),
                new(5, 5)
            };
            var expectedPositions = new List<Vector2Int>
            {
                new(0, 0),
                new(1, 1),
                new(2, 2)
            };
            for (var i = 0; i < allPositions.Count; i++)
            {
                board[allPositions[i]].AddContent(contentList[i]);
            }
            var lostPositionsCalculator = new Mock<ILostPositionsCalculator>().Object;
            var calculator = new OccupiedPositionsCalculator(board, lostPositionsCalculator);
            
            var actualPositions = calculator.GetOccupiedPositions(player);
            
            Assert.AreEqual(expectedPositions, actualPositions);
        }

        [Test]
        public void GetOccupiedPositionsAfterMove_ShouldReturnListOfPositions_WithExcludedLostPositions()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var move = GetMove();
            var contentList = new List<Content>
            {
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player),
                GetPlayerOwnedContent(player)
            };
            var contentPositions = new List<Vector2Int>
            {
                new(0, 0),
                new(1, 1),
                new(2, 2),
                new(3, 3)
            };
            var lostPositions = new List<Vector2Int>
            {
                new(1, 1),
                new(3, 3)
            };
            var expectedPositions = new List<Vector2Int>
            {
                new(0, 0),
                new(2, 2)
            };
            for (var i = 0; i < contentList.Count; i++)
            {
                board[contentPositions[i]].AddContent(contentList[i]);
            }
            var lostPositionsCalculatorMock = new Mock<ILostPositionsCalculator>();
            lostPositionsCalculatorMock.Setup(x => x.GetLostPositions(player, move)).Returns(lostPositions);
            var calculator = new OccupiedPositionsCalculator(board, lostPositionsCalculatorMock.Object);
            
            var actualPositions = calculator.GetOccupiedPositionsAfterMove(player, move);
            
            Assert.AreEqual(expectedPositions, actualPositions);
        }
    }
}