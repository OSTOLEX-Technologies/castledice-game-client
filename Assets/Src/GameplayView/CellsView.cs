using castledice_game_logic;
using Src.GameplayPresenter;
using Src.GameplayPresenter.Cells;

namespace Src.GameplayView
{
    public abstract class CellsView
    {
        public abstract void GenerateCells(CellType cellType, CellViewData[,] cellViewMap);
    }
}