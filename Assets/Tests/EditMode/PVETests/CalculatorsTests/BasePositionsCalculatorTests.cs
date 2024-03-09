using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameObjects;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.Checkers;
using Src.PVE.MoveConditions;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class BasePositionsCalculatorTests
    {
        [Test]
        public void GetBasePositions_ShouldReturnEmptyList_IfNoContentOnBoard()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var baseCheckerMock = new Mock<IPlayerBaseChecker>();
            baseCheckerMock.Setup(x => x.IsPlayerBase(It.IsAny<Content>(), player)).Returns(true);
            var basePositionsCalculator = new BasePositionsCalculatorBuilder
            {
                BaseChecker = baseCheckerMock.Object,
                Board = board
            }.Build();
            
            var result = basePositionsCalculator.GetBasePositions(player);
            
            Assert.IsEmpty(result);
        }
        
        [Test]
        public void GetBasePositions_ShouldReturnEmptyList_IfNoContentOnBoardSatisfiesCondition()
        {
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var someContent = GetContent();
            board[0, 0].AddContent(someContent);
            board[0, 1].AddContent(someContent);
            var baseCheckerMock = new Mock<IPlayerBaseChecker>();
            baseCheckerMock.Setup(x => x.IsPlayerBase(someContent, player)).Returns(false);
            var basePositionsCalculator = new BasePositionsCalculatorBuilder
            {
                BaseChecker = baseCheckerMock.Object,
                Board = board
            }.Build();
            
            var result = basePositionsCalculator.GetBasePositions(player);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetBasePositions_ShouldReturnPositionsOfContent_ThatSatisfiesCondition()
        {
            var rnd = new Random();
            var board = GetFullNByNBoard(10);
            var player = GetPlayer();
            var notSatisfyingContent = GetContent();
            var satisfyingContent = GetContent();
            var satisfyingContentPositions = new List<Vector2Int>
                { (rnd.Next(1, 3), rnd.Next(1, 3)), (rnd.Next(3, 6), rnd.Next(1, 3)) };
            var notSatisfyingContentPositions = new List<Vector2Int>
                { (rnd.Next(1, 3), rnd.Next(3, 6)), (rnd.Next(3, 6), rnd.Next(3, 6)) };
            satisfyingContentPositions.ForEach(pos => board[pos.X, pos.Y].AddContent(satisfyingContent));
            notSatisfyingContentPositions.ForEach(pos => board[pos.X, pos.Y].AddContent(notSatisfyingContent));
            var baseCheckerMock = new Mock<IPlayerBaseChecker>();
            baseCheckerMock.Setup(x => x.IsPlayerBase(satisfyingContent, player)).Returns(true);
            baseCheckerMock.Setup(x => x.IsPlayerBase(notSatisfyingContent, player)).Returns(false);
            var basePositionsCalculator = new BasePositionsCalculatorBuilder
            {
                BaseChecker = baseCheckerMock.Object,
                Board = board
            }.Build();

            var result = basePositionsCalculator.GetBasePositions(player);

            Assert.AreEqual(satisfyingContentPositions, result);
        }

        [Test]
        public void GetBasePositionsAfterMove_ShouldIncludePositionsOfNewBase_IfMoveWillCaptureItForPlayer()
        {
            var player = GetPlayer();
            var positionToCapture = (1, 1);
            var capturingMove = GetMove(player, positionToCapture);
            var baseToCapture = GetPlayerOwnedContent();
            var board = GetFullNByNBoard(10);
            board[positionToCapture].AddContent(baseToCapture);
            var expectedPositions = new List<Vector2Int> { positionToCapture };
            var playerBaseCheckerMock = new Mock<IPlayerBaseChecker>();
            //This base does not belong to player before move
            playerBaseCheckerMock.Setup(x => x.IsPlayerBase(baseToCapture, player)).Returns(false); 
            var baseCaptureCheckerMock = new Mock<IMoveCondition>();
            //This move will capture base
            baseCaptureCheckerMock.Setup(x => x.IsSatisfiedBy(capturingMove)).Returns(true);
            var basePositionsCalculator = new BasePositionsCalculatorBuilder
            {
                BaseChecker = playerBaseCheckerMock.Object,
                BaseCaptureCondition = baseCaptureCheckerMock.Object,
                Board = board
            }.Build();
            
            var result = basePositionsCalculator.GetBasePositionsAfterMove(player, capturingMove);
            
            Assert.AreEqual(expectedPositions, result);
        }
        
        [Test]
        public void GetBasePositionsAfterMove_ShouldExcludePositionOfBase_OnWhichMoveWillCaptureItForEnemy()
        {
            var player = GetPlayer();
            var positionToCapture = (1, 1);
            var firstPlayerBase = GetPlayerOwnedContent();
            var secondPlayerBase = GetPlayerOwnedContent();
            var capturingMove = GetMove(GetPlayer(), positionToCapture);
            var board = GetFullNByNBoard(10);
            board[1, 1].AddContent(firstPlayerBase);
            board[2, 2].AddContent(secondPlayerBase);
            var expectedPositions = new List<Vector2Int> { (2, 2) };
            var baseCheckerMock = new Mock<IPlayerBaseChecker>();
            baseCheckerMock.Setup(x => x.IsPlayerBase(firstPlayerBase, player)).Returns(true);
            baseCheckerMock.Setup(x => x.IsPlayerBase(secondPlayerBase, player)).Returns(true);
            var baseCaptureCheckerMock = new Mock<IMoveCondition>();
            baseCaptureCheckerMock.Setup(x => x.IsSatisfiedBy(capturingMove)).Returns(true);
            var basePositionsCalculator = new BasePositionsCalculatorBuilder
            {
                BaseCaptureCondition = baseCaptureCheckerMock.Object,
                Board = board,
                BaseChecker = baseCheckerMock.Object
            }.Build();
            
            var result = basePositionsCalculator.GetBasePositionsAfterMove(player, capturingMove);
            
            Assert.AreEqual(expectedPositions, result);
        }

        private class BasePositionsCalculatorBuilder
        {
            public Board Board { get; set; }
            public IPlayerBaseChecker BaseChecker { get; set; }
            public IMoveCondition BaseCaptureCondition { get; set; }
            
            public BasePositionsCalculatorBuilder()
            {
                Board = GetFullNByNBoard(10);
                BaseChecker = new Mock<IPlayerBaseChecker>().Object;
                BaseCaptureCondition = new Mock<IMoveCondition>().Object;
            }
            
            public BasePositionsCalculator Build()
            {
                return new BasePositionsCalculator(Board, BaseChecker, BaseCaptureCondition);
            }
        }
    }
}