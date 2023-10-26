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
            //TODO: Make GetPossibleMoves accept player id instead of Player object and return empty list if player with given id is not found
            var player = _game.GetAllPlayers().Find(p => p.Id == playerId);
            var moves = _game.GetPossibleMoves(player, position);
            return moves;
        }
    }
}