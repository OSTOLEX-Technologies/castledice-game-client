using Src.GameplayPresenter.Cells;

namespace Src.GameplayView.Cells
{
    public interface ICellsViewGenerator
    {
        void GenerateCellsView(CellViewData[,] cellsViewMap);
    }
}