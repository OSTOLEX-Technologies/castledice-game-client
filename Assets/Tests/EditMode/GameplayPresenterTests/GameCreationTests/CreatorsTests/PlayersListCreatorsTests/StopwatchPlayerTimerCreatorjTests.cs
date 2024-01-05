using static Tests.ObjectCreationUtility;
using System;
using castledice_game_logic.Time;
using NUnit.Framework;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.CreatorsTests.PlayersListCreatorsTests
{
    public class StopwatchPlayerTimerCreatorTests
    {
        [Test]
        public void GetPlayerTimer_ShouldReturnStopwatchPlayerTimer()
        {
            var creator = new StopwatchPlayerTimerCreator();
            
            var playerTimer = creator.GetPlayerTimer(GetRandomTimeSpan());
            
            Assert.IsInstanceOf<StopwatchPlayerTimer>(playerTimer);
        }
        
        [Test]
        public void GetPlayerTimer_ShouldReturnStopwatchPlayerTimer_WithPassedTimeSpan()
        {
            var timeSpan = GetRandomTimeSpan();
            var creator = new StopwatchPlayerTimerCreator();
            
            var playerTimer = creator.GetPlayerTimer(timeSpan);
            
            Assert.AreEqual(timeSpan, playerTimer.GetTimeLeft());
        }
    }
}