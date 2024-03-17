using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public interface ILostPositionsCalculator
    {
        /// <summary>
        /// This methods predicts on which positions given player will loose units after given move.
        /// If no units will be destroyed after given move, this method should return empty list.
        /// </summary>
        /// <param name="forPlayer"></param>
        /// <param name="afterMove"></param>
        /// <returns></returns>
        List<Vector2Int> GetLostPositions(Player forPlayer, AbstractMove afterMove);
    }
}