using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public class UnityGrid : MonoBehaviour, IGrid
    {
        private Dictionary<Vector2Int, UnityGridCell> _cells = new();
        
        public IEnumerator<IGridCell> GetEnumerator()
        {
            return _cells.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddCell(Vector2Int gamePosition, Vector3 scenePosition)
        {
            if (_cells.ContainsKey(gamePosition))
            {
                throw new InvalidOperationException("Cell already exists on given game position " + gamePosition);
            }
            var cell = new GameObject().AddComponent<UnityGridCell>();
            cell.transform.position = scenePosition;
            _cells.Add(gamePosition, cell);
        }

        public bool RemoveCell(Vector2Int gamePosition)
        {
            if (!_cells.ContainsKey(gamePosition)) return false;
            var cell = _cells[gamePosition];
            _cells.Remove(gamePosition);
            Destroy(cell.gameObject);
            return true;
        }

        public IGridCell GetCell(Vector2Int gamePosition)
        {
            if (!_cells.ContainsKey(gamePosition))
            {
                throw new InvalidOperationException("Cell does not exist on given game position " + gamePosition);
            }
            return _cells[gamePosition];
        }
    }
}