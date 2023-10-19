using castledice_game_data_logic;

namespace Src.GameplayPresenter.CellsView
{
    public interface ICellViewMapProvider
    {
        CellViewData[,] GetCellViewMap(GameStartData gameStartData);
    }
}