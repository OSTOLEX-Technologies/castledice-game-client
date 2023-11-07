using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public class CellMovesListProvider : ICellMovesListProvider
    {
        private readonly Game _game;

        public CellMovesListProvider(Game game)
        {
            _game = game;
        }

        public List<CellMove> GetCellMovesList(int playerId)
        {
            return _game.GetCellMoves(playerId);
        }
    }
}