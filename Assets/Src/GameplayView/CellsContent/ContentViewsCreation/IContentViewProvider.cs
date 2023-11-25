using castledice_game_logic.GameObjects;

namespace Src.GameplayView.CellsContent.ContentViewsCreation
{
    public interface IContentViewProvider
    {
        ContentView GetContentView(Content content);
    }
}