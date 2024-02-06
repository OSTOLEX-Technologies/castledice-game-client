using System.Collections;
using System.Linq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using UnityEngine;
using UnityEngine.TestTools;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.EditMode.GameplayViewTests.GridTests
{
    public class UnityGridCellTests
    {
        public static Vector2Int[] Positions = { (0, 0), (1, 0), (2, 0), (0, 1), (1, 1) };
        
        [Test]
        public void PositionProperty_ShouldReturnPosition_GivenInInit([ValueSource(nameof(Positions))]Vector2Int position)
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            
            cell.Init(position);
            
            Assert.AreEqual(position, cell.Position);
        }
        
        [Test]
        public void Contains_ShouldReturnTrueForGameObject_IfAddChildWasCalled()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();

            cell.AddChild(child);

            Assert.True(cell.Contains(child));
        }

        [Test]
        public void AddChild_ShouldSetCellGameObject_AsParentOfChildGameObject()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();

            cell.AddChild(child);

            Assert.AreSame(cell.gameObject, child.transform.parent.gameObject);
        }

        [UnityTest]
        public IEnumerator AddChild_ShouldSetChildWorldPosition_ToCellPosition()
        {
            var cellPosition = new Vector3(1, 2, 3);
            var gameObject = new GameObject();
            gameObject.transform.position = cellPosition;
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();

            cell.AddChild(child);
            yield return null;

            Assert.AreEqual(cellPosition, child.transform.position);
        }

        [Test]
        public void RemoveChild_ShouldReturnTrue_IfChildWasAddedBefore()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();
            cell.AddChild(child);

            var result = cell.RemoveChild(child);

            Assert.True(result);
        }

        [Test]
        public void RemoveChild_ShouldReturnFalse_IfChildWasNotAddedBefore()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();

            var result = cell.RemoveChild(child);

            Assert.False(result);
        }

        [Test]
        public void Contains_ShouldReturnFalse_IfChildWasRemoved()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();
            cell.AddChild(child);

            cell.RemoveChild(child);

            Assert.False(cell.Contains(child));        
        }

        [UnityTest]
        public IEnumerator RemoveChild_ShouldDestroyChildGameObject()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();
            cell.AddChild(child);
            
            cell.RemoveChild(child);
            yield return null;
            
            Assert.True(child == null);
        }

        [UnityTest]
        public IEnumerator RemoveChild_ShouldNotDestroyChildGameObject_IfItWasNotAddedBefore()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child = new GameObject();
            
            cell.RemoveChild(child);
            yield return null;
            
            Assert.True(child is not null);
        }
        
        [Test]
        public void GetEnumerator_ShouldReturnAllChildren()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();
            var child1 = new GameObject();
            var child2 = new GameObject();
            cell.AddChild(child1);
            cell.AddChild(child2);

            var result = cell.ToList();

            Assert.AreEqual(2, result.Count);
            Assert.Contains(child1, result);
            Assert.Contains(child2, result);
        }
        
        [Test]
        public void GetEnumerator_ShouldReturnEmptyList_IfNoChildren()
        {
            var gameObject = new GameObject();
            var cell = gameObject.AddComponent<UnityGridCell>();

            var result = cell.ToList();

            Assert.AreEqual(0, result.Count);
        }
    }
}