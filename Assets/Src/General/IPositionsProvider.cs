using System.Collections.Generic;
using castledice_game_logic.Math;

namespace Src.General
{
    public interface IPositionsProvider
    {
        List<Vector2Int> GetPositions();
    }
}