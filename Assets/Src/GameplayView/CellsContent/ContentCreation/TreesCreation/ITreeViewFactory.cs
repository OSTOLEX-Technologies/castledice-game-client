using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation.TreesCreation
{
    public interface ITreeViewFactory
    {
        TreeView GetTreeView(Tree tree);
    }
}