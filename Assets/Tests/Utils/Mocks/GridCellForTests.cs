using System.Collections;
using System.Collections.Generic;
using Src.GameplayView.Grid;
using Src.Tutorial.HintPointer;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.Utils.Mocks
{
    public class GridCellForTests : IGridCell
    {
        public Vector2Int Position { get; set; }
        public IEnumerable<GameObject> Children => ChildrenList;
        public List<GameObject> ChildrenList { get; set; } = new();
            
        public GridCellForTests(Vector2Int position)
        {
            Position = position;
        }
        public IEnumerator<GameObject> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddChild(GameObject child)
        {
            ChildrenList.Add(child);                
        }

        public bool RemoveChild(GameObject child)
        {
            return ChildrenList.Remove(child);
        }
    }
}