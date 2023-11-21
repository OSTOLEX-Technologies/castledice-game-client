namespace Src.GameplayView.CellMovesHighlights
{
    /// <summary>
    /// Classes implementing this interface are responsible for providing prefabs for UnityCellMoveHighlight.
    /// </summary>
    public interface IUnityCellMoveHighlightsConfig
    {
        UnityCellMoveHighlight GetCellHighlightPrefab();
    }
}