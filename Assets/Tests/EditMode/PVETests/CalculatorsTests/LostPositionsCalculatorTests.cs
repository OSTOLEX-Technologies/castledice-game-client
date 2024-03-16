using System.Collections.Generic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE.Calculators;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class LostPositionsCalculatorTests
    {
        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfArmyStateDoesntChangeAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyState = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyState);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyState);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetLostPosition_ShouldReturnEmptyList_IfUnitsWillBeAddedAndNotDestroyedAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            //Adding unit on (2, 1)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Unit, SimpleCellState.Unit}
            };
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            Assert.IsEmpty(result);
        }

        //Test case if bases will be added after move
        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfBasesWillBeAddedAndNotCapturedAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            //Adding base on (2, 1)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit}
            };
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            Assert.IsEmpty(result);
        }
        
        [Test]
        public void GetLostPositions_ShouldReturnListWithLostUnitsPositions_IfUnitsWillBeDestroyedAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new [,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            //Destroying units on (0, 1) and (0, 0)
            var armyStateAfterMove = new [,]
            {
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            var expectedPositions = new List<Vector2Int>{(0, 0), (0, 1)};
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }

        [Test]
        public void GetLostPositions_ShouldReturnListWithLostBasePositions_IfBasesWillBeCapturedAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Base, SimpleCellState.Unit, SimpleCellState.Base},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Base}
            };
            //Enemy will capture bases on (0,0) and (1,1)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Neither, SimpleCellState.Unit, SimpleCellState.Base},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Neither}
            };
            var expectedPositions = new List<Vector2Int>{(0, 0), (1, 1), (2,2)};
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }
        
        [Test]
        public void GetLostPositions_ShouldReturnListWithLostUnitsAndBasePositions_IfUnitsWillBeDestroyedAndBasesCapturedAfterMove()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Base, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Base}
            };
            //Enemy will capture base on (0,0) and destroy unit on (0,1)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Base}
            };
            var expectedPositions = new List<Vector2Int>{(0, 0), (0, 1), (1, 1)};
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }

        [Test]
        public void GetLostPositions_ShouldNotReturnPositions_WereUnitsWillBeChangedToBase()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Unit}
            };
            //Changing unit to base on (0, 0) and destroying unit on (2, 2)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Base, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Neither}
            };
            var expectedPositions = new List<Vector2Int>{(2, 2)};
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }
        
        [Test]
        public void GetLostPositions_ShouldNotReturnPositions_WereBasesWillBeChangedToUnit()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var armyStateBeforeMove = new SimpleCellState[,]
            {
                {SimpleCellState.Base, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Base}
            };
            //Changing base to unit on (0, 0) and capturing base on (2, 2)
            var armyStateAfterMove = new SimpleCellState[,]
            {
                {SimpleCellState.Unit, SimpleCellState.Unit, SimpleCellState.Neither},
                {SimpleCellState.Neither, SimpleCellState.Base, SimpleCellState.Unit},
                {SimpleCellState.Neither, SimpleCellState.Neither, SimpleCellState.Neither}
            };
            var expectedPositions = new List<Vector2Int>{(2, 2)};
            var armyStateCalculatorMock = new Mock<ISimpleArmyStateCalculator>();
            armyStateCalculatorMock.Setup(a => a.GetArmyState(player)).Returns(armyStateBeforeMove);
            armyStateCalculatorMock.Setup(a => a.GetArmyStateAfterMove(player, move)).Returns(armyStateAfterMove);
            var lostPositionsCalculator = new LostPositionsCalculator(armyStateCalculatorMock.Object);
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            CollectionAssert.AreEquivalent(expectedPositions, result);
        }
    }
}