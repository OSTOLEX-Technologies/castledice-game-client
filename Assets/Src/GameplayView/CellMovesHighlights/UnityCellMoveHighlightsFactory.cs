using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    public class UnityCellMoveHighlightsFactory : MonoBehaviour, IUnityCellMoveHighlightsFactory
    {
        private IUnityCellMoveHighlightsConfig _config;

        public void Init(IUnityCellMoveHighlightsConfig config)
        {
            _config = config;
        }
        
        public UnityCellMoveHighlight GetCellMoveHighlight()
        {
            return Instantiate(_config.GetCellHighlightPrefab());
        }
    }
}