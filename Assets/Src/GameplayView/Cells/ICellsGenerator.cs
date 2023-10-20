using Src.GameplayPresenter.Cells;

namespace Src.GameplayView.Cells
{
    public interface ICellsGenerator
    {
        void GenerateCells(CellViewData[,] cellsViewMap);
    }
}