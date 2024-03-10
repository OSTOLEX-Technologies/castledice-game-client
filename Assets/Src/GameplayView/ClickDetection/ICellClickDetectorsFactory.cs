using castledice_game_logic.Math;

namespace Src.GameplayView.ClickDetection
{
    public interface ICellClickDetectorsFactory
    {
        CellClickDetector GetDetector(Vector2Int position);
    }
}