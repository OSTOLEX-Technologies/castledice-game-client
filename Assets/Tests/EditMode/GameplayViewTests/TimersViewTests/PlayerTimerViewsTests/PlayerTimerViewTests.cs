using castledice_game_logic.Time;
using Moq;
using NUnit.Framework;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class PlayerTimerViewTests
    {
        [Test]
        public void Highlight_ShouldCallHighlight_OnHighlighter()
        {
            var highlighterMock = new Mock<Highlighter>();
            var playerTimerView = new PlayerTimerViewBuilder
            {
                Highlighter = highlighterMock.Object
            }.Build();
            
            playerTimerView.Highlight();
            
            highlighterMock.Verify(highlighter => highlighter.Highlight(), Times.Once);
        }
        
        [Test]
        public void Obscure_ShouldCallObscure_OnHighlighter()
        {
            var highlighterMock = new Mock<Highlighter>();
            var playerTimerView = new PlayerTimerViewBuilder
            {
                Highlighter = highlighterMock.Object
            }.Build();
            
            playerTimerView.Obscure();
            
            highlighterMock.Verify(highlighter => highlighter.Obscure(), Times.Once);
        }
        
        [Test]
        public void Update_ShouldSetTimeLeftFromTimer_ToTimeView()
        {
            var timeViewMock = new Mock<TimeView>();
            var playerTimerMock = new Mock<IPlayerTimer>();
            var playerTimer = playerTimerMock.Object;
            var playerTimerView = new PlayerTimerViewBuilder
            {
                TimeView = timeViewMock.Object,
                PlayerTimer = playerTimer
            }.Build();
            
            playerTimerView.Update();
            
            timeViewMock.Verify(timeView => timeView.SetTime(playerTimer.GetTimeLeft()), Times.Once);
        }
        
        private class PlayerTimerViewBuilder
        {
            public TimeView TimeView { get; set; } = new Mock<TimeView>().Object;
            public Highlighter Highlighter { get; set; } = new Mock<Highlighter>().Object;
            public IPlayerTimer PlayerTimer { get; set; } = new Mock<IPlayerTimer>().Object;
            
            public PlayerTimerView Build()
            {
                return new PlayerTimerView(TimeView, Highlighter, PlayerTimer);
            }
        }
    }
}