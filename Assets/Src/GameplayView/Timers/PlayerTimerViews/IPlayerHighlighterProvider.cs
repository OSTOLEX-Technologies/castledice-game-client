using castledice_game_logic;
using Src.GameplayView.Timers.PlayerTimerViews;

namespace Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests
{
    public interface IPlayerHighlighterProvider
    {
        Highlighter GetHighlighter(Player player);
    }
}