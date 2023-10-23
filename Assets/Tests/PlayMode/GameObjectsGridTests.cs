using System;
using System.Collections;
using NUnit.Framework;
using Src.GameplayView.Grid;
using UnityEngine;
using UnityEngine.TestTools;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Tests.PlayMode
{
    public class GameObjectsGridTests
    {
        [Test]
        public void GetParent_ShouldReturnAddedGameObject()
        {
            var position = new Vector2Int(0, 0);
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var gameObjectToAdd = new GameObject();

            grid.AddParent(position, gameObjectToAdd);
            var result = grid.GetParent(position);

            Assert.AreSame(gameObjectToAdd, result);
        }

        [Test]
        public void AddParent_ShouldThrowInvalidOperationException_IfParentForGivenPositionAlreadyExists()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            grid.AddParent((0, 0), new GameObject());

            Assert.Throws<InvalidOperationException>(() => grid.AddParent((0, 0), new GameObject()));
        }

        [Test]
        public void GetParent_ShouldThrowInvalidOperationException_IfParentForGivenPositionDoesNotExist()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();

            Assert.Throws<InvalidOperationException>(() => grid.GetParent((0, 0)));
        }

        [Test]
        public void RemoveParent_ShouldReturnFalse_IfParentForGivenPositionDoesNotExist()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();

            var result = grid.RemoveParent((0, 0));

            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveParent_ShouldReturnTrue_IfParentForGivenPositionExists()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            grid.AddParent((0, 0), new GameObject());

            var result = grid.RemoveParent((0, 0));

            Assert.IsTrue(result);
        }

        [Test]
        //In this test we verify the fact that parent was removed by checking if GetParent method will throw an exception.
        public void RemoveParent_ShouldRemoveParentFromGrid()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            grid.AddParent((0, 0), new GameObject());

            grid.RemoveParent((0, 0));

            Assert.Throws<InvalidOperationException>(() => grid.GetParent((0, 0)));
        }

        [UnityTest]
        public IEnumerator RemoveParent_ShouldDestroyParentGameObject()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);

            grid.RemoveParent((0, 0));
            yield return new WaitForEndOfFrame();
            
            Assert.IsTrue(parent == null);
        }

        [Test]
        public void AddChild_ShouldThrowInvalidOperationException_IfNoParentOnGivenPosition()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var childToAdd = new GameObject();

            Assert.Throws<InvalidOperationException>(() => grid.AddChild((0, 0), childToAdd));
        }

        [Test]
        public void AddChild_ShouldAddGivenObject_AsChildOfParentTransform()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            var childToAdd = new GameObject();
            
            grid.AddChild((0, 0), childToAdd);
            
            Assert.AreSame(parent.transform, childToAdd.transform.parent);
        }
        
        [Test]
        public void AddChild_ShouldSetChildPositionToParentPosition()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            var childToAdd = new GameObject();
            childToAdd.transform.position = new Vector3(2, 3, 4);
            
            grid.AddChild((0, 0), childToAdd);
            
            Assert.AreEqual(parent.transform.position, childToAdd.transform.position);
        }

        [Test]
        public void RemoveChild_ShouldThrowInvalidOperationException_IfNoParentOnGivenPosition()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            
            Assert.Throws<InvalidOperationException>(() => grid.RemoveChild((0, 0), new GameObject()));
        }
        
        [Test]
        public void RemoveChild_ShouldReturnFalse_IfNoChildToRemove()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            
            var result = grid.RemoveChild((0, 0), new GameObject());
            
            Assert.IsFalse(result);
        }
        
        [Test]
        public void RemoveChild_ShouldReturnTrue_IfChildToRemoveExists()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            var child = new GameObject();
            grid.AddChild((0, 0), child);
            
            var result = grid.RemoveChild((0, 0), child);
            
            Assert.IsTrue(result);
        }
        
        [UnityTest]
        public IEnumerator RemoveChild_ShouldRemoveChildFromParentTransform()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            var child = new GameObject();
            grid.AddChild((0, 0), child);
            
            grid.RemoveChild((0, 0), child);
            yield return new WaitForEndOfFrame();
            
            Assert.AreEqual(0, parent.transform.childCount);
        }


        [UnityTest]
        public IEnumerator RemoveChild_ShouldDestroyChildGameObject()
        {
            var gameObject = new GameObject();
            var grid = gameObject.AddComponent<GameObjectsGrid>();
            var parent = new GameObject();
            grid.AddParent((0, 0), parent);
            var child = new GameObject();
            grid.AddChild((0, 0), child);
            
            grid.RemoveChild((0, 0), child);
            yield return new WaitForEndOfFrame();
            
            Assert.IsTrue(child == null);
        }
    }
}