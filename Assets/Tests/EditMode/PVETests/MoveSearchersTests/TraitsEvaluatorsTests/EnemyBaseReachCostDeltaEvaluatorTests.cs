using System;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitsEvaluatorsTests
{
    public class EnemyBaseReachCostDeltaEvaluatorTests
    {

        [Test]
        public void EvaluateTrait_ShouldReturnDifference_BetweenReachCostBeforeAndAfterMove()
        {
            var rnd = new Random();
            var move = GetMove();
            var player = GetPlayer();
            var enemyBasePosition = GetRandomVector2Int(0, 10);
            var reachCostBefore = rnd.Next(1, 10);
            var reachCostAfter = rnd.Next(1, 10);
            var expectedDelta = reachCostBefore - reachCostAfter;
            var reachCostCalculatorMock = new Mock<IReachCostCalculator>();
            reachCostCalculatorMock.Setup(r => r.GetMinReachCost(player, enemyBasePosition)).Returns(reachCostBefore);
            reachCostCalculatorMock.Setup(r => r.GetMinReachCostAfterMove(player, enemyBasePosition, move)).Returns(reachCostAfter);
            var evaluator = new EnemyBaseReachCostDeltaEvaluator(enemyBasePosition, reachCostCalculatorMock.Object, player);
            
            var result = evaluator.EvaluateTrait(move);
            
            Assert.AreEqual(expectedDelta, result);
        }
    }
}