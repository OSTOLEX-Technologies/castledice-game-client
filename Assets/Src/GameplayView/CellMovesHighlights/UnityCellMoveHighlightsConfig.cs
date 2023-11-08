using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    public class UnityCellMoveHighlightsConfig : ScriptableObject, IUnityCellMoveHighlightsConfig
    {
        [SerializeField] private UnityCellMoveHighlight cellHighlight;
        
        public UnityCellMoveHighlight GetCellHighlightPrefab()
        {
            return cellHighlight;
        }
    }
}