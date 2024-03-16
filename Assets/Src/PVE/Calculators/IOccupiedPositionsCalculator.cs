using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public interface IOccupiedPositionsCalculator
    {
        List<Vector2Int> GetOccupiedPositions(Player forPlayer);
        List<Vector2Int> GetOccupiedPositionsAfterMove(Player forPlayer, AbstractMove afterMove);
    }
}