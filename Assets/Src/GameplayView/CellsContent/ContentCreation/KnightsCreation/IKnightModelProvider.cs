using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation.KnightsCreation
{
    /// <summary>
    /// By model we mean some 3D, 2D or any other visual representation of knight.
    /// </summary>
    public interface IKnightModelProvider
    {
        GameObject GetKnightModel(PlayerColor color);
    }
}