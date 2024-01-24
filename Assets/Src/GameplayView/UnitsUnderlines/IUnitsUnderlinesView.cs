using castledice_game_logic;
using castledice_game_logic.Math;

namespace Src.GameplayView.UnitsUnderlines
{
    public interface IUnitsUnderlinesView
    {
        void ShowUnderline(Vector2Int position, Player player);
        void HideUnderline(Vector2Int position);
    }
}