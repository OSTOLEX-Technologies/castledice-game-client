using castledice_game_logic.GameObjects;

namespace Src.GameplayView.ContentVisuals.VisualsCreation
{
    public interface IContentVisualCreator
    {
        ContentVisual GetVisual(Content content);
    }
}