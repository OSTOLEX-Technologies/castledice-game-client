using NUnit.Framework;
using Src.GameplayPresenter.CellMovesHighlights;
using static Tests.Utils.ObjectCreationUtility;

namespace Tests.EditMode.GameplayPresenterTests.CellMovesHighlightsTests
{
    public class CellMovesHighlightObserverTests
    {
        [Test]
        public void TimeToHighlight_ShouldBeInvokedOnce_WhenPlayerMoveAppliedAndPlayerHasActionPoints()
        {
            var player = GetPlayer(actionPointsCount: 6);
            var move = GetMove(player);
            var gameMock = GetGameMock();
            var observer = new CellMovesHighlightObserver(gameMock.Object, player);
            var timesInvoked = 0;
            observer.TimeToHighlight += () => timesInvoked++;
            
            gameMock.Raise(x => x.MoveApplied += null, gameMock.Object, move);
            
            Assert.AreEqual(1, timesInvoked);
        }
        
        [Test]
        public void TimeToHighlight_ShouldBeInvokedOnce_WhenActionPointsIncreased()
        {
            var player = GetPlayer(actionPointsCount: 6);
            var gameMock = GetGameMock();
            var observer = new CellMovesHighlightObserver(gameMock.Object, player);
            var timesInvoked = 0;
            observer.TimeToHighlight += () => timesInvoked++;
            
            player.ActionPoints.IncreaseActionPoints(1);
            
            Assert.AreEqual(1, timesInvoked);
        }
        
        [Test]
        public void TimeToHighlight_ShouldNotBeInvoked_WhenPlayerMoveAppliedAndPlayerHasNoActionPoints()
        {
            var player = GetPlayer(actionPointsCount: 0);
            var move = GetMove(player);
            var gameMock = GetGameMock();
            var observer = new CellMovesHighlightObserver(gameMock.Object, player);
            var timesInvoked = 0;
            observer.TimeToHighlight += () => timesInvoked++;
            
            gameMock.Raise(x => x.MoveApplied += null, gameMock.Object, move);
            
            Assert.AreEqual(0, timesInvoked);
        }
    }
}