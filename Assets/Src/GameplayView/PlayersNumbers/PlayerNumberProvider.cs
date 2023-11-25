using System;
using System.Collections.Generic;
using castledice_game_logic;

namespace Src.GameplayView.PlayersNumbers
{
    public class PlayerNumberProvider : IPlayerNumberProvider
    {
        private readonly List<Player> _players;

        public PlayerNumberProvider(List<Player> players)
        {
            _players = players;
        }

        public int GetPlayerNumber(Player player)
        {
            if (_players.Contains(player))
            {
                return _players.IndexOf(player) + 1;
            }

            throw new ArgumentException("Player is not in players list");
        }
    }
}