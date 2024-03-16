using System;
using System.Collections.Generic;
using System.Linq;
using castledice_game_logic.Math;
using Moq;
using static Tests.Utils.ObjectCreationUtility;
using NUnit.Framework;
using Src.PVE;
using Src.PVE.Calculators;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class ReachCostCalculatorTests
    {
        [Test]
        public void GetMinReachCost_ShouldReturnIntMaxValue_IfPlayerHasNoOccupiedPositions()
        {
            var player = GetPlayer();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(new List<Vector2Int>());
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCost(player, GetRandomVector2Int());
            
            Assert.AreEqual(int.MaxValue, result);
        }

        [Test]
        public void GetMinReachCost_ShouldReturnPathCost_IfPlayerHasOneOccupiedPosition()
        {
            var player = GetPlayer();
            var position = GetRandomVector2Int();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(new List<Vector2Int> {position});
            var costsCalculatorMock = new Mock<IBoardCostsCalculator>();
            var costsMatrix = new int[1, 1]{{1}};
            costsCalculatorMock.Setup(x => x.GetCosts(player)).Returns(costsMatrix);
            var pathCost = new Random().Next(1, 100);
            var minCostSearcherMock = new Mock<IMatrixPathMinCostSearcher>();
            minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, position, position)).Returns(pathCost);
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object,
                CostsCalculator = costsCalculatorMock.Object,
                MinCostSearcher = minCostSearcherMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCost(player, position);
            
            Assert.AreEqual(pathCost, result);
        }

        [Test]
        public void GetMinReachCost_ShouldReturnMinimalFromFoundPathCosts_IfPlayerHasMultipleOccupiedPositions()
        {
            var player = GetPlayer();
            var position = GetRandomVector2Int();
            var occupiedPositions = new List<Vector2Int> {GetRandomVector2Int(), GetRandomVector2Int(), GetRandomVector2Int()};
            var reachCosts = new List<int> {new Random().Next(1, 100), new Random().Next(1, 100), new Random().Next(1, 100)};
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(occupiedPositions);
            var costsCalculatorMock = new Mock<IBoardCostsCalculator>();
            var costsMatrix = new int[1, 1]{{1}};
            costsCalculatorMock.Setup(x => x.GetCosts(player)).Returns(costsMatrix);
            var minCostSearcherMock = new Mock<IMatrixPathMinCostSearcher>();
            minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, It.IsAny<Vector2Int>(), It.IsAny<Vector2Int>())).Returns(int.MaxValue);
            for (var i = 0; i < occupiedPositions.Count; i++)
            {
                minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, occupiedPositions[i], position)).Returns(reachCosts[i]);
            }
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object,
                CostsCalculator = costsCalculatorMock.Object,
                MinCostSearcher = minCostSearcherMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCost(player, position);
            
            Assert.AreEqual(reachCosts.Min(), result);
        }

        [Test]
        public void GetMinReachCostAfterMove_ShouldReturnIntMaxValue_IfPlayerWillHaveNoOccupiedPositionsAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove();
            var position = GetRandomVector2Int();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            var positionsBeforeMove = new List<Vector2Int> {GetRandomVector2Int(), GetRandomVector2Int(), GetRandomVector2Int()};
            var positionsAfterMove = new List<Vector2Int>();
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(positionsBeforeMove);
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositionsAfterMove(player, move)).Returns(positionsAfterMove);
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCostAfterMove(player, position, move);
            
            Assert.AreEqual(int.MaxValue, result);
        }

        [Test]
        //This test also checks if the method uses correct matrix from IBoardCostsCalculator
        public void GetMinReachCostAfterMove_ShouldReturnPathCost_IfPlayerWillHaveOnlyOneOccupiedPositionAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove();
            var position = GetRandomVector2Int();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            var positionsBeforeMove = new List<Vector2Int> {GetRandomVector2Int(), GetRandomVector2Int(), GetRandomVector2Int()};
            var positionsAfterMove = new List<Vector2Int> {GetRandomVector2Int()};
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(positionsBeforeMove);
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositionsAfterMove(player, move)).Returns(positionsAfterMove);
            var costsCalculatorMock = new Mock<IBoardCostsCalculator>();
            var costsMatrix = new int[1, 1]{{1}};
            costsCalculatorMock.Setup(x => x.GetCostsAfterMove(player, move)).Returns(costsMatrix);
            var pathCost = new Random().Next(1, 100);
            var minCostSearcherMock = new Mock<IMatrixPathMinCostSearcher>();
            minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, It.IsAny<Vector2Int>(), It.IsAny<Vector2Int>())).Returns(int.MaxValue);
            minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, positionsAfterMove[0], position)).Returns(pathCost);
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object,
                CostsCalculator = costsCalculatorMock.Object,
                MinCostSearcher = minCostSearcherMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCostAfterMove(player, position, move);
            
            Assert.AreEqual(pathCost, result);
        }
        
        [Test]
        public void GetMinReachCostAfterMove_ShouldReturnMinimalFromFoundPathCosts_IfPlayerWillHaveMultipleOccupiedPositionsAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove();
            var position = GetRandomVector2Int();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            var positionsBeforeMove = new List<Vector2Int> {GetRandomVector2Int(), GetRandomVector2Int(), GetRandomVector2Int()};
            var positionsAfterMove = new List<Vector2Int> {GetRandomVector2Int(), GetRandomVector2Int(), GetRandomVector2Int()};
            var minReachCost = new Random().Next(1, 100);
            var reachCosts = new List<int> {minReachCost + new Random().Next(1, 100), minReachCost, minReachCost + new Random().Next(1, 100)};
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositions(player)).Returns(positionsBeforeMove);
            occupiedPositionsCalculatorMock.Setup(x => x.GetOccupiedPositionsAfterMove(player, move)).Returns(positionsAfterMove);
            var costsCalculatorMock = new Mock<IBoardCostsCalculator>();
            var costsMatrix = new int[1, 1]{{1}};
            costsCalculatorMock.Setup(x => x.GetCostsAfterMove(player, move)).Returns(costsMatrix);
            var minCostSearcherMock = new Mock<IMatrixPathMinCostSearcher>();
            minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, It.IsAny<Vector2Int>(), It.IsAny<Vector2Int>())).Returns(int.MaxValue);
            for (var i = 0; i < positionsAfterMove.Count; i++)
            {
                minCostSearcherMock.Setup(x => x.GetMinCost(costsMatrix, positionsAfterMove[i], position)).Returns(reachCosts[i]);
            }
            var calculator = new ReachCostCalculatorBuilder
            {
                OccupiedPositionsCalculator = occupiedPositionsCalculatorMock.Object,
                CostsCalculator = costsCalculatorMock.Object,
                MinCostSearcher = minCostSearcherMock.Object
            }.Build();
            
            var result = calculator.GetMinReachCostAfterMove(player, position, move);
            
            Assert.AreEqual(minReachCost, result);
        }

        private class ReachCostCalculatorBuilder
        {
            public IBoardCostsCalculator CostsCalculator { get; set; }
            public IOccupiedPositionsCalculator OccupiedPositionsCalculator { get; set; }
            public IMatrixPathMinCostSearcher MinCostSearcher { get; set; }
            
            public ReachCostCalculatorBuilder()
            {
                CostsCalculator = new Mock<IBoardCostsCalculator>().Object;
                OccupiedPositionsCalculator = new Mock<IOccupiedPositionsCalculator>().Object;
                MinCostSearcher = new Mock<IMatrixPathMinCostSearcher>().Object;
            }
            
            public ReachCostCalculator Build()
            {
                return new ReachCostCalculator(CostsCalculator, OccupiedPositionsCalculator, MinCostSearcher);
            }
        }
    }
}