using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    [CreateAssetMenu(fileName = "UnityCellMoveHighlightsConfig", menuName = "Configs/UnityCellMoveHighlightsConfig", order = 1)]
    public class UnityCellMoveHighlightsConfig : ScriptableObject, IUnityCellMoveHighlightsConfig
    {
        [SerializeField] private UnityCellMoveHighlight cellHighlight;
        
        public UnityCellMoveHighlight GetCellHighlightPrefab()
        {
            return cellHighlight;
        }
    }
}