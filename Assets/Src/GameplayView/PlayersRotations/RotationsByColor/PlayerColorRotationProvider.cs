using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByColor
{
    public class PlayerColorRotationProvider : IPlayerRotationProvider
    {
        private readonly IPlayerColorRotationConfig _playerColorRotationConfig;
        private readonly IPlayerColorProvider _playerColorProvider;
        
        public PlayerColorRotationProvider(IPlayerColorRotationConfig playerColorRotationConfig, IPlayerColorProvider playerColorProvider)
        {
            _playerColorRotationConfig = playerColorRotationConfig;
            _playerColorProvider = playerColorProvider;
        }
        
        public Vector3 GetRotation(Player player)
        {
            var color = _playerColorProvider.GetPlayerColor(player);
            var rotation = _playerColorRotationConfig.GetRotation(color);
            return rotation;
        }
    }
}