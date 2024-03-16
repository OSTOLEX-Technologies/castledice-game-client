using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;

namespace Src.GameplayView.ClientMoves
{
    public interface IMovesView
    {
        void ShowMovesList(List<AbstractMove> moves);
        event EventHandler<Vector2Int> PositionClicked;
        event EventHandler<AbstractMove> MovePicked;
    }
}