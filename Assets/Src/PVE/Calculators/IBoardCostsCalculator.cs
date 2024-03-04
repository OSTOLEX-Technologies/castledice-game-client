using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    /// <summary>
    /// Methods of this interface should return matrix each cell of which contains the cost of performing a move to the cell.
    /// If no move is possible on the cell, then the cost should be int.MaxValue.
    /// </summary>
    public interface IBoardCostsCalculator
    {
        int[,] GetCosts(Player forPlayer);
        int[,] GetCostsAfterMove(Player forPlayer, AbstractMove afterMove);
    }
}