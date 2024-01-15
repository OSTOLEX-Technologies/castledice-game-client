using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayPresenter.ClientMoves;

namespace Src.GameplayPresenter.GameWrappers
{
    public class PossiblePositionMovesProvider : IPossiblePositionMovesProvider
    {
        private readonly Game _game;
        
        public PossiblePositionMovesProvider(Game game)
        {
            _game = game;
        }
        
        public List<AbstractMove> GetPossibleMoves(Vector2Int position, int playerId)
        {
            return _game.GetPossibleMoves(playerId, position);
        }
    }
}