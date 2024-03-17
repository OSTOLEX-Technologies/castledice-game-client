using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.PVE.Calculators
{
    public interface IBasePositionsCalculator
    {
        List<Vector2Int> GetBasePositions(Player forPlayer);
        List<Vector2Int> GetBasePositionsAfterMove(Player forPlayer, AbstractMove afterMove);
    }
}