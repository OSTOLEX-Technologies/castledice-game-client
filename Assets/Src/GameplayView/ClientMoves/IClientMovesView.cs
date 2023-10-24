using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.ClientMoves
{
    public interface IClientMovesView
    {
        void ShowMovesList(List<AbstractMove> moves);
        
        /// <summary>
        /// This method should invoke <see cref="PositionClicked"/>  event
        /// </summary>
        /// <param name="position"></param>
        void ClickOnPosition(Vector2Int position);
        
        /// <summary>
        /// This method should invoke <see cref="MovePicked"/>  event
        /// </summary>
        /// <param name="move"></param>
        void PickMove(AbstractMove move);

        event EventHandler<Vector2Int> PositionClicked;
        event EventHandler<AbstractMove> MovePicked;
    }
}