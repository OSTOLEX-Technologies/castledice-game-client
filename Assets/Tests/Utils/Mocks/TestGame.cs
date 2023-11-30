using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.GameConfiguration;
using castledice_game_logic.GameObjects;
using castledice_game_logic.MovesLogic;

namespace Tests.Utils.Mocks
{
    public class TestGame : Game
    {
        public TestGame(List<Player> players, BoardConfig boardConfig, PlaceablesConfig placeablesConfig, IDecksList decksList) : base(players, boardConfig, placeablesConfig, decksList)
        {
        }
        
        public void ForceWin(Player winner)
        {
            OnWin(winner);
        }
        
        public void ForceDraw()
        {
            OnDraw();
        }

        public void InvokeMoveApplied(AbstractMove move)
        {
            OnMoveApplied(move);
        }
    }
}