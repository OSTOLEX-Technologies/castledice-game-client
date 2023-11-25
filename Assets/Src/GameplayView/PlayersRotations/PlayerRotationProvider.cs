using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations
{
    public class PlayerRotationProvider : IPlayerRotationProvider
    {
        private readonly IPlayerRotationConfig _playerRotationConfig;
        
        public PlayerRotationProvider(IPlayerRotationConfig playerRotationConfig)
        {
            _playerRotationConfig = playerRotationConfig;
        } 
        
        public Vector3 GetRotation(PlayerColor playerColor)
        {
            return _playerRotationConfig.GetRotations()[playerColor];
        }
    }
}