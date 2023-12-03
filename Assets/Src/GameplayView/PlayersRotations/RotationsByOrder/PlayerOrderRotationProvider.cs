using System.Collections.Generic;
using castledice_game_logic;
using Src.GameplayView.PlayersNumbers;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByOrder
{
    public class PlayerOrderRotationProvider : IPlayerRotationProvider
    {
        private readonly IPlayerOrderRotationConfig _config;
        private readonly IPlayerNumberProvider _playerNumberProvider;

        public PlayerOrderRotationProvider(IPlayerOrderRotationConfig config, IPlayerNumberProvider playerNumberProvider)
        {
            _config = config;
            _playerNumberProvider = playerNumberProvider;
        }

        public Vector3 GetRotation(Player player)
        {
            var playerNumber = _playerNumberProvider.GetPlayerNumber(player);
            var rotation = _config.GetRotation(playerNumber);
            return rotation;
        }
    }
}