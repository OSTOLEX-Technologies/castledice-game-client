using castledice_game_logic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.GameOver
{
    public class GameOverView : IGameOverView
    {
        private readonly IPlayerColorProvider _playerColorProvider;
        private readonly GameObject _bluePlayerWinScreen;
        private readonly GameObject _redPlayerWinScreen;
        private readonly GameObject _drawScreen;

        public GameOverView(IPlayerColorProvider playerColorProvider, GameObject bluePlayerWinScreen, GameObject redPlayerWinScreen, GameObject drawScreen)
        {
            _playerColorProvider = playerColorProvider;
            _bluePlayerWinScreen = bluePlayerWinScreen;
            _redPlayerWinScreen = redPlayerWinScreen;
            _drawScreen = drawScreen;
        }

        public void ShowWin(Player winner)
        {
            var playerColor = _playerColorProvider.GetPlayerColor(winner);
            _bluePlayerWinScreen.SetActive(playerColor == PlayerColor.Blue);
            _redPlayerWinScreen.SetActive(playerColor == PlayerColor.Red);
        }

        public void ShowDraw()
        {
            _drawScreen.SetActive(true);
        }
    }
}