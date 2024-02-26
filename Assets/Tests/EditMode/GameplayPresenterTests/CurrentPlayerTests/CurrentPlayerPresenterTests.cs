using castledice_game_logic;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.CurrentPlayer;
using Src.GameplayView.CurrentPlayer;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CurrentPlayerTests
{
    public class CurrentPlayerPresenterTests
    {
        [Test]
        public void ShowCurrentPlayer_ShouldPassCurrentPlayerFromGivenGame_ToGivenView()
        {
            var currentPlayer = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(game => game.GetCurrentPlayer()).Returns(currentPlayer);
            var viewMock = new Mock<ICurrentPlayerView>();
            var presenter = new CurrentPlayerPresenter(gameMock.Object, viewMock.Object);
            
            presenter.ShowCurrentPlayer();
            
            viewMock.Verify(view => view.ShowCurrentPlayer(currentPlayer), Times.Once);
        }

        [Test]
        public void ShowCurrentPlayer_ShouldBeCalled_WhenTurnSwitched()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.SwitchTurn()).Raises(g => g.TurnSwitched += null, this, gameMock.Object);
            var testGame = gameMock.Object;
            var viewMock = new Mock<ICurrentPlayerView>();
            var presenter = new CurrentPlayerPresenter(testGame, viewMock.Object);
            
            testGame.SwitchTurn();
            
            viewMock.Verify(view => view.ShowCurrentPlayer(It.IsAny<Player>()), Times.Once);
        }
    }
}