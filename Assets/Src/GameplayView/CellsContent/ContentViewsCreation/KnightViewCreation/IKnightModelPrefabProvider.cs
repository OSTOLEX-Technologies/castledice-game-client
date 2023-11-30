using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public interface IKnightModelPrefabProvider
    {
        GameObject GetKnightModelPrefab(PlayerColor color);
    }
}