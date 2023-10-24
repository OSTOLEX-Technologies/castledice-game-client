using System.Collections;
using System.Linq;
using NUnit.Framework;
using Src.GameplayView.Grid;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class UnityGridCellTests
    {
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
    }
}