using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using Tests.EditMode.GameplayViewTests.TimersViewTests.PlayerTimerViewsTests;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerColorHighlighterProvider : IPlayerHighlighterProvider
    {
        private readonly Highlighter _redPlayerHighlighter;
        private readonly Highlighter _bluePlayerHighlighter;
        private readonly IPlayerColorProvider _playerColorProvider;
        
        public PlayerColorHighlighterProvider(Highlighter redPlayerHighlighter, Highlighter bluePlayerHighlighter, IPlayerColorProvider playerColorProvider)
        {
            _redPlayerHighlighter = redPlayerHighlighter;
            _bluePlayerHighlighter = bluePlayerHighlighter;
            _playerColorProvider = playerColorProvider;
        }

        public Highlighter GetHighlighter(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return playerColor switch
            {
                PlayerColor.Red => _redPlayerHighlighter,
                PlayerColor.Blue => _bluePlayerHighlighter,
                _ => throw new System.NotImplementedException()
            };
        }
    }
}