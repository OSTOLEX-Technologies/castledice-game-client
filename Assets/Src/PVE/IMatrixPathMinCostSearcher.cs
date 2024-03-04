using castledice_game_logic.Math;

namespace Src.PVE
{
    public interface IMatrixPathMinCostSearcher
    {
        /// <summary>
        /// Finds the path with the minimum sum of costs from the cell "from" to the cell "to".
        /// Path can go only up, down, left, right and by diagonals.
        /// Next comes an example of work.
        /// Given matrix:
        /// 0 3 4 4 1
        /// 1 3 4 4 1
        /// 1 3 3 3 1
        /// 1 1 1 1 3
        /// 1 1 1 1 0
        /// The minimum cost path from (0, 0) to (4, 4) is 6.
        /// The cost does not include the cost of the cell "from", but includes the cost of the cell "to".
        /// That is, if in the given matrix the cell "from" would have the cost 5 and the cell "to" would have the cost 7,
        /// the minimum cost path from "from" to "to" would be 13.
        /// If the sum of costs of the path is greater than int.MaxValue, the method should return int.MaxValue.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        int GetMinCost(int[,] matrix, Vector2Int from, Vector2Int to);
    }
}