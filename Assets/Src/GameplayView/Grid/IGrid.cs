using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public interface IGrid : IEnumerable<IGridCell>
    {
        void AddCell(Vector2Int gamePosition, Vector3 scenePosition);
        bool RemoveCell(Vector2Int gamePosition);
        IGridCell GetCell(Vector2Int gamePosition);
    }
}