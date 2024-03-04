using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;

namespace Src.PVE.Calculators
{
    public interface IUnitsPositionsCalculator
    {
        List<Vector2Int> GetUnitsPositions(Player forPlayer);
    }
}