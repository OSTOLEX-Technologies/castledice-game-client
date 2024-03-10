using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.Grid
{
    public class GameObjectsGridCell : MonoBehaviour, IGridCell
    {
        public Vector2Int Position { get; private set; }

        private readonly List<GameObject> _children = new();
        
        public void Init(Vector2Int position)
        {
            Position = position;
        }
        
        public IEnumerator<GameObject> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void AddChild(GameObject child)
        {
            _children.Add(child);
            var parentTransform = transform;
            child.transform.parent = parentTransform;
            child.transform.position = parentTransform.position;
        }

        public bool RemoveChild(GameObject child)
        {
            if (!_children.Contains(child)) return false;
            _children.Remove(child);
            Destroy(child);
            return true;
        }
    }
}