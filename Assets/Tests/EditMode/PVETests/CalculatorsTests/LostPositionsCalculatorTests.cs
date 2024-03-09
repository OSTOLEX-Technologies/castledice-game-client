using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using castledice_game_logic.MovesLogic.Rules;
using Moq;
using NUnit.Framework;
using Src.PVE;
using Src.PVE.Calculators;
using static Tests.Utils.ObjectCreationUtility;
using static Tests.Utils.ContentMocksCreationUtility;

namespace Tests.EditMode.PVETests.CalculatorsTests
{
    public class LostPositionsCalculatorTests
    {
        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfMoveIsDoneByPlayer()
        {

        }

        [Test]
        public void GetLostPositions_ShouldReturnEmptyList_IfPlaceMoveApplied()
        {

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
            public Board Board { get; set; }
            public IUnitsPositionsSearcher UnitsPositionsSearcher { get; set; }
            public IUnconnectedValuesCutter<SimpleCellState> UnconnectedValuesCutter { get; set; }
            public IBasePositionsCalculator BasePositionsCalculator { get; set; }

            public LostPositionsCalculatorBuilder()
            {
                Board = GetFullNByNBoard(1);
                var unitsPositionsSearcherMock = new Mock<IUnitsPositionsSearcher>();
                unitsPositionsSearcherMock.Setup(x => x.GetUnitsPositions(It.IsAny<Player>())).Returns(new List<Vector2Int>());
            }
        }
    }
}