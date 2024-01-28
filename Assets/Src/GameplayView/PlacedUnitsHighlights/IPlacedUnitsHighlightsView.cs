using castledice_game_logic;
using castledice_game_logic.Math;

namespace Src.GameplayView.PlacedUnitsHighlights
{
    public interface IPlacedUnitsHighlightsView
    {
        void ShowHighlight(Vector2Int position, Player player);
        void HideHighlight(Vector2Int position);
    }
}