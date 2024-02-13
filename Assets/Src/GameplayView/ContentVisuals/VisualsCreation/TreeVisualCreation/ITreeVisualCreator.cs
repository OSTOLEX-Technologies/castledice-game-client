using castledice_game_logic.GameObjects;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation
{
    public interface ITreeVisualCreator
    {
        TreeVisual GetTreeVisual(Tree tree);
    }
}