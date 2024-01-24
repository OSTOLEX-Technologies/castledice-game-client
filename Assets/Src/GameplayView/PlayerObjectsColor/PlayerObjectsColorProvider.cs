using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayerObjectsColor
{
    public class PlayerObjectsColorProvider : IPlayerObjectsColorProvider
    {
        private readonly IPlayerObjectsColorConfig _config;
        private readonly IPlayerColorProvider _playerColorProvider;

        public PlayerObjectsColorProvider(IPlayerObjectsColorConfig config, IPlayerColorProvider playerColorProvider)
        {
            _config = config;
            _playerColorProvider = playerColorProvider;
        }

        public Color GetColor(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            return _config.GetColor(playerColor);
        }
    }
}