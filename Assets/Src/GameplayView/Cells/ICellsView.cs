using castledice_game_logic;
using Src.GameplayPresenter.Cells;

namespace Src.GameplayView.Cells
{
    public interface ICellsView
    {
        public void GenerateCells(CellType cellType, CellViewData[,] cellViewMap);
    }
}