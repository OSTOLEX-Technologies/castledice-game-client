using System.Collections.Generic;
using System.Linq;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.GameWrappers
{
    public class TotalPossibleMovesProvider : ITotalPossibleMovesProvider
    {
        private readonly Game _game;
        
        public TotalPossibleMovesProvider(Game game)
        {
            _game = game;
        }
        
        public List<AbstractMove> GetTotalPossibleMoves(int playerId)
        {
            var cellMoves = _game.GetCellMoves(playerId);
            var totalMoves = new List<AbstractMove>();
            foreach (var possibleMoves in cellMoves.Select(cellMove => _game.GetPossibleMoves(playerId, cellMove.Cell.Position)))
            {
                totalMoves.AddRange(possibleMoves);
            }

            return totalMoves;
        }
    }
}