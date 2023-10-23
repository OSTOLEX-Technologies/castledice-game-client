using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public interface IGameObjectsGrid
    {
        void AddParent(Vector2Int position, GameObject parent);
        GameObject GetParent(Vector2Int position);
        bool RemoveParent(Vector2Int position);
        void AddChild(Vector2Int position, GameObject child);
        bool RemoveChild(Vector2Int position, GameObject child);
    }
}