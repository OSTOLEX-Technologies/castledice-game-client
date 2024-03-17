using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Providers
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
            var totalPossibleMoves = new List<AbstractMove>();
            foreach (var cellMove in cellMoves)
            {
                var possibleMoves = _game.GetPossibleMoves(playerId, cellMove.Cell.Position);
                totalPossibleMoves.AddRange(possibleMoves);
            }
            return totalPossibleMoves;
        }
    }
}