using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.ContentColor
{
    public class PlayerContentColorProvider : IPlayerContentColorProvider
    {
        private readonly IPlayerContentColorConfig _config;
        private readonly IPlayerColorProvider _playerColorProvider;

        public PlayerContentColorProvider(IPlayerContentColorConfig config, IPlayerColorProvider playerColorProvider)
        {
            _config = config;
            _playerColorProvider = playerColorProvider;
        }

        public Color GetContentColor(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return _config.GetColor(playerColor);
        }
    }
}