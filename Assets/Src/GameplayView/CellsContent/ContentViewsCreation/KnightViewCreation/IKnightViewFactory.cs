using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    public interface IKnightViewFactory
    {
        KnightView GetKnightView(Knight knight);
    }
}