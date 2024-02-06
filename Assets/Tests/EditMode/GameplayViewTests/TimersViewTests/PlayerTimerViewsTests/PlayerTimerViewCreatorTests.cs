using castledice_game_logic;
using castledice_game_logic.Time;
using Moq;
using NUnit.Framework;
using static Tests.Utils.ObjectCreationUtility;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public class PlayerTimerViewCreatorTests
    {
        private const string HighlighterFieldName = "_highlighter";
        private const string TimeViewFieldName = "_timeView";
        private const string TimerFieldName = "_playerTimer";
        
        [Test]
        public void Create_ShouldReturnPlayerTimerView_WithHighlighterFromProvider()
        {
            var expectedHighlighter = new Mock<Highlighter>().Object;
            var playerHighlighterProviderMock = new Mock<IHighlighterForPlayerProvider>();
            var player = GetPlayer();
            playerHighlighterProviderMock.Setup(provider => provider.GetHighlighter(player))
                .Returns(expectedHighlighter);
            var playerTimerViewCreator = new PlayerTimerViewCreatorBuilder
                { HighlighterForPlayerProvider = playerHighlighterProviderMock.Object }.Build();
            
            var playerTimerView = playerTimerViewCreator.Create(player);
            var actualHighlighter = playerTimerView.GetPrivateField<Highlighter>(HighlighterFieldName);
            
            Assert.AreSame(expectedHighlighter, actualHighlighter);
        }
        
        [Test]
        public void Create_ShouldReturnPlayerTimerView_WithTimeViewFromProvider()
        {
            var expectedTimeView = new Mock<TimeView>().Object;
            var playerTimeViewProviderMock = new Mock<ITimeViewForPlayerProvider>();
            var player = GetPlayer();
            playerTimeViewProviderMock.Setup(provider => provider.GetTimeView(player))
                .Returns(expectedTimeView);
            var playerTimerViewCreator = new PlayerTimerViewCreatorBuilder
                { TimeViewForPlayerProvider = playerTimeViewProviderMock.Object }.Build();
            
            var playerTimerView = playerTimerViewCreator.Create(player);
            var actualTimeView = playerTimerView.GetPrivateField<TimeView>(TimeViewFieldName);
            
            Assert.AreSame(expectedTimeView, actualTimeView);
        }

        [Test]
        public void Create_ShouldReturnPlayerTimerView_WithTimerFromPlayer()
        {
            var player = GetPlayer();
            var expectedTimer = player.Timer;
            var playerTimerViewCreator = new PlayerTimerViewCreatorBuilder().Build();
            
            var playerTimerView = playerTimerViewCreator.Create(player);
            var actualTimer = playerTimerView.GetPrivateField<IPlayerTimer>(TimerFieldName);
            
            Assert.AreSame(expectedTimer, actualTimer);
        }
        
        private class PlayerTimerViewCreatorBuilder
        {
            public IHighlighterForPlayerProvider HighlighterForPlayerProvider { get; set; }
            public ITimeViewForPlayerProvider TimeViewForPlayerProvider { get; set; }
            
            public PlayerTimerViewCreatorBuilder()
            {
                var highlighterForPlayerProviderMock = new Mock<IHighlighterForPlayerProvider>();
                var highlightMock = new Mock<Highlighter>();
                highlighterForPlayerProviderMock.Setup(provider => provider.GetHighlighter(It.IsAny<Player>()))
                    .Returns(highlightMock.Object);
                HighlighterForPlayerProvider = highlighterForPlayerProviderMock.Object;
                
                var timeViewForPlayerProviderMock = new Mock<ITimeViewForPlayerProvider>();
                var timeViewMock = new Mock<TimeView>();
                timeViewForPlayerProviderMock.Setup(provider => provider.GetTimeView(It.IsAny<Player>()))
                    .Returns(timeViewMock.Object);
                TimeViewForPlayerProvider = timeViewForPlayerProviderMock.Object;
            }
            
            public PlayerTimerViewCreator Build()
            {
                return new PlayerTimerViewCreator(HighlighterForPlayerProvider, TimeViewForPlayerProvider);
            }
        }
    }
}