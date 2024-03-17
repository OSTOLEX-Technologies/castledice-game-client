using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using Src.PVE.GameSituations;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.GameSituationsTests
{
    public class OccupiedPositionsSituationTests
    {
        [Test]
        public void IsSituation_ShouldReturnTrue_IfExpectedPositionsAreSameWithPositionsFromCalculator()
        {
            var positionsList = GetRandomVector2IntList(0, 10, 10);
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>())).Returns(positionsList);
            var situation = new OccupiedPositionsSituation(positionsList, occupiedPositionsCalculatorMock.Object, GetPlayer());
            
            var isSituation = situation.IsSituation();
            
            Assert.True(isSituation);
        }
        
        [Test]
        public void IsSituation_ShouldReturnFalse_IfSomePositionsFromCalculatorDifferFromExpected()
        {
            var calculatedPositionsList = GetRandomVector2IntList(0, 10, 10);
            var expectedPositionsList = new List<Vector2Int>(
                GetRandomVector2IntList(0, 10, 5));
            expectedPositionsList.AddRange(GetRandomVector2IntList(10, 20, 5));
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>())).Returns(calculatedPositionsList);
            var situation = new OccupiedPositionsSituation(expectedPositionsList, occupiedPositionsCalculatorMock.Object, GetPlayer());
            
            var isSituation = situation.IsSituation();
            
            Assert.False(isSituation);
        }

        [Test]
        public void IsSituation_ShouldReturnFalse_IfExpectedListIsBiggerThanListFromCalculator()
        {
            var positionsList = GetRandomVector2IntList(0, 10, 10);
            var expectedList = new List<Vector2Int>(positionsList);
            expectedList.Add(GetRandomVector2Int(0, 10));
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>())).Returns(positionsList);
            var situation = new OccupiedPositionsSituation(expectedList, occupiedPositionsCalculatorMock.Object, GetPlayer());
            
            var isSituation = situation.IsSituation();
            
            Assert.False(isSituation);
        }
        
        [Test]
        public void IsSituation_ShouldReturnFalse_IfExpectedListIsSmallerThanListFromCalculator()
        {
            var positionsList = GetRandomVector2IntList(0, 10, 10);
            var expectedList = new List<Vector2Int>(positionsList);
            expectedList.RemoveAt(0);
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>())).Returns(positionsList);
            var situation = new OccupiedPositionsSituation(expectedList, occupiedPositionsCalculatorMock.Object, GetPlayer());
            
            var isSituation = situation.IsSituation();
            
            Assert.False(isSituation);
        }
        
        [Test]
        public void IsSituation_ShouldCallGetOccupiedPositions_Once()
        {
            var positionsList = GetRandomVector2IntList(0, 10, 10);
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>())).Returns(positionsList);
            var situation = new OccupiedPositionsSituation(positionsList, occupiedPositionsCalculatorMock.Object, GetPlayer());
            
            situation.IsSituation();
            
            occupiedPositionsCalculatorMock.Verify(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(It.IsAny<Player>()), Times.Once);
        }

        [Test]
        public void IsSituation_ShouldCallGetOccupiedPositions_WithPlayerGivenInConstructor()
        {
            var player = GetPlayer();
            var occupiedPositionsCalculatorMock = new Mock<IOccupiedPositionsCalculator>();
            occupiedPositionsCalculatorMock.Setup(o => o.GetOccupiedPositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
            var situation = new OccupiedPositionsSituation(new List<Vector2Int>(), occupiedPositionsCalculatorMock.Object, player);
            
            situation.IsSituation();
            
            occupiedPositionsCalculatorMock.Verify(occupiedPositionsCalculator => occupiedPositionsCalculator.GetOccupiedPositions(player));
        }
    }
}