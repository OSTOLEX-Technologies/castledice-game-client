using System;
using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public class GameObjectsGrid : MonoBehaviour, IGameObjectsGrid
    {
        private Dictionary<Vector2Int, GameObject> _parents = new();
        
        public void AddParent(Vector2Int position, GameObject parent)
        {
            if (_parents.ContainsKey(position))
            {
                throw new InvalidOperationException("Parent for given position already exists. Position: " + position);
            }
            _parents.Add(position, parent);
        }

        public GameObject GetParent(Vector2Int position)
        {
            if (!_parents.ContainsKey(position))
            {
                throw new InvalidOperationException("Parent for given position does not exists. Position: " + position);
            }
            return _parents[position];
        }

        public bool RemoveParent(Vector2Int position)
        {
            if (!_parents.ContainsKey(position)) return false;
            var parent = _parents[position];
            _parents.Remove(position);
            Destroy(parent);
            return true;
        }

        public void AddChild(Vector2Int position, GameObject child)
        {
            if (!_parents.ContainsKey(position))
            {
                throw new InvalidOperationException("Parent for given position does not exists. Position: " + position);
            }
            child.transform.SetParent(_parents[position].transform);
            child.transform.localPosition = Vector3.zero;
        }

        public bool RemoveChild(Vector2Int position, GameObject child)
        {
            if (!_parents.ContainsKey(position))
            {
                throw new InvalidOperationException("Parent for given position does not exists. Position: " + position);
            }
            if (child.transform.parent != _parents[position].transform) return false;
            Destroy(child);
            return true;
        }
    }
}