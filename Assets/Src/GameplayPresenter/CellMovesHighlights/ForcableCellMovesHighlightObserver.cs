using System;
using castledice_game_logic;
using castledice_game_logic.MovesLogic;
using UnityEngine;

namespace Src.GameplayPresenter.CellMovesHighlights
{
    public class ForcableCellMovesHighlightObserver : MonoBehaviour, ICellMovesHighlightObserver
    {
        public event Action TimeToHighlight;
        public event Action TimeToHide;
        
        private Game _game;
        private Player _player;
        
        public void Init(Game game, Player player)
        {
            _game = game;
            _player = player;
            _game.MoveApplied += (sender, move) => OnMoveApplied(move);
            _player.ActionPoints.ActionPointsIncreased += (sender, amount) => OnActionPointsIncreased();
        }

        public void OnMoveApplied(AbstractMove move)
        {
            if (move.Player == _player)
            {
                if (_player.ActionPoints.Amount <= 0)
                {
                    TimeToHide?.Invoke();
                }
                else
                {
                    TimeToHighlight?.Invoke();
                }
            }
        }

        public void OnActionPointsIncreased()
        {
            TimeToHighlight?.Invoke();   
        }
        
        public void ForceHighlight()
        {
            TimeToHighlight?.Invoke();
        }
        
        public void ForceHide()
        {
            TimeToHide?.Invoke();
        }
    }
}