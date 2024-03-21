using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.OLDPVE.MoveSearchers.TraitsEvaluators
{
    public interface IBoardCellsCostCalculator
    {
        /// <summary>
        ///     Returns the cost of each cell on the board from the perspective of the given player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int[,] GetCellsCosts(Player player);

        /// <summary>
        ///     Returns the cost of each cell on the board from the perspective of the given player`s enemy.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int[,] GetInverseCellsCosts(Player player);

        /// <summary>
        ///     Returns the cost of each cell on the board after the given player move from move player perspective.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public int[,] GetCellsCostsAfterMove(AbstractMove move);

        /// <summary>
        ///     Returns the cost of each cell on the board after the given player move from move player`s enemy perspective.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public int[,] GetInverseCellsCostsAfterMove(AbstractMove move);
    }
}