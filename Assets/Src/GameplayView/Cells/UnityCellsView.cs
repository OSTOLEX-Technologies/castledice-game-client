using castledice_game_logic;
using Src.GameplayPresenter.Cells;
using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class UnityCellsView : MonoBehaviour, ICellsView
    {
        private ICellsGeneratorsFactory _cellsGeneratorsFactory;

        public void Init(ICellsGeneratorsFactory cellsGeneratorsFactory)
        {
            _cellsGeneratorsFactory = cellsGeneratorsFactory;
        }
        
        public void GenerateCells(CellType cellType, CellViewData[,] cellViewMap)
        {
            var cellsGenerator = _cellsGeneratorsFactory.GetGenerator(cellType);
            cellsGenerator.GenerateCells(cellViewMap);
        }
    }
}