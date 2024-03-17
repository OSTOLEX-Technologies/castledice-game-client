using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE;
using Src.PVE.Checkers;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests
{
    public class UnitsPositionsSearcherTests
    {
        [Test]
        public void GetUnitsPositions_ShouldReturnEmptyList_IfNoContentOnBoard()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var unitCheckerMock = new Mock<IPlayerUnitChecker>();
            var unitsPositionsSearcher = new UnitsPositionsSearcher(board, unitCheckerMock.Object);
            
            var result = unitsPositionsSearcher.GetUnitsPositions(player);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetUnitsPositions_ShouldReturnEmptyList_IfNoContentOnBoardSatisfiesCondition()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var someContent = GetContent();
            board[0, 0].AddContent(someContent);
            board[0, 1].AddContent(someContent);
            var unitCheckerMock = new Mock<IPlayerUnitChecker>();
            unitCheckerMock.Setup(x => x.IsPlayerUnit(someContent, player)).Returns(false);
            var unitsPositionsSearcher = new UnitsPositionsSearcher(board, unitCheckerMock.Object);
            
            var result = unitsPositionsSearcher.GetUnitsPositions(player);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetUnitPositions_ShouldReturnPositionsOfContent_ThatSatisfiesCondition()
        {
            var rnd = new Random();
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var notSatisfyingContent = GetContent();
            var satisfyingContent = GetContent();
            var satisfyingContentPositions = new List<Vector2Int> {(rnd.Next(1, 3), rnd.Next(1, 3)), (rnd.Next(3, 6), rnd.Next(1, 3))};
            var notSatisfyingContentPositions = new List<Vector2Int> {(rnd.Next(1, 3), rnd.Next(3, 6)), (rnd.Next(3, 6), rnd.Next(3, 6))};
            satisfyingContentPositions.ForEach(pos => board[pos.X, pos.Y].AddContent(satisfyingContent));
            notSatisfyingContentPositions.ForEach(pos => board[pos.X, pos.Y].AddContent(notSatisfyingContent));
            var unitCheckerMock = new Mock<IPlayerUnitChecker>();
            unitCheckerMock.Setup(x => x.IsPlayerUnit(satisfyingContent, player)).Returns(true);
            unitCheckerMock.Setup(x => x.IsPlayerUnit(notSatisfyingContent, player)).Returns(false);
            var unitsPositionsSearcher = new UnitsPositionsSearcher(board, unitCheckerMock.Object);
            
            var result = unitsPositionsSearcher.GetUnitsPositions(player);
            
            Assert.AreEqual(satisfyingContentPositions, result);
        }
    }
}