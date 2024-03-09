using System.Collections.Generic;
using castledice_game_logic;
using castledice_game_logic.Math;

namespace Src.PVE
{
    public interface IUnitsPositionsSearcher
    {
        /// <summary>
        /// Return positions of all player`s units.
        /// Unit is an entity that can be placed by player on the cell and replaced by other players.
        /// </summary>
        /// <param name="forPlayer"></param>
        /// <returns></returns>
        List<Vector2Int> GetUnitsPositions(Player forPlayer);
    }
}