using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation.KnightsCreation
{
    public interface IKnightViewFactory
    {
        KnightView GetKnightView(Knight knight);
    }
}