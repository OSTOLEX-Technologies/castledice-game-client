using System.Collections.Generic;
using castledice_game_logic;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByOrder
{
    public class PlayerOrderRotationProvider : IPlayerRotationProvider
    {
        private readonly IPlayerOrderRotationConfig _config;
        private readonly List<Player> _orderedPlayers;

        public PlayerOrderRotationProvider(IPlayerOrderRotationConfig config, List<Player> orderedPlayers)
        {
            _config = config;
            _orderedPlayers = orderedPlayers;
        }

        public Vector3 GetRotation(Player player)
        {
            var playerOrder = _orderedPlayers.IndexOf(player) + 1;
            var rotation = _config.GetRotation(playerOrder);
            return rotation;
        }
    }
}