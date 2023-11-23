using castledice_game_logic.GameObjects;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public interface IContentViewProvider
    {
        ContentView GetContentView(Content content);
    }
}