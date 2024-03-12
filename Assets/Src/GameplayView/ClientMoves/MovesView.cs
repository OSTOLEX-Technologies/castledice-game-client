using System;
using System.Collections.Generic;
using castledice_game_logic.Math;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.ClickDetection;

namespace Src.GameplayView.ClientMoves
{
    public class MovesView : IMovesView
    {
        public event EventHandler<Vector2Int> PositionClicked;
        public event EventHandler<AbstractMove> MovePicked;
        
        private List<ICellClickDetector> _detectors;

        public MovesView(List<ICellClickDetector> detectors)
        {
            _detectors = detectors;
            foreach (var detector in _detectors)
            {
                detector.Clicked += OnClickDetected;
            }
        }

        private void OnClickDetected(object sender, Vector2Int position)
        {
            PositionClicked?.Invoke(this, position);
        }
        
        public void ShowMovesList(List<AbstractMove> moves)
        {
            //TODO: In the future there must be logic for demonstrating list of moves to the player.

            if (moves.Count > 0)
            {
                var move = moves[0];
                MovePicked?.Invoke(this, move);
            }
        }
    }
}