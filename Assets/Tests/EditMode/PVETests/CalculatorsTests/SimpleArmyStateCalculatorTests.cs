using System;
using System.Collections.Generic;
using castledice_game_logic;
using static Tests.Utils.ObjectCreationUtility;
using static Tests.Utils.ContentMocksCreationUtility;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.PVE;
using Src.PVE.Calculators;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class SimpleArmyStateCalculatorTests
    {
        [Test]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(-1, -1)]
        [TestCase(-13, -2)]
        public void Constructor_ShouldThrowArgumentException_IfInvalidBoardSizeGiven(int x, int y)
        {
            var boardSize = new Vector2Int(x, y);
            
            Assert.Throws<ArgumentException>(() => new SimpleArmyStateCalculatorBuilder{BoardSize = boardSize}.Build() );
        }

        [Test]
        public void GetArmyState_ShouldReturnTowDimensionalArray_CorrespondingToBoardSize()
        {
            var rnd = new Random();
            var boardSize = new Vector2Int(rnd.Next(5, 15), rnd.Next(5, 15));
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder {BoardSize = boardSize}.Build();
            
            var result = simpleArmyStateCalculator.GetArmyState(GetPlayer());
            
            Assert.AreEqual(boardSize.X, result.GetLength(0));
            Assert.AreEqual(boardSize.Y, result.GetLength(1));
        }
        
        [Test]
        public void GetArmyStateAfterMove_ShouldReturnTowDimensionalArray_CorrespondingToBoardSize()
        {
            var rnd = new Random();
            var boardSize = new Vector2Int(rnd.Next(5, 15), rnd.Next(5, 15));
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder {BoardSize = boardSize}.Build();
            
            var result = simpleArmyStateCalculator.GetArmyStateAfterMove(GetPlayer(), GetMove(GetPlayer()));
            
            Assert.AreEqual(boardSize.X, result.GetLength(0));
            Assert.AreEqual(boardSize.Y, result.GetLength(1));
        }

        [Test]
        public void GetArmyState_ShouldSetUnitsMarks_OnPositionsFromUnitsSearcher()
        {
            var player = GetPlayer();
            var boardSize = new Vector2Int(10, 10);
            var unitsPositions = GetRandomVector2IntList(0, 9, 10);
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            foreach (var position in unitsPositions)
            {
                expectedArmyState[position.X, position.Y] = SimpleCellState.Unit;
            }
            var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
            unitsPositionsSearcherMock.Setup(u => u.GetUnitsPositions(player)).Returns(unitsPositions);
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder
            {
                BoardSize = boardSize,
                UnitsPositionsSearcher = unitsPositionsSearcherMock.Object
            }.Build();
            
            var result = simpleArmyStateCalculator.GetArmyState(player);
            
            Assert.AreEqual(expectedArmyState, result);
        }
        
        [Test]
        public void GetArmyState_ShouldSetBaseMarks_OnPositionsFromBasePositionsCalculator()
        {
            var player = GetPlayer();
            var boardSize = new Vector2Int(10, 10);
            var basePositions = GetRandomVector2IntList(0, 9, 10);
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            foreach (var position in basePositions)
            {
                expectedArmyState[position.X, position.Y] = SimpleCellState.Base;
            }
            var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
            basePositionsCalculatorMock.Setup(b => b.GetBasePositions(player)).Returns(basePositions);
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder
            {
                BoardSize = boardSize,
                BasePositionsCalculator = basePositionsCalculatorMock.Object
            }.Build();
            
            var result = simpleArmyStateCalculator.GetArmyState(player);
            
            Assert.AreEqual(expectedArmyState, result);
        }
        
        [Test]
        public void GetArmyState_ShouldSetEverythingToNeither_IfNoUnitsAndBasesPresent()
        {
            var player = GetPlayer();
            var boardSize = new Vector2Int(10, 10);
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder {BoardSize = boardSize}.Build();
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            for (var i = 0; i < expectedArmyState.GetLength(0); i++)
            {
                for (var j = 0; j < expectedArmyState.GetLength(1); j++)
                {
                    expectedArmyState[i, j] = SimpleCellState.Neither;
                }
            }
            
            var result = simpleArmyStateCalculator.GetArmyState(player);
            
            Assert.AreEqual(expectedArmyState, result);
        }
        
        [Test]
        public void GetArmyStateAfterMove_ShouldSetUnitsMarks_OnPositionsFromUnitsSearcher()
        {
            var player = GetPlayer();
            var boardSize = new Vector2Int(10, 10);
            var unitsPositions = GetRandomVector2IntList(0, 9, 10);
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            foreach (var position in unitsPositions)
            {
                expectedArmyState[position.X, position.Y] = SimpleCellState.Unit;
            }
            var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
            unitsPositionsSearcherMock.Setup(u => u.GetUnitsPositions(player)).Returns(unitsPositions);
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder
            {
                BoardSize = boardSize,
                UnitsPositionsSearcher = unitsPositionsSearcherMock.Object
            }.Build();
            
            var result = simpleArmyStateCalculator.GetArmyStateAfterMove(player, GetMove(player));
            
            Assert.AreEqual(expectedArmyState, result);
        }
        
        [Test]
        public void GetArmyStateAfterMove_ShouldSetBaseMarks_OnAfterMovePositionsFromBasePositionsCalculator()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var boardSize = new Vector2Int(10, 10);
            var basePositions = GetRandomVector2IntList(0, 9, 10);
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            foreach (var position in basePositions)
            {
                expectedArmyState[position.X, position.Y] = SimpleCellState.Base;
            }
            var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
            basePositionsCalculatorMock.Setup(b => b.GetBasePositions(player)).Returns(new List<Vector2Int>());
            basePositionsCalculatorMock.Setup(b => b.GetBasePositionsAfterMove(player, move)).Returns(basePositions);
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder
            {
                BoardSize = boardSize,
                BasePositionsCalculator = basePositionsCalculatorMock.Object
            }.Build();
            
            var result = simpleArmyStateCalculator.GetArmyStateAfterMove(player, move);
            
            Assert.AreEqual(expectedArmyState, result);
        }

        [Test]
        public void GetArmyStateAfterMove_ShouldSetNeitherToUnitsPositions_ThatWereCutByValuesCutter_IfEnemyReplaceMoveGiven()
        {
            var player = GetPlayer();
            var move = new ReplaceMove(GetPlayer(), (2, 2), GetPlaceablePlayerOwned(GetPlayer()));
            //Setting up expected army state and army state before move
            var boardSize = new Vector2Int(10, 10);
            var unitsPositionsBeforeMove = new List<Vector2Int>{(1, 1), (2, 2), (3, 3), (4, 4) };
            var unitsPositionsAfterMove = new List<Vector2Int>{(1, 1),};
            var basePositions = new List<Vector2Int> { (0, 0) };
            var armyStateBeforeMove = new SimpleCellState[boardSize.X, boardSize.Y];
            var expectedArmyState = new SimpleCellState[boardSize.X, boardSize.Y];
            foreach (var position in unitsPositionsBeforeMove) armyStateBeforeMove[position.X, position.Y] = SimpleCellState.Unit;
            foreach (var position in unitsPositionsAfterMove) expectedArmyState[position.X, position.Y] = SimpleCellState.Unit;
            foreach (var position in basePositions)
            {
                armyStateBeforeMove[position.X, position.Y] = SimpleCellState.Base;
                expectedArmyState[position.X, position.Y] = SimpleCellState.Base;
            }
            //Setting up values cutter
            var positionsToCut = new List<Vector2Int>{(2, 2), (3, 3), (4, 4) };
            var unconnectedValuesCutterMock = new Mock<IUnconnectedValuesCutter>();
            //Making values cutter set Neither to positions from positionsToCut
            unconnectedValuesCutterMock
                .Setup(u => u.CutUnconnectedValues(It.IsAny<SimpleCellState[,]>(), SimpleCellState.Unit, SimpleCellState.Base, SimpleCellState.Neither))
                .Callback((SimpleCellState[,] armyState, SimpleCellState unitState, SimpleCellState baseState, SimpleCellState freeState) =>
                {
                    foreach (var position in positionsToCut)
                    {
                        armyState[position.X, position.Y] = SimpleCellState.Neither;
                    }
                });
            //Setting up other mocks
            var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
            unitsPositionsSearcherMock.Setup(u => u.GetUnitsPositions(player)).Returns(unitsPositionsBeforeMove);
            var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
            basePositionsCalculatorMock.Setup(b => b.GetBasePositionsAfterMove(player, move)).Returns(basePositions);
            //Building calculator
            var simpleArmyStateCalculator = new SimpleArmyStateCalculatorBuilder
            {
                BoardSize = boardSize,
                UnconnectedValuesCutter = unconnectedValuesCutterMock.Object,
                UnitsPositionsSearcher = unitsPositionsSearcherMock.Object,
                BasePositionsCalculator = basePositionsCalculatorMock.Object
            }.Build();
            
            var result = simpleArmyStateCalculator.GetArmyStateAfterMove(player, move);
            
            Assert.AreEqual(expectedArmyState, result);
        }

        private class SimpleArmyStateCalculatorBuilder
        {
            public Vector2Int BoardSize { get; set; }
            public IUnconnectedValuesCutter UnconnectedValuesCutter { get; set; }
            public IUnitsPositionsSearcher UnitsPositionsSearcher { get; set; }
            public IBasePositionsCalculator BasePositionsCalculator { get; set; }

            public SimpleArmyStateCalculatorBuilder()
            {
                var unconnectedValuesCutterMock = new Mock<IUnconnectedValuesCutter>();
                UnconnectedValuesCutter = unconnectedValuesCutterMock.Object;
                var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
                unitsPositionsSearcherMock.Setup(u => u.GetUnitsPositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
                UnitsPositionsSearcher = unitsPositionsSearcherMock.Object;
                var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
                basePositionsCalculatorMock.Setup(b => b.GetBasePositionsAfterMove(It.IsAny<Player>(), It.IsAny<AbstractMove>())).Returns(new List<Vector2Int>());
                basePositionsCalculatorMock.Setup(b => b.GetBasePositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
                BasePositionsCalculator = basePositionsCalculatorMock.Object;
            }
            
            public SimpleArmyStateCalculator Build()
            {
                return new SimpleArmyStateCalculator(UnconnectedValuesCutter, UnitsPositionsSearcher, BasePositionsCalculator, BoardSize);
            }
        }
    }
}