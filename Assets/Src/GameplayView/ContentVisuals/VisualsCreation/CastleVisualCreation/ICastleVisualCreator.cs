using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation
{
    public interface ICastleVisualCreator
    {
        CastleVisual GetCastleVisual(CastleGO castle);
    }
}