using castledice_game_data_logic;

namespace Src.GameplayPresenter.Cells
{
    public interface ICellViewMapProvider
    {
        CellViewData[,] GetCellViewMap(GameStartData gameStartData);
    }
}