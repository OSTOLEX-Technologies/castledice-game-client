using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameOver;
using Src.GameplayView.GameOver;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.GameOverTests
{
    public class GameOverPresenterTests
    {
        [Test]
        public void Presenter_ShouldInvokeShowWinOnViewWithWinner_IfWinEventIsInvokedOnGame()
        {
            var winner = GetPlayer();
            var testGame = GetTestGameMock().Object;
            var viewMock = new Mock<IGameOverView>();
            var presenter = new GameOverPresenter(testGame, viewMock.Object);
            
            testGame.ForceWin(winner);
            
            viewMock.Verify(view => view.ShowWin(winner), Times.Once);
        }

        [Test]
        public void Presenter_ShouldInvokeShowDrawOnView_IfDrawEventIsInvokedOnGame()
        {
            var testGame = GetTestGameMock().Object;
            var viewMock = new Mock<IGameOverView>();
            var presenter = new GameOverPresenter(testGame, viewMock.Object);
            
            testGame.ForceDraw();
            
            viewMock.Verify(view => view.ShowDraw(), Times.Once);
        }
    }
}