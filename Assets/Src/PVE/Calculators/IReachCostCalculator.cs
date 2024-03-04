using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    /// <summary>
    /// Methods of this interface should answers the questions "How many action points should given player spend to reach given position?"
    /// Reach cost includes the cost of placing unit on the position or performing capture hit.
    /// If no move is possible on given position or it cannot be reached, the method should return int.MaxValue.
    /// </summary>
    public interface IReachCostCalculator
    {
        /// <summary>
        /// This method returns the minimum reach cost in the current game state.
        /// </summary>
        /// <param name="forPlayer"></param>
        /// <param name="toPosition"></param>
        /// <returns></returns>
        int GetMinReachCost(Player forPlayer, Vector2Int toPosition);
        /// <summary>
        /// This method returns the minimum reach cost in the game state that will occur after given move.
        /// Player that performs move <b>afterMove</b> can be different than <b>forPlayer</b>.
        /// </summary>
        /// <param name="forPlayer"></param>
        /// <param name="toPosition"></param>
        /// <param name="afterMove"></param>
        /// <returns></returns>
        int GetMinReachCostAfterMove(Player forPlayer, Vector2Int toPosition, AbstractMove afterMove);
    }
}