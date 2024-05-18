using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    public class UnityCellMovesHighlightsToggle : MonoBehaviour
    {
        private List<UnityCellMoveHighlight> _highlights;
        
        public void Init(List<UnityCellMoveHighlight> highlights)
        {
            _highlights = highlights;
        }
        
        public void SetHighlightsActive(bool value)
        {
            foreach (var highlight in _highlights)
            {
                highlight.gameObject.SetActive(value);
            }
        }
    }
}