using castledice_game_data_logic.ConfigsData;

namespace Src.GameplayPresenter.Cells
{
    public interface ICellViewMapProvider
    {
        CellViewData[,] GetCellViewMap(BoardData boardData);
    }
}