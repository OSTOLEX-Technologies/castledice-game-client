using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using Src.PVE.MoveSearchers.TraitsEvaluators;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.PVE.TraitsEvaluators
{
    public class DestructivenessEvaluator : IMoveTraitEvaluator
    {
        private readonly Player _player;
        private readonly Board _board;
        private readonly IBoardStateCalculator _boardStateCalculator;

        public DestructivenessEvaluator(Board board, Player player, IBoardStateCalculator boardStateCalculator)
        {
            _board = board;
            _player = player;
            _boardStateCalculator = boardStateCalculator;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var currentBoardState = _boardStateCalculator.GetCurrentBoardState(move.Player);
            var currentEnemyCount = GetEnemyCount(currentBoardState);
            var boardStateAfterMove = _boardStateCalculator.GetBoardStateAfterPlayerMove(move);
            var enemyCountAfterMove = GetEnemyCount(boardStateAfterMove);
            return (currentEnemyCount - enemyCountAfterMove) / (float)currentEnemyCount;
        }
        
        
        private int GetEnemyCount(CellState[,] currentBoardState)
        {
            int enemyCount = 0;
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (currentBoardState[i, j] == CellState.Enemy)
                    {
                        enemyCount++;
                    }
                }
            }

            return enemyCount;
        }
    }
}