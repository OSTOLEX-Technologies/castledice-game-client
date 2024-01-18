using System;
using Moq;
using NUnit.Framework;
using Src.TimeManagement;
using static Tests.ObjectCreationUtility;

namespace Tests.EditMode.TimeManagementTests
{
    public class UpdatablePlayerTimerTests
    {
        [Test]
        public void GetTimeLeft_ShouldReturnTimeLeftFromConstructor_IfNoTimePassed()
        {
            var timeLeft = GetRandomTimeSpan();
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeft
            }.Build();
            
            var actualTimeLeft = updatablePlayerTimer.GetTimeLeft();
            
            Assert.AreEqual(timeLeft, actualTimeLeft);
        }

        [Test]
        public void Update_ShouldReduceTimeLeftByDeltaFromProvider_IfTimerIsStarted()
        {
            var timeLeftBeforeUpdate = GetRandomTimeSpan(1000, 1000000);
            var timeDelta = timeLeftBeforeUpdate.Seconds / 3;
            var timeDeltaProviderMock = new Mock<ITimeDeltaProvider>();
            timeDeltaProviderMock.Setup(provider => provider.GetDeltaTime())
                .Returns(timeDelta);
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeftBeforeUpdate,
                TimeDeltaProvider = timeDeltaProviderMock.Object
            }.Build();
            
            updatablePlayerTimer.Start();
            updatablePlayerTimer.Update();
            var timeLeftAfterUpdate = updatablePlayerTimer.GetTimeLeft();
            
            Assert.AreEqual(timeLeftBeforeUpdate - TimeSpan.FromSeconds(timeDelta), timeLeftAfterUpdate);
        }
        
        [Test]
        public void Update_ShouldNotReduceTimeLeft_IfTimerIsNotStarted()
        {
            var timeLeftBeforeUpdate = GetRandomTimeSpan(1000, 1000000);
            var timeDelta = timeLeftBeforeUpdate.Seconds / 3;
            var timeDeltaProviderMock = new Mock<ITimeDeltaProvider>();
            timeDeltaProviderMock.Setup(provider => provider.GetDeltaTime())
                .Returns(timeDelta);
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeftBeforeUpdate,
                TimeDeltaProvider = timeDeltaProviderMock.Object
            }.Build();
            
            updatablePlayerTimer.Update();
            var timeLeftAfterUpdate = updatablePlayerTimer.GetTimeLeft();
            
            Assert.AreEqual(timeLeftBeforeUpdate, timeLeftAfterUpdate);
        }
        
        [Test]
        public void Update_ShouldNotReduceTimeLeft_IfTimerIsStopped()
        {
            var timeLeftBeforeUpdate = GetRandomTimeSpan(1000, 1000000);
            var timeDelta = timeLeftBeforeUpdate.Seconds / 3;
            var timeDeltaProviderMock = new Mock<ITimeDeltaProvider>();
            timeDeltaProviderMock.Setup(provider => provider.GetDeltaTime())
                .Returns(timeDelta);
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeftBeforeUpdate,
                TimeDeltaProvider = timeDeltaProviderMock.Object
            }.Build();
            
            updatablePlayerTimer.Start();
            updatablePlayerTimer.Stop();
            updatablePlayerTimer.Update();
            var timeLeftAfterUpdate = updatablePlayerTimer.GetTimeLeft();
            
            Assert.AreEqual(timeLeftBeforeUpdate, timeLeftAfterUpdate);
        }

        [Test]
        public void Update_ShouldStopTimer_IfTimeLeftIsZero()
        {
            var timeLeft = TimeSpan.Zero;
            var timeDelta = GetRandomTimeSpan();
            var newTimeLeft = GetRandomTimeSpan();
            var timeDeltaProviderMock = new Mock<ITimeDeltaProvider>();
            timeDeltaProviderMock.Setup(provider => provider.GetDeltaTime())
                .Returns(timeDelta.Seconds);
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeft,
                TimeDeltaProvider = timeDeltaProviderMock.Object
            }.Build();
            
            //In this test we check whether or not timer was stopped by setting new time left and checking if it will be reduced after Update call.
            updatablePlayerTimer.Start();
            updatablePlayerTimer.Update();
            updatablePlayerTimer.SetTimeLeft(newTimeLeft);
            updatablePlayerTimer.Update();
            var timeLeftAfterUpdate = updatablePlayerTimer.GetTimeLeft();
            
            Assert.AreEqual(newTimeLeft, timeLeftAfterUpdate);
        }
        
        [Test]
        public void Update_ShouldFireTimeIsUpEvent_IfTimeLeftIsZeroAndTimerIsStarted()
        {
            var timeLeft = TimeSpan.Zero;
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeft,
            }.Build();
            var timeIsUpEventFired = false;
            updatablePlayerTimer.TimeIsUp += () => timeIsUpEventFired = true;
            
            updatablePlayerTimer.Start();
            updatablePlayerTimer.Update();
            
            Assert.True(timeIsUpEventFired);
        }

        [Test]
        public void SetTimeLeft_ShouldSetNewTimeLeft()
        {
            var timeLeft = GetRandomTimeSpan();
            var newTimeLeft = timeLeft * 2;
            var updatablePlayerTimer = new UpdatablePlayerTimerBuilder
            {
                TimeLeft = timeLeft,
            }.Build();
            
            updatablePlayerTimer.SetTimeLeft(newTimeLeft);
            
            Assert.AreEqual(newTimeLeft, updatablePlayerTimer.GetTimeLeft());
        }

        private class UpdatablePlayerTimerBuilder
        {
            public TimeSpan TimeLeft { get; set; } = TimeSpan.Zero;
            public ITimeDeltaProvider TimeDeltaProvider { get; set; } = new Mock<ITimeDeltaProvider>().Object;
            
            public UpdatablePlayerTimer Build()
            {
                return new UpdatablePlayerTimer(TimeLeft, TimeDeltaProvider);
            }
        }
    }
}