using castledice_game_logic;
using Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerTimerViewCreator : IPlayerTimerViewCreator
    {
        private readonly IHighlighterForPlayerProvider _highlighterForPlayerProvider;
        private readonly ITimeViewForPlayerProvider _timeViewForPlayerProvider;
        
        public PlayerTimerViewCreator(IHighlighterForPlayerProvider highlighterForPlayerProvider, ITimeViewForPlayerProvider timeViewForPlayerProvider)
        {
            _highlighterForPlayerProvider = highlighterForPlayerProvider;
            _timeViewForPlayerProvider = timeViewForPlayerProvider;
        }
        
        public IPlayerTimerView Create(Player player)
        {
            var highlighter = _highlighterForPlayerProvider.GetHighlighter(player);
            var timeView = _timeViewForPlayerProvider.GetTimeView(player);
            var timer = player.Timer;
            return new PlayerTimerView(timeView, highlighter, timer);
        }
    }
}