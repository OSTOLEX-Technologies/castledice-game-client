using castledice_game_logic;
using castledice_game_logic.Math;

namespace Src.GameplayView.NewUnitsHighlights
{
    public interface INewUnitsHighlightsView
    {
        public void ShowHighlight(Vector2Int position, Player player);
        public void HideHighlights();
    }
}