using System.Collections;
using Moq;
using Src.GameplayView.Updatables;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditMode.GameplayViewTests.UpdatablesTests
{
    public class FixedUpdaterBehaviourTests
    {
        [UnityTest]
        public IEnumerator FixedUpdate_ShouldCallUpdate_OnGivenUpdater()
        {
            var updaterMock = new Mock<IUpdater>();
            var gameObject = new GameObject();
            var updaterBehaviour = gameObject.AddComponent<UpdaterBehaviour>();
            updaterBehaviour.Init(updaterMock.Object);
            
            yield return null;
            
            updaterMock.Verify(u => u.Update(), Times.Once);
        }
    }
}