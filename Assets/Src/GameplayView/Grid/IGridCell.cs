using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public interface IGridCell : IEnumerable<GameObject>
    {
        Vector2Int Position { get; }
        
        void AddChild(GameObject child);
        bool RemoveChild(GameObject child);
    }
}