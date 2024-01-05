using castledice_game_logic.Time;
using Moq;
using NUnit.Framework;
using static Tests.ObjectCreationUtility;
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
            var playerHighlighterProviderMock = new Mock<IPlayerHighlighterProvider>();
            var player = GetPlayer();
            playerHighlighterProviderMock.Setup(provider => provider.GetHighlighter(player))
                .Returns(expectedHighlighter);
            var playerTimerViewCreator = new PlayerTimerViewCreatorBuilder
                { PlayerHighlighterProvider = playerHighlighterProviderMock.Object }.Build();
            
            var playerTimerView = playerTimerViewCreator.Create(player);
            var actualHighlighter = playerTimerView.GetPrivateField<Highlighter>(HighlighterFieldName);
            
            Assert.AreSame(expectedHighlighter, actualHighlighter);
        }
        
        [Test]
        public void Create_ShouldReturnPlayerTimerView_WithTimeViewFromProvider()
        {
            var expectedTimeView = new Mock<TimeView>().Object;
            var playerTimeViewProviderMock = new Mock<IPlayerTimeViewProvider>();
            var player = GetPlayer();
            playerTimeViewProviderMock.Setup(provider => provider.GetTimeView(player))
                .Returns(expectedTimeView);
            var playerTimerViewCreator = new PlayerTimerViewCreatorBuilder
                { PlayerTimeViewProvider = playerTimeViewProviderMock.Object }.Build();
            
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
            public IPlayerHighlighterProvider PlayerHighlighterProvider { get; set; } = new Mock<IPlayerHighlighterProvider>().Object;
            public IPlayerTimeViewProvider PlayerTimeViewProvider { get; set; } = new Mock<IPlayerTimeViewProvider>().Object;
            
            public PlayerTimerViewCreator Build()
            {
                return new PlayerTimerViewCreator(PlayerHighlighterProvider, PlayerTimeViewProvider);
            }
        }
    }
}