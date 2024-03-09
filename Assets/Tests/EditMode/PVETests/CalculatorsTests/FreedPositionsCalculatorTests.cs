using System;
using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using static Tests.Utils.ObjectCreationUtility;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.MoveConditions;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class FreedPositionsCalculatorTests
    {
        [Test]
        public void GetFreedPositions_ShouldReturnListWithMovePosition_IfMoveSatisfiesFreeingCondition()
        {
            var moveMock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            var move = moveMock.Object;
            var cellFreeingConditionMock = new Mock<IMoveCondition>();
            cellFreeingConditionMock.Setup(c => c.IsSatisfiedBy(move)).Returns(true);
            var freedPositionsCalculator = new FreedPositionsCalculatorBuilder()
                .WithCellFreeingCondition(cellFreeingConditionMock.Object)
                .Build();
            
            var result = freedPositionsCalculator.GetFreedPositions(move);
            
            Assert.Contains(move.Position, result);
        }
        
        [Test]
        public void GetFreedPositions_ShouldReturnListWithoutMovePosition_IfMoveDoesNotSatisfyFreeingCondition()
        {
            var moveMock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            var move = moveMock.Object;
            var cellFreeingConditionMock = new Mock<IMoveCondition>();
            cellFreeingConditionMock.Setup(c => c.IsSatisfiedBy(move)).Returns(false);
            var freedPositionsCalculator = new FreedPositionsCalculatorBuilder()
                .WithCellFreeingCondition(cellFreeingConditionMock.Object)
                .Build();
            
            var result = freedPositionsCalculator.GetFreedPositions(move);
            
            Assert.IsFalse(result.Contains(move.Position));
        }

        [Test]
        public void GetFreedPositions_ShouldReturnListWithPositions_WhichWillBeLostByPlayers()
        {
            var moveMock = new Mock<AbstractMove>(GetPlayer(), new Vector2Int(0, 0));
            var rnd = new Random();
            var move = moveMock.Object;
            var firstPlayer = GetPlayer();
            var secondPlayer = GetPlayer();
            var firstPlayerLostPosition = new Vector2Int(rnd.Next(1, 10), rnd.Next(1, 10));
            var secondPlayerLostPosition = new Vector2Int(rnd.Next(1, 10), rnd.Next(1, 10));
            var expectedPositions = new List<Vector2Int> {firstPlayerLostPosition, secondPlayerLostPosition};
            var lostPositionsCalculatorMock = new Mock<ILostPositionsCalculator>();
            lostPositionsCalculatorMock.Setup(l => l.GetLostPositions(firstPlayer, move)).Returns(new List<Vector2Int> {firstPlayerLostPosition});
            lostPositionsCalculatorMock.Setup(l => l.GetLostPositions(secondPlayer, move)).Returns(new List<Vector2Int> {secondPlayerLostPosition});
            var freedPositionsCalculator = new FreedPositionsCalculatorBuilder()
                .WithLostPositionsCalculator(lostPositionsCalculatorMock.Object)
                .WithPlayers(new List<Player> {firstPlayer, secondPlayer})
                .Build();
            
            var result = freedPositionsCalculator.GetFreedPositions(move);
            
            Assert.AreEqual(expectedPositions, result);
        }
        
        private class FreedPositionsCalculatorBuilder
        {
            private ILostPositionsCalculator _lostPositionsCalculator;
            private IMoveCondition _cellFreeingCondition;
            private List<Player> _players;
            
            public FreedPositionsCalculatorBuilder()
            {
                var lostPositionsCalculatorMock = new Mock<ILostPositionsCalculator>();
                _lostPositionsCalculator = lostPositionsCalculatorMock.Object;
                _cellFreeingCondition = new Mock<IMoveCondition>().Object;
                _players = new List<Player>();
            }
            
            public FreedPositionsCalculatorBuilder WithLostPositionsCalculator(ILostPositionsCalculator lostPositionsCalculator)
            {
                _lostPositionsCalculator = lostPositionsCalculator;
                return this;
            }
            
            public FreedPositionsCalculatorBuilder WithCellFreeingCondition(IMoveCondition cellFreeingCondition)
            {
                _cellFreeingCondition = cellFreeingCondition;
                return this;
            }
            
            public FreedPositionsCalculatorBuilder WithPlayers(List<Player> players)
            {
                _players = players;
                return this;
            }
            
            public FreedPositionsCalculator Build()
            {
                return new FreedPositionsCalculator(_lostPositionsCalculator, _players, _cellFreeingCondition);
            }
        }
    }
}