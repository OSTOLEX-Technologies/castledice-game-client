using Src.GameplayPresenter.CellsGeneration;

namespace Src.GameplayView.Cells
{
    public interface ICellsViewGenerator
    {
        void GenerateCellsView(CellViewData[,] cellsViewMap);
    }
}