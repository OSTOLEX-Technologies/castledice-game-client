using System;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    /// <summary>
    /// This interface describes classes that should react on situations when its time to highlight cell moves.
    /// </summary>
    public interface ICellMovesHighlightObserver
    {
        public event Action TimeToHighlight;
        public event Action TimeToHide;
    }
}