using castledice_game_logic;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerColorTimeViewProvider : ITimeViewForPlayerProvider
    {
        private readonly TimeView _redPlayerTimeView;
        private readonly TimeView _bluePlayerTimeView;
        private readonly IPlayerColorProvider _playerColorProvider;
        
        public PlayerColorTimeViewProvider(TimeView redPlayerTimeView, TimeView bluePlayerTimeView, IPlayerColorProvider playerColorProvider)
        {
            _redPlayerTimeView = redPlayerTimeView;
            _bluePlayerTimeView = bluePlayerTimeView;
            _playerColorProvider = playerColorProvider;
        }
        
        public TimeView GetTimeView(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return playerColor switch
            {
                PlayerColor.Red => _redPlayerTimeView,
                PlayerColor.Blue => _bluePlayerTimeView,
                _ => throw new System.NotImplementedException()
            };
        }
    }
}