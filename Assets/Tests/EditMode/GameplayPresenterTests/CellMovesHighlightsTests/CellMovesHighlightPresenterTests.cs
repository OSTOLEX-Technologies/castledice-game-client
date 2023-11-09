using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayPresenter.CellMovesHighlights;
using Src.GameplayView.CellMovesHighlights;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightPresenterTests
    {
        [Test]
        public void HighlightCellMoves_ShouldSendCurrentPlayerCellMovesList_ToGivenView([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var viewMock = new Mock<ICellMovesHighlightView>();
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var expectedList = new List<CellMove>();
            playerDataProviderMock.Setup(provider => provider.GetId()).Returns(playerId);
            cellMovesListProviderMock.Setup(provider => provider.GetCellMovesList(playerId)).Returns(expectedList);
            var gameMock = GetGameMock();
            var presenter = new CellMovesHighlightPresenter(playerDataProviderMock.Object, cellMovesListProviderMock.Object, gameMock.Object, viewMock.Object);
            
            presenter.HighlightCellMoves();
            
            viewMock.Verify(view => view.HighlightCellMoves(expectedList), Times.Once);
        }

        [Test]
        public void HighlightCellMoves_ShouldBeCalled_IfMoveAppliedEventInvokedInGame()
        {
            var game = GetTestGameMock().Object;
            var viewMock = new Mock<ICellMovesHighlightView>();
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var presenterMock = new Mock<CellMovesHighlightPresenter>(playerDataProviderMock.Object, cellMovesListProviderMock.Object, game, viewMock.Object);
            var testObject = presenterMock.Object;
            
            game.InvokeMoveApplied(GetMove());
            
            presenterMock.Verify(presenter => presenter.HighlightCellMoves(), Times.Once);
        }

        [Test]
        public void HighlightCellMoves_ShouldBeCalled_IfTurnSwitchedInGame()
        {
            var game = GetTestGameMock().Object;
            var viewMock = new Mock<ICellMovesHighlightView>();
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var presenterMock = new Mock<CellMovesHighlightPresenter>(playerDataProviderMock.Object, cellMovesListProviderMock.Object, game, viewMock.Object);
            var testObject = presenterMock.Object;
            
            game.SwitchTurn();
            
            presenterMock.Verify(presenter => presenter.HighlightCellMoves(), Times.Once);
        }

        [Test]
        public void HighlightCellMoves_ShouldBeCalled_IfPlayerGotActionPoints()
        {
            var game = GetTestGameMock().Object;
            var player = game.GetAllPlayers()[0];
            var playerId = player.Id;
            var viewMock = new Mock<ICellMovesHighlightView>();
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(provider => provider.GetId()).Returns(playerId);
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var cellMovesList = new List<CellMove>();
            cellMovesListProviderMock.Setup(provider => provider.GetCellMovesList(playerId)).Returns(cellMovesList);
            var presenter = new CellMovesHighlightPresenter(playerDataProviderMock.Object, cellMovesListProviderMock.Object, game, viewMock.Object);
            
            player.ActionPoints.IncreaseActionPoints(3);
            
            viewMock.Verify(view => view.HighlightCellMoves(cellMovesList), Times.Once);
        }
        
        [Test]
        public void HighlightCellMoves_ShouldCallHideHighlights_BeforeCallingHighlightCellMoves()
        {
            var game = GetTestGameMock().Object;
            var viewMock = new Mock<ICellMovesHighlightView>(MockBehavior.Strict);
            var mockSequence = new MockSequence();
            viewMock.InSequence(mockSequence).Setup(view => view.HideHighlights());
            viewMock.InSequence(mockSequence).Setup(view => view.HighlightCellMoves(It.IsAny<List<CellMove>>()));
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var cellMovesListProviderMock = new Mock<ICellMovesListProvider>();
            var presenter = new CellMovesHighlightPresenter(playerDataProviderMock.Object, cellMovesListProviderMock.Object, game, viewMock.Object);
            
            presenter.HighlightCellMoves();
        }
    }
}