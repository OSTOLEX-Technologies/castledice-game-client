using UnityEngine;

namespace Src.GameplayView.CellMovesHighlights
{
    /// <summary>
    /// Classes implementing this interface are responsible for instantiating cell move highlights.
    /// </summary>
    public interface IUnityCellMoveHighlightsFactory
    {
        UnityCellMoveHighlight GetCellMoveHighlight();
    }
}