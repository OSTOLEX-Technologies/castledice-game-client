using Moq;
using NUnit.Framework;
using Src.GameplayPresenter;
using Src.GameplayPresenter.ActionPointsCount;
using Src.GameplayView.ActionPointsCount;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.ActionPointsCountTests
{
    public class ActionPointsCountPresenterTests
    {
        [Test]
        public void UpdateActionPointsCount_ShouldHideActionPointsCount_IfPlayerIsNotCurrent([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(GetPlayer(playerId + 1));
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(p => p.GetId()).Returns(playerId);
            var viewMock = new Mock<IActionPointsCountView>();
            var presenter = new ActionPointsCountPresenter(playerDataProviderMock.Object, gameMock.Object, viewMock.Object);
            
            presenter.UpdateActionPointsCount();
            
            viewMock.Verify(v => v.HideActionPointsCount(), Times.Once);
        }

        [Test, Combinatorial]
        public void UpdateActionPointsCount_ShouldShowActionPointsCount_IfPlayerIsCurrent(
            [Values(1, 2, 3, 4, 5)] int playerId, [Values(1, 2, 3, 4, 5, 6)] int actionPointsCount)
        {
            var gameMock = GetGameMock();
            var player = GetPlayer(playerId);
            player.ActionPoints.Amount = actionPointsCount;
            gameMock.Setup(g => g.GetCurrentPlayer()).Returns(player);
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(p => p.GetId()).Returns(playerId);
            var viewMock = new Mock<IActionPointsCountView>();
            var presenter = new ActionPointsCountPresenter(playerDataProviderMock.Object, gameMock.Object, viewMock.Object);
            
            presenter.UpdateActionPointsCount();
            
            viewMock.Verify(v => v.ShowActionPointsCount(actionPointsCount), Times.Once);
        }

        [Test]
        public void UpdateActionPoints_ShouldBeCalled_IfPlayerGetsActionPoints([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var gameMock = GetGameMock();
            var player = GetPlayer();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(p => p.GetId()).Returns(playerId);
            var viewMock = new Mock<IActionPointsCountView>();
            var presenterMock = new Mock<ActionPointsCountPresenter>(playerDataProviderMock.Object, gameMock.Object, viewMock.Object);
            var presenter = presenterMock.Object;
            
            player.ActionPoints.IncreaseActionPoints(3);
            
            presenterMock.Verify(p => p.UpdateActionPointsCount(), Times.Once);
        }
        
        [Test]
        public void UpdateActionPoints_ShouldBeCalled_IfPlayerLosesActionPoints([Values(1, 2, 3, 4, 5)]int playerId)
        {
            var gameMock = GetGameMock();
            var player = GetPlayer();
            player.ActionPoints.Amount = 5;
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            playerDataProviderMock.Setup(p => p.GetId()).Returns(playerId);
            var viewMock = new Mock<IActionPointsCountView>();
            var presenterMock = new Mock<ActionPointsCountPresenter>(playerDataProviderMock.Object, gameMock.Object, viewMock.Object);
            var presenter = presenterMock.Object; //Forcing constructor call
            
            player.ActionPoints.DecreaseActionPoints(3);
            
            presenterMock.Verify(p => p.UpdateActionPointsCount(), Times.Once);
        }

        [Test]
        public void UpdateActionPoints_ShouldBeCalled_WhenTurnSwitchedInGame()
        {
            var game = GetTestGameMock().Object;
            var playerDataProviderMock = new Mock<IPlayerDataProvider>();
            var viewMock = new Mock<IActionPointsCountView>();
            var presenterMock = new Mock<ActionPointsCountPresenter>(playerDataProviderMock.Object, game, viewMock.Object);
            var presenter = presenterMock.Object; //Forcing constructor call
            
            game.SwitchTurn();
            
            presenterMock.Verify(p => p.UpdateActionPointsCount(), Times.Once);
        }
    }
}