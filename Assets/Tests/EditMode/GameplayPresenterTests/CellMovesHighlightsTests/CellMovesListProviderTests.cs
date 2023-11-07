using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using NUnit.Framework;
using Src.GameplayPresenter.CellMovesHighlights;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellMovesHighlightsTests
{
    public class CellMovesListProviderTests
    {
        [Test]
        public void GetCellMovesList_ShouldReturnAppropriateListFromGivenGame([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var gameMock = GetGameMock();
            var expectedList = new List<CellMove>();
            gameMock.Setup(game => game.GetCellMoves(playerId)).Returns(expectedList);
            var provider = new CellMovesListProvider(gameMock.Object);
            
            var actualList = provider.GetCellMovesList(playerId);
            
            Assert.AreSame(expectedList, actualList);
        }
    }
}