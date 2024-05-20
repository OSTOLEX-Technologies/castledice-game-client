using System;
using System.Collections;
using System.Collections.Generic;
using castledice_game_logic.MovesLogic;
using Src.GameplayView.CellMovesHighlights;
using UnityEngine;
using GameLogicVector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.Components
{
    public class CellMovesHighlightsAnimation : MonoBehaviour
    {
        [Serializable]
        private class PositionToMoveType
        {
            public Vector2Int position;
            public MoveType moveType;
        }

        [SerializeField] private List<PositionToMoveType> sequence;
        [SerializeField] private float oneCellHighlightDuration;
        private Dictionary<GameLogicVector2Int, ICellMoveHighlight> _highlights;
        private bool _isPlaying;
        private int _currentIndex;
        
        public void Init(Dictionary<GameLogicVector2Int, ICellMoveHighlight> highlights)
        {
            _highlights = highlights;
        }
        
        [ContextMenu("Play Animation")]
        public void PlayAnimation()
        {
            StopAnimation();
            _isPlaying = true;
            _currentIndex = 0;
            StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            while (_isPlaying)
            {
                var current = sequence[_currentIndex];
                HighlightCell(current);
                var elapsedTime = 0f;
                while (elapsedTime < oneCellHighlightDuration)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                UnhighlightCell(current);
                _currentIndex++;
                if (_currentIndex >= sequence.Count)
                {
                    _currentIndex = 0;
                }
            }
        }
        
        [ContextMenu("Stop Animation")]
        public void StopAnimation()
        {
            _isPlaying = false;
            var current = sequence[_currentIndex];
            UnhighlightCell(current);
        }
        
        private void HighlightCell(PositionToMoveType positionToMoveType)
        {
            var position = positionToMoveType.position;
            var moveType = positionToMoveType.moveType;
            var highlight = _highlights[(position.x, position.y)];
            highlight.ShowHighlight(moveType);
        }
        
        private void UnhighlightCell(PositionToMoveType positionToMoveType)
        {
            var moveType = positionToMoveType.moveType;
            var position = positionToMoveType.position;
            var highlight = _highlights[(position.x, position.y)];
            highlight.HideHighlight(moveType);
        }
    }
}