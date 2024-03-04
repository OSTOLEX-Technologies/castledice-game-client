using System;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.GameSituations;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.GameSituationsTests
{
    public class PositionCanBeReachedSituationTests
    {
        [Test]
        public void IsSituation_ShouldReturnTrue_IfMinReachCostIsLessThanMaxActionPoints()
        {
            var player = GetPlayer();
            var position = GetRandomVector2Int();
            var reachCostCalculatorMock = new Mock<IReachCostCalculator>();
            var maxActionPoints = new Random().Next(1, 100);
            var reachCost = new Random().Next(1, maxActionPoints - 1);
            reachCostCalculatorMock.Setup(x => x.GetMinReachCost(player, position)).Returns(reachCost);
            var situation = new PositionCanBeReachedSituation(position, reachCostCalculatorMock.Object, maxActionPoints, player);
            
            var result = situation.IsSituation();
            
            Assert.True(result);
        }
        
        [Test]
        public void IsSituation_ShouldReturnTrue_IfMinReachCostIsEqualToMaxActionPoints()
        {
            var player = GetPlayer();
            var position = GetRandomVector2Int();
            var reachCostCalculatorMock = new Mock<IReachCostCalculator>();
            var maxActionPoints = new Random().Next(1, 100);
            var reachCost = maxActionPoints;
            reachCostCalculatorMock.Setup(x => x.GetMinReachCost(player, position)).Returns(reachCost);
            var situation = new PositionCanBeReachedSituation(position, reachCostCalculatorMock.Object, maxActionPoints, player);
            
            var result = situation.IsSituation();
            
            Assert.True(result);
        }
        
        [Test]
        public void IsSituation_ShouldReturnFalse_IfMinReachCostIsGreaterThanMaxActionPoints()
        {
            var player = GetPlayer();
            var position = GetRandomVector2Int();
            var reachCostCalculatorMock = new Mock<IReachCostCalculator>();
            var maxActionPoints = new Random().Next(1, 100);
            var reachCost = maxActionPoints + 1;
            reachCostCalculatorMock.Setup(x => x.GetMinReachCost(player, position)).Returns(reachCost);
            var situation = new PositionCanBeReachedSituation(position, reachCostCalculatorMock.Object, maxActionPoints, player);
            
            var result = situation.IsSituation();
            
            Assert.False(result);
        }
    }
}