using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.Grid
{
    public interface IGridCell : IEnumerable<GameObject>
    {
        void AddChild(GameObject child);
        bool RemoveChild(GameObject child);
    }
}