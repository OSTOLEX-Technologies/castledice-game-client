using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameOver;
using Src.GameplayView.GameOver;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameOverTests
{
    public class GameOverPresenterTests
    {
        [Test]
        public void Presenter_ShouldInvokeShowWinOnViewWithWinner_IfWinEventIsInvokedOnGame()
        {
            var winner = GetPlayer();
            var gameMock = GetGameMock();
            gameMock.Setup(game => game.CheckTurns()).Raises(g => g.Win += null, this, (gameMock.Object, winner));
            var testGame = gameMock.Object;
            var viewMock = new Mock<IGameOverView>();
            var presenter = new GameOverPresenter(testGame, viewMock.Object);
            
            testGame.CheckTurns();
            
            viewMock.Verify(view => view.ShowWin(winner), Times.Once);
        }

        [Test]
        public void Presenter_ShouldInvokeShowDrawOnView_IfDrawEventIsInvokedOnGame()
        {
            var gameMock = GetGameMock();
            gameMock.Setup(game => game.CheckTurns()).Raises(g => g.Draw += null, this, gameMock.Object);
            var testGame = gameMock.Object;
            var viewMock = new Mock<IGameOverView>();
            var presenter = new GameOverPresenter(testGame, viewMock.Object);
            
            testGame.CheckTurns();
            
            viewMock.Verify(view => view.ShowDraw(), Times.Once);
        }
    }
}