using castledice_game_data_logic.ConfigsData;

namespace Src.GameplayPresenter.CellsGeneration
{
    public interface ICellViewMapProvider
    {
        CellViewData[,] GetCellViewMap(BoardData boardData);
    }
}