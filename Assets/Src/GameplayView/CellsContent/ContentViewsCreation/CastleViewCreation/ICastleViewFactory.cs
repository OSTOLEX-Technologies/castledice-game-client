using Src.GameplayView.CellsContent.ContentViews;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    public interface ICastleViewFactory
    {
        CastleView GetCastleView(CastleGO castle);
    }
}