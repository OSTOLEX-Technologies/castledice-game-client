using System;
using System.Collections.Generic;
using castledice_game_logic;
using Src.GameplayView.PlayersColors;

namespace Src.GameplayView.Timers.PlayerTimerViews
{
    public class PlayerTimerViewProvider : IPlayerTimerViewProvider
    {
        private readonly Dictionary<PlayerColor, IPlayerTimerView> _playerTimerViews;
        private readonly IPlayerColorProvider _playerColorProvider;
        
        public PlayerTimerViewProvider(Dictionary<PlayerColor, IPlayerTimerView> playerTimerViews, IPlayerColorProvider playerColorProvider)
        {
            _playerTimerViews = playerTimerViews;
            _playerColorProvider = playerColorProvider;
        }

        public IPlayerTimerView GetTimerViewForPlayer(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            if (!_playerTimerViews.ContainsKey(playerColor))
            {
                throw new InvalidOperationException("Timer view for player color " + playerColor + " not found.");
            }
            return _playerTimerViews[playerColor];
        }
    }
}