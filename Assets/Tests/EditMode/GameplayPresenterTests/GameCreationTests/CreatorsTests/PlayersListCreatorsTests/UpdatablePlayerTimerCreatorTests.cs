using static Tests.Utils.ObjectCreationUtility;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayView.Updatables;
using Src.TimeManagement;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlayersListCreatorsTests
{
    public class UpdatablePlayerTimerCreatorTests
    {
        private const string TimeDeltaProviderFieldName = "_timeDeltaProvider";
        
        [Test]
        public void GetPlayerTimer_ShouldReturnUpdatablePlayerTimer()
        {
            var updatablePlayerTimerCreator = new UpdatablePlayerTimerCreatorBuilder().Build();
            
            var playerTimer = updatablePlayerTimerCreator.GetPlayerTimer(GetRandomTimeSpan());
            
            Assert.IsInstanceOf<UpdatablePlayerTimer>(playerTimer);
        }
        
        [Test]
        public void GetPlayerTimer_ShouldReturnUpdatablePlayerTimer_WithGivenTimeDeltaProvider()
        {
            var timeDeltaProvider = new Mock<ITimeDeltaProvider>().Object;
            var updatablePlayerTimerCreator = new UpdatablePlayerTimerCreatorBuilder
            {
                TimeDeltaProvider = timeDeltaProvider
            }.Build();
            
            var playerTimer = updatablePlayerTimerCreator.GetPlayerTimer(GetRandomTimeSpan());
            var actualTimeDeltaProvider = playerTimer.GetPrivateField<ITimeDeltaProvider>(TimeDeltaProviderFieldName);
            
            Assert.AreSame(timeDeltaProvider, actualTimeDeltaProvider);
        }
        
        [Test]
        public void GetPlayerTimer_ShouldReturnUpdatablePlayerTimer_WithGivenTimeSpan()
        {
            var timeSpan = GetRandomTimeSpan();
            var updatablePlayerTimerCreator = new UpdatablePlayerTimerCreatorBuilder().Build();
            
            var playerTimer = updatablePlayerTimerCreator.GetPlayerTimer(timeSpan);
            var actualTimeSpan = playerTimer.GetTimeLeft();
            
            Assert.AreEqual(timeSpan, actualTimeSpan);
        }
        
        [Test]
        public void GetPlayerTimer_ShouldPutCreatedTimer_ToUpdater()
        {
            var updaterMock = new Mock<IUpdater>();
            var updatablePlayerTimerCreator = new UpdatablePlayerTimerCreatorBuilder
            {
                Updater = updaterMock.Object
            }.Build();
            
            var playerTimer = updatablePlayerTimerCreator.GetPlayerTimer(GetRandomTimeSpan());
            
            updaterMock.Verify(updater => updater.AddUpdatable(playerTimer as IUpdatable));
        }
        
        private class UpdatablePlayerTimerCreatorBuilder
        {
            public ITimeDeltaProvider TimeDeltaProvider { get; set; } = new Mock<ITimeDeltaProvider>().Object;
            public IUpdater Updater { get; set; } = new Mock<IUpdater>().Object;
            
            public UpdatablePlayerTimerCreator Build()
            {
                return new UpdatablePlayerTimerCreator(TimeDeltaProvider, Updater);
            }
        }
    }
}