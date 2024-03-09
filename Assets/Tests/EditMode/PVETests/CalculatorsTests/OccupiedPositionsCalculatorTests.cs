using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Vector2Int = castledice_game_logic.Math.Vector2Int;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class OccupiedPositionsCalculatorTests
    {
        [Test]
        public void GetOccupiedPositions_ShouldReturnUnitsAndBasesPositions_AccordingToCalculatedArmyState()
        {
            var player = GetPlayer();
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(new SimpleCellState[,]
            {
                { SimpleCellState.Base, SimpleCellState.Unit, SimpleCellState.Neither },
                { SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit },
                { SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit }
            });
            var expectedPositions = new List<Vector2Int>
            {
                (0, 0), (0, 1), (1, 2), (2, 2)
            };
            var occupiedPositionsCalculator = new OccupiedPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = occupiedPositionsCalculator.GetOccupiedPositions(player);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }

        [Test]
        //Army state is calculated with ISimpleArmyStateCalculator
        public void GetOccupiedPositionsAfterMove_ShouldReturnUnitsAndBasePositions_AccordingToArmyStateAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            //Making states before and after move different
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(new SimpleCellState[,]
            {
                { SimpleCellState.Neither, SimpleCellState.Unit, SimpleCellState.Neither },
                { SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit },
                { SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit }
            });
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(new SimpleCellState[,]
            {
                { SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither },
                { SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit },
                { SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit }
            });
            var expectedPositions = new List<Vector2Int>
            {
                (0, 1), (1, 1), (1, 2), (2, 2), (0,0)
            };
            var occupiedPositionsCalculator = new OccupiedPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = occupiedPositionsCalculator.GetOccupiedPositionsAfterMove(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }
    }
}