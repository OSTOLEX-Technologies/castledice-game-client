using System.Collections.Generic;
using castledice_game_logic.Math;

namespace Src.GameplayView.CellMovesHighlights
{
    public interface ICellHighlightsPlacer
    {
        /// <summary>
        /// This method places highlights on the scene/field/grid or whatever is used to display the game and returns a dictionary of highlights corresponding to their positions.
        /// </summary>
        /// <returns></returns>
        Dictionary<Vector2Int, ICellHighlight> PlaceHighlights();
    }
}