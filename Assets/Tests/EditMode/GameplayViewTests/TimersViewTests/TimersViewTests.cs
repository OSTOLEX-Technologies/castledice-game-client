using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.Timers;
using Src.GameplayView.Timers.PlayerTimerViews;
using Src.GameplayView.Updatables;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests
{
    public class TimersViewTests
    {
        [Test]
        //TimersView obtains IPlayerTimerView from IPlayerTimerViewProvider
        public void StartTimer_ShouldPutPlayerTimerView_ToUpdater()
        {
            var playerTimerView = new Mock<IPlayerTimerView>().Object;
            var updaterMock = new Mock<IUpdater>();
            var playerTimerViewProviderMock = new Mock<IPlayerTimerViewProvider>();
            var player = GetPlayer();
            playerTimerViewProviderMock.Setup(provider => provider.GetTimerViewForPlayer(player))
                .Returns(playerTimerView);
            var timersView = new TimersView(playerTimerViewProviderMock.Object, updaterMock.Object);
            
            timersView.StartTimer(player);
            
            updaterMock.Verify(updater => updater.AddUpdatable(playerTimerView), Times.Once);
        }
        
        [Test]
        //TimersView obtains IPlayerTimerView from IPlayerTimerViewProvider
        public void StopTimer_ShouldRemovePlayerTimerView_FromUpdater()
        {
            var playerTimerView = new Mock<IPlayerTimerView>().Object;
            var updaterMock = new Mock<IUpdater>();
            var playerTimerViewProviderMock = new Mock<IPlayerTimerViewProvider>();
            var player = GetPlayer();
            playerTimerViewProviderMock.Setup(provider => provider.GetTimerViewForPlayer(player))
                .Returns(playerTimerView);
            var timersView = new TimersView(playerTimerViewProviderMock.Object, updaterMock.Object);
            
            timersView.StopTimer(player);
            
            updaterMock.Verify(updater => updater.RemoveUpdatable(playerTimerView), Times.Once);
        }
    }
}