using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.ActionPointsGiving;
using Src.GameplayPresenter.GameWrappers;
using Src.GameplayView.ActionPointsGiving;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.ActionPointsGivingTests
{
    public class ActionPointsGivingPresenterTests
    {
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(157, 3)]
        [TestCase(3812, 6)]
        public void GiveActionPoints_ShouldCallGiveActionPoints_OnGivenGiverWithAppropriateArguments(int playerId,
            int amount)
        {
            var playerProviderMock = new Mock<IPlayerProvider>();
            var actionPointsGiverMock = new Mock<IActionPointsGiver>();
            var viewMock = new Mock<IActionPointsGivingView>();
            var presenter = new ActionPointsGivingPresenter(playerProviderMock.Object, actionPointsGiverMock.Object,
                viewMock.Object);
            
            presenter.GiveActionPoints(playerId, amount);
            
            actionPointsGiverMock.Verify(giver => giver.GiveActionPoints(playerId, amount), Times.Once);
        }

        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(157, 3)]
        [TestCase(3812, 6)]
        public void GiveActionPoints_ShouldPassPlayerFromProviderAndGivenAmount_ToView(int playerId, int amount)
        {
            var player = GetPlayer();
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(provider => provider.GetPlayer(playerId)).Returns(player);
            var actionPointsGiverMock = new Mock<IActionPointsGiver>();
            var viewMock = new Mock<IActionPointsGivingView>();
            var presenter = new ActionPointsGivingPresenter(playerProviderMock.Object, actionPointsGiverMock.Object,
                viewMock.Object);
            
            presenter.GiveActionPoints(playerId, amount);
            
            viewMock.Verify(view => view.ShowActionPointsForPlayer(player, amount), Times.Once);
        }
    }
}