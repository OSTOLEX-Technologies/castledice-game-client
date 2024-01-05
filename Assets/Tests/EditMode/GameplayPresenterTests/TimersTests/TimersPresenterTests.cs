using System;
using castledice_game_logic;
using castledice_game_logic.Time;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.Timers;
using static Tests.ObjectCreationUtility;
using Src.GameplayView.Timers;

namespace Tests.EditMode.GameplayPresenterTests.TimersTests
{
    public class TimersPresenterTests
    {
        [Test]
        public void SwitchTimerForPlayer_ShouldStartTimerForPlayerWithId_IfSwitchToIsTrue()
        {
            var playerId = new Random().Next();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var player = GetPlayer(timer: playerTimerMock.Object);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var presenter = new TimersPresenterBuilder
            {
                Game = gameMock.Object
            }.Build();
            
            presenter.SwitchTimerForPlayer(playerId, TimeSpan.Zero, true);
            
            playerTimerMock.Verify(t => t.Start(), Times.Once);
        }
        
        [Test]
        public void SwitchTimerForPlayer_ShouldStopTimerForPlayerWithId_IfSwitchToIsFalse()
        {
            var playerId = new Random().Next();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var player = GetPlayer(timer: playerTimerMock.Object);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var presenter = new TimersPresenterBuilder
            {
                Game = gameMock.Object
            }.Build();
            
            presenter.SwitchTimerForPlayer(playerId, TimeSpan.Zero, false);
            
            playerTimerMock.Verify(t => t.Stop(), Times.Once);
        }

        [Test]
        public void SwitchTimerForPlayer_ShouldSetGivenTimeSpan_ToPlayerTimer()
        {
            var playerId = new Random().Next();
            var timeSpan = GetRandomTimeSpan();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var player = GetPlayer(timer: playerTimerMock.Object);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var presenter = new TimersPresenterBuilder
            {
                Game = gameMock.Object
            }.Build();
            
            presenter.SwitchTimerForPlayer(playerId, timeSpan, true);
            
            playerTimerMock.Verify(t => t.SetTimeLeft(timeSpan), Times.Once);
        }

        [Test]
        public void SwitchTimerForPlayer_ShouldPassPlayerToStopTimerOnView_IfSwitchToIsFalse()
        {
            var playerId = new Random().Next();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var player = GetPlayer(timer: playerTimerMock.Object);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var timersViewMock = new Mock<ITimersView>();
            var presenter = new TimersPresenterBuilder
            {
                Game = gameMock.Object,
                TimersView = timersViewMock.Object
            }.Build();
            
            presenter.SwitchTimerForPlayer(playerId, TimeSpan.Zero, false);
            
            timersViewMock.Verify(v => v.StopTimerForPlayer(player), Times.Once);
        }
        
        [Test]
        public void SwitchTimerForPlayer_ShouldPassPlayerToStartTimerOnView_IfSwitchToIsTrue()
        {
            var playerId = new Random().Next();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var player = GetPlayer(timer: playerTimerMock.Object);
            var gameMock = GetGameMock();
            gameMock.Setup(g => g.GetPlayer(playerId)).Returns(player);
            var timersViewMock = new Mock<ITimersView>();
            var presenter = new TimersPresenterBuilder
            {
                Game = gameMock.Object,
                TimersView = timersViewMock.Object
            }.Build();
            
            presenter.SwitchTimerForPlayer(playerId, TimeSpan.Zero, true);
            
            timersViewMock.Verify(v => v.StartTimerForPlayer(player), Times.Once);
        }

        private class TimersPresenterBuilder
        {
            public ITimersView TimersView { get; set; } = new Mock<ITimersView>().Object;
            public Game Game { get; set; } = GetGame();
            
            public TimersPresenter Build()
            {
                return new TimersPresenter(TimersView, Game);
            }
        }
    }
}