using castledice_game_logic;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public interface ISimpleArmyStateCalculator
    {
        SimpleCellState[,] GetArmyState(Player forPlayer);
        SimpleCellState[,] GetArmyStateAfterMove(Player forPlayer, AbstractMove afterMove);
    }
}