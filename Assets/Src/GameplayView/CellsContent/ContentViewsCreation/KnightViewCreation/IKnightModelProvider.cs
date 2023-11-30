using castledice_game_logic.GameObjects;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    /// <summary>
    /// By model we mean some 3D, 2D or any other visual representation of knight.
    /// </summary>
    public interface IKnightModelProvider
    {
        GameObject GetKnightModel(Knight knight);
    }
}