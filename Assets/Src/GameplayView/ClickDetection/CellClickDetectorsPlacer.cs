using System.Collections.Generic;
using Src.GameplayView.Grid;

namespace Src.GameplayView.ClickDetection
{
    public class CellClickDetectorsPlacer
    {
        private IGrid _grid;
        private IUnityCellClickDetectorsFactory _detectorsFactory;
        
        public CellClickDetectorsPlacer(IGrid grid, IUnityCellClickDetectorsFactory detectorsFactory)
        {
            _grid = grid;
            _detectorsFactory = detectorsFactory;
        }

        public List<ICellClickDetector> PlaceDetectors()
        {
            throw new System.NotImplementedException();
        } 
    }
}