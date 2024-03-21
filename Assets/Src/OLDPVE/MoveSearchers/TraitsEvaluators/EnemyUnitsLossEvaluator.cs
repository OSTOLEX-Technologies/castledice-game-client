using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    /// <summary>
    ///     This class evaluates how many enemy units will be lost after a move.
    ///     Method EvaluateTrait returns the percentage of the change in the number of enemy units.
    /// </summary>
    public class EnemyUnitsLossEvaluator : IMoveTraitEvaluator
    {
        private readonly IBoardCellsStateCalculator _boardCellsStateCalculator;

        public EnemyUnitsLossEvaluator(IBoardCellsStateCalculator boardCellsStateCalculator)
        {
            _boardCellsStateCalculator = boardCellsStateCalculator;
        }

        public float EvaluateTrait(AbstractMove move)
        {
            var currentBoardState = _boardCellsStateCalculator.GetCurrentBoardState(move.Player);
            var currentEnemyCount = GetEnemyCount(currentBoardState);
            var boardStateAfterMove = _boardCellsStateCalculator.GetBoardStateAfterPlayerMove(move);
            var enemyCountAfterMove = GetEnemyCount(boardStateAfterMove);
            return (currentEnemyCount - (float)enemyCountAfterMove) / currentEnemyCount;
        }


        private int GetEnemyCount(CellState[,] currentBoardState)
        {
            var enemyCount = 0;
            for (var i = 0; i < currentBoardState.GetLength(0); i++)
            for (var j = 0; j < currentBoardState.GetLength(1); j++)
                if (currentBoardState[i, j] == CellState.Enemy)
                    enemyCount++;

            return enemyCount;
        }
    }
}