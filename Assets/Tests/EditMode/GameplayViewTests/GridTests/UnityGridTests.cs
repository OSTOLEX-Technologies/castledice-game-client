using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using UnityEngine;
using UnityEngine.TestTools;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.PlayMode
{
    public class UnityGridTests
    {
        public static Vector3[] ScenePositions = { new (1, 2, 3), new (4, 5, 6), new (0.2f, 3, 2.5f)};
        public static Vector2Int[] GamePositions = { (0, 0), (1, 0), (2, 0), (0, 1), (1, 1) };
        
        [Test]
        public void AddCell_ShouldCreateUnityGridCell_WithGivenGamePosition([ValueSource(nameof(GamePositions))]Vector2Int gamePosition)
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            
            grid.AddCell(gamePosition, new Vector3());
            var cell = grid.GetCell(gamePosition);
            
            Assert.AreEqual(gamePosition, cell.Position);
        }
        
        [UnityTest]
        public IEnumerator AddCell_ShouldCreateUnityGridCell_OnGivenScenePosition([ValueSource(nameof(ScenePositions))]Vector3 scenePosition)
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            grid.AddCell(gamePosition, scenePosition);
            yield return null;
            var gameObject = (grid.First() as UnityGridCell).gameObject;
            
            Assert.AreEqual(scenePosition, gameObject.transform.position);
        }
        
        [Test]
        public void AddCell_ShouldThrowInvalidOperationException_IfCellAlreadyExistsOnGivenGamePosition()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            grid.AddCell(gamePosition, new Vector3());
            
            Assert.Throws<InvalidOperationException>(() => grid.AddCell(gamePosition, new Vector3()));
        }

        [Test]
        public void GetCell_ShouldReturnIGridCell_IfItWasPreviouslyAdded()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            grid.AddCell(gamePosition, new Vector3());
            
            Assert.IsInstanceOf<IGridCell>(grid.GetCell(gamePosition));
        }

        [Test]
        public void GetCell_ShouldThrowInvalidOperationException_IfCellWasNotAddedOnGivenGamePosition()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            Assert.Throws<InvalidOperationException>(() => grid.GetCell(gamePosition));
        }
        
        [Test]
        public void RemoveCell_ShouldReturnTrue_IfCellWasPreviouslyAdded()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            grid.AddCell(gamePosition, new Vector3());
            
            Assert.True(grid.RemoveCell(gamePosition));
        }
        
        [Test]
        public void RemoveCell_ShouldReturnFalse_IfCellWasNotPreviouslyAdded()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            Assert.False(grid.RemoveCell(gamePosition));
        }
        
        [Test]
        public void Any_ShouldReturnFalse_IfAllCellsWereRemoved()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            
            grid.AddCell(gamePosition, new Vector3());
            grid.RemoveCell(gamePosition);
            
            Assert.False(grid.Any());
        }

        [UnityTest]
        public IEnumerator RemoveCell_ShouldDestroyCellGameObject()
        {
            var grid = new GameObject().AddComponent<UnityGrid>();
            var gamePosition = new Vector2Int(1, 1);
            var scenePosition = Vector3.zero;
            
            grid.AddCell(gamePosition, scenePosition);
            var cellGameObject = (grid.GetCell(gamePosition) as UnityGridCell).gameObject;
            grid.RemoveCell(gamePosition);
            yield return null;

            Assert.True(cellGameObject == null);
        }
    }
}