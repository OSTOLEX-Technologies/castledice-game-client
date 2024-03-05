using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public interface IFreedPositionsCalculator
    {
        /// <summary>
        /// This method answers the question "Which positions will have no content on them after the given move?"
        /// </summary>
        /// <param name="afterMove"></param>
        /// <returns></returns>
        List<Vector2Int> GetFreedPositions(AbstractMove afterMove);
    }
}