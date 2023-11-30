using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public interface ICastleModelPrefabProvider
    {
        GameObject GetCastleModelPrefab(PlayerColor color);
    }
}