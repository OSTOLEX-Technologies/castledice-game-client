using castledice_game_logic.Math;

namespace Src.GameplayView.ClickDetection
{
    public interface IUnityCellClickDetectorsFactory
    {
        UnityCellClickDetector GetDetector(Vector2Int position);
    }
}