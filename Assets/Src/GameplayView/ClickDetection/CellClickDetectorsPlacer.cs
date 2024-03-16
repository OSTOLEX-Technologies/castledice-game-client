using System.Collections.Generic;
using Src.GameplayView.Grid;

namespace Src.GameplayView.ClickDetection
{
    public class CellClickDetectorsPlacer
    {
        private readonly IGrid _grid;
        private readonly ICellClickDetectorsFactory _detectorsFactory;
        
        public CellClickDetectorsPlacer(IGrid grid, ICellClickDetectorsFactory detectorsFactory)
        {
            _grid = grid;
            _detectorsFactory = detectorsFactory;
        }

        public List<ICellClickDetector> PlaceDetectors()
        {
            var detectors = new List<ICellClickDetector>();
            foreach (var cell in _grid)
            {
                var position = cell.Position;
                var detector = _detectorsFactory.GetDetector(position);
                cell.AddChild(detector.gameObject);
                detectors.Add(detector);
            }

            return detectors;
        } 
    }
}