using castledice_game_logic.GameObjects;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation
{
    public interface IKnightVisualCreator
    {
        KnightVisual GetKnightVisual(Knight knight);
    }
}