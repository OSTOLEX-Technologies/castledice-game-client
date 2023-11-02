using System;
using System.Collections.Generic;
using castledice_game_logic;
using Src.GameplayView.CellsContent.ContentCreation;

namespace Src.GameplayView.PlayersColor
{
    public class DuelPlayerColorProvider : IPlayerColorProvider
    {
        private readonly List<Player> _players;

        public DuelPlayerColorProvider(List<Player> players)
        {
            if (players.Count != 2)
            {
                throw new ArgumentException("Players list should contain exactly two players, as it is duel mode");
            }
            _players = players;
        }

        public PlayerColor GetPlayerColor(Player player)
        {
            if (_players[0] == player)
            {
                return PlayerColor.Blue;
            }

            if (_players[1] == player)
            {
                return PlayerColor.Red;
            } 
            throw new ArgumentException("Player not found in the list");
        }
    }
}