using castledice_game_data_logic.Content.Placeable;

namespace Src.GameplayPresenter.Cells
{
    public interface ICellViewMapProvider
    {
        CellViewData[,] GetCellViewMap(BoardData boardData);
    }
}