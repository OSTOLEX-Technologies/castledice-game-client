using System.Threading.Tasks;
using castledice_game_logic;
using Src.GameplayPresenter.GameWrappers;
using Src.OLDPVE.MoveSearchers;
using Src.PVE.Providers;

namespace Src.OLDPVE
{
    public class Bot
    {
        private readonly IBestMoveSearcher _bestMoveSearcher;
        private readonly int _botPlayerId;
        private readonly Game _game;
        private readonly ILocalMoveApplier _localMoveApplier;
        private readonly ITotalPossibleMovesProvider _totalPossibleMovesProvider;

        public Bot(ILocalMoveApplier localMoveApplier, ITotalPossibleMovesProvider totalPossibleMovesProvider,
            IBestMoveSearcher bestMoveSearcher, Game game, int botPlayerId)
        {
            _localMoveApplier = localMoveApplier;
            _totalPossibleMovesProvider = totalPossibleMovesProvider;
            _bestMoveSearcher = bestMoveSearcher;
            _game = game;
            _botPlayerId = botPlayerId;
            _game.MoveApplied += (_, _) => TryMakeMove();
            var player = _game.GetPlayer(_botPlayerId);
            player.ActionPoints.ActionPointsIncreased += (_, _) => TryMakeMove();
            if (_game.GetCurrentPlayer().Id == _botPlayerId) TryMakeMove();
        }


        private async void TryMakeMove()
        {
            if (_game.GetCurrentPlayer() != _game.GetPlayer(_botPlayerId)) return;
            await Task.Delay(500);
            var possibleMoves = _totalPossibleMovesProvider.GetTotalPossibleMoves(_botPlayerId);
            if (possibleMoves.Count == 0) return;

            var bestMove = _bestMoveSearcher.GetBestMove(possibleMoves);
            _localMoveApplier.ApplyMove(bestMove);
        }
    }
}