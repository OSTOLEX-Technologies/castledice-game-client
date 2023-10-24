using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.Grid
{
    public class UnityGridCell : MonoBehaviour, IGridCell
    {
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
            throw new System.NotImplementedException();
        }

        public bool RemoveChild(GameObject child)
        {
            throw new System.NotImplementedException();
        }
    }
}