using System.Collections;
using System.Collections.Generic;
using Src.GameplayView.Grid;
using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.Utils.Mocks
{
    public class GridForTests : IGrid
    {
        public List<GridCellForTests> Cells { get; set; } = new();
            
        public IEnumerator<IGridCell> GetEnumerator()
        {
            return Cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void AddCell(Vector2Int gamePosition, Vector3 scenePosition)
        {
            Cells.Add(new GridCellForTests(gamePosition));
        }

        public bool RemoveCell(Vector2Int gamePosition)
        {
            throw new System.NotImplementedException();
        }

        public IGridCell GetCell(Vector2Int gamePosition)
        {
            return Cells.Find(c => c.Position == gamePosition);
        }
    }
}