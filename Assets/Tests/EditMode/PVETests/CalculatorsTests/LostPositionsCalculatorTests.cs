using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using Moq;
using NUnit.Framework;
using Src.PVE;
using Src.PVE.Calculators;
using static Tests.Utils.ContentMocksCreationUtility;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class LostPositionsCalculatorTests
    {
        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfMoveIsDoneByPlayer()
        {
            var player = GetPlayer();
            var move = GetMove(player);
            var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
            unitsPositionsSearcherMock.Setup(u => u.GetUnitsPositions(player)).Returns(new List<Vector2Int>
            {
                (0, 1), (1, 0), (1,1)
            }); //Making some units positions to make sure they won't be present in the returned list
            var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
            basePositionsCalculatorMock.Setup(b => b.GetBasePositions(player)).Returns(new List<Vector2Int>
            {
                
            });
            var lostPositionsCalculator = new LostPositionsCalculatorBuilder().Build();
            
            var result = lostPositionsCalculator.GetLostPositions(player, move);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfEnemyPlaceMoveApplied()
        {
            var player = GetPlayer();
            var enemy = GetPlayer();

        }
        
        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfUpgradeMoveApplied()
        {

        }

        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfRemoveMoveApplied()
        {

        }

        [Test]
        public void GetLostPositions_ShouldReturnLostPosition_WhereReplaceMoveWasApplied()
        {

        }

        [Test]
        public void GetLostPositions_ShouldReturnPositionsOfUnits_DestroyedWithReplaceMove()
        {
        
        }

        private class LostPositionsCalculatorBuilder
        {
            public IUnitsPositionsSearcher UnitsPositionsSearcher { get; set; }
            public IUnconnectedValuesCutter<SimpleCellState> UnconnectedValuesCutter { get; set; }
            public IBasePositionsCalculator BasePositionsCalculator { get; set; }

            public LostPositionsCalculatorBuilder()
            {
                var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
                unitsPositionsSearcherMock.Setup(x => x.GetUnitsPositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
                UnitsPositionsSearcher = unitsPositionsSearcherMock.Object;
                var unconnectedValuesCutterMock = new Mock<IUnconnectedValuesCutter<SimpleCellState>>();
                UnconnectedValuesCutter = unconnectedValuesCutterMock.Object;
                var basePositionsCalculatorMock = new Mock<IBasePositionsCalculator>();
                basePositionsCalculatorMock.Setup(x => x.GetBasePositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
                BasePositionsCalculator = basePositionsCalculatorMock.Object;
            }  
            
            public LostPositionsCalculator Build()
            {
                return new LostPositionsCalculator(UnitsPositionsSearcher, UnconnectedValuesCutter, BasePositionsCalculator);
            }
        }
    }
}