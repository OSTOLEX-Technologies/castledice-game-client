using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CurrentPlayer
{
    public class CurrentPlayerView : ICurrentPlayerView
    {
        private readonly IPlayerColorProvider _playerColorProvider;
        private readonly GameObject _bluePlayerLabel;
        private readonly GameObject _redPlayerLabel;

        public CurrentPlayerView(IPlayerColorProvider playerColorProvider, GameObject bluePlayerLabel, GameObject redPlayerLabel)
        {
            _playerColorProvider = playerColorProvider;
            _bluePlayerLabel = bluePlayerLabel;
            _redPlayerLabel = redPlayerLabel;
        }

        public void ShowCurrentPlayer(Player player)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(player);
            _bluePlayerLabel.SetActive(playerColor == PlayerColor.Blue);
            _redPlayerLabel.SetActive(playerColor == PlayerColor.Red);
        }
    }
}