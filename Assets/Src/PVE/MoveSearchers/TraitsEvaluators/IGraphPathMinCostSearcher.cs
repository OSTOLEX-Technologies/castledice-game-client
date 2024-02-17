using castledice_game_logic.Math;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public interface IGraphPathMinCostSearcher
    {
        public int FindMinCost(int[,] graph, Vector2Int start, Vector2Int end);
    }
}