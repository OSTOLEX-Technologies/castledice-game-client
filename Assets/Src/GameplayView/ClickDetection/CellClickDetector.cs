using System;
using JetBrains.Annotations;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.ClickDetection
{
    public class CellClickDetector : MonoBehaviour, ICellClickDetector, IClickable
    {
        [CanBeNull] public event EventHandler<Vector2Int> Clicked;

        private Vector2Int _position;
        
        public void Init(Vector2Int position)
        {
            _position = position;
        }
        
        public void Click()
        {
            Clicked?.Invoke(this, _position);
        }
    }
}