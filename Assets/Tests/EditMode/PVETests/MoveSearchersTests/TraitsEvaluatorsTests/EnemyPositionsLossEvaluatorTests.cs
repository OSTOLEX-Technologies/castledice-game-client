using System;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.MoveSearchers.TraitBasedSearchers.TraitsEvaluators;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.MoveSearchersTests.TraitsEvaluatorsTests
{
    public class EnemyPositionsLossEvaluatorTests
    {
        [Test]
        public void EvaluateTrait_ShouldReturnLostPositionsCount_ForEnemy()
        {
            var move = GetMove();
            var enemy = GetPlayer();
            var count = new Random().Next(1, 10);
            var lostPositions = GetRandomVector2IntList(0, 10, count);
            var lostPositionsCalculatorMock = new Mock<ILostPositionsCalculator>();
            lostPositionsCalculatorMock.Setup(l => l.GetLostPositions(enemy, It.IsAny<AbstractMove>())).Returns(lostPositions);
            var evaluator = new EnemyPositionsLossEvaluator(enemy, lostPositionsCalculatorMock.Object);
            
            var result = evaluator.EvaluateTrait(move);
            
            Assert.AreEqual(count, result);
        }
    }
}