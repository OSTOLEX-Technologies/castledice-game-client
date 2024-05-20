using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.CellMovesHighlights
{
    public class UnityCellMovesHighlightsToggle : MonoBehaviour
    {
        private Dictionary<Vector2Int, UnityCellMoveHighlight> _highlights;
        
        public void Init(Dictionary<Vector2Int, UnityCellMoveHighlight> highlights)
        {
            _highlights = highlights;
        }
        
        public void ToggleAllHighlights(bool active)
        {
            foreach (var highlight in _highlights.Values)
            {
                highlight.gameObject.SetActive(active);
            }
        }
        
        public void ToggleHighlight(Vector2Int position, bool active)
        {
            _highlights[position].gameObject.SetActive(active);
        }
    }
}