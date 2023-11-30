using CastleGO = castledice_game_logic.GameObjects.Castle;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public interface ICastleModelProvider
    {
        GameObject GetCastleModel(CastleGO castle);
    }
}