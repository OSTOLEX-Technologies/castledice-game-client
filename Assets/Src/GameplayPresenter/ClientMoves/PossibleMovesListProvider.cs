using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayPresenter.ClientMoves
{
    public class PossibleMovesListProvider : IPossibleMovesListProvider
    {
        private readonly Game _game;
        
        public PossibleMovesListProvider(Game game)
        {
            _game = game;
        }
        
        public List<AbstractMove> GetPossibleMoves(Vector2Int position, int playerId)
        {
            return _game.GetPossibleMoves(playerId, position);
        }
    }
}