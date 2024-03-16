using System;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.General.MoveConditions;

namespace Tests.EditMode.GeneralTests.MoveConditionsTests
{
    public class PositionsMoveConditionTests
    {
        private readonly Random _rnd = new Random();
        
        [Test]
        public void IsSatisfiedBy_ShouldReturnFalse_IfPositionsOfMoveIsNotInGivenList()
        {
            var movePosition = new Vector2Int(_rnd.Next(1, 10), _rnd.Next(1, 10));
            var allowedPositions = new System.Collections.Generic.List<Vector2Int>
            {
                new Vector2Int(_rnd.Next(10, 20), _rnd.Next(10, 20)),
                new Vector2Int(_rnd.Next(20, 30), _rnd.Next(20, 30)),
                new Vector2Int(_rnd.Next(30, 40), _rnd.Next(30, 40))
            };
            var move = new Mock<AbstractMove>(GetPlayer(), movePosition);
            var positionsMoveCondition = new PositionsMoveCondition(allowedPositions);
            
            var result = positionsMoveCondition.IsSatisfiedBy(move.Object);
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void IsSatisfiedBy_ShouldReturnTrue_IfPositionsOfMoveIsInGivenList()
        {
            var movePosition = new Vector2Int(_rnd.Next(1, 10), _rnd.Next(1, 10));
            var allowedPositions = new System.Collections.Generic.List<Vector2Int>
            {
                new Vector2Int(_rnd.Next(1, 10), _rnd.Next(1, 10)),
                movePosition,
                new Vector2Int(_rnd.Next(20, 30), _rnd.Next(20, 30))
            };
            var move = new Mock<AbstractMove>(GetPlayer(), movePosition);
            var positionsMoveCondition = new PositionsMoveCondition(allowedPositions);
            
            var result = positionsMoveCondition.IsSatisfiedBy(move.Object);
            
            Assert.IsTrue(result);
        }
    }
}