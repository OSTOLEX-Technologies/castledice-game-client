using System;
using System.Collections;
using NUnit.Framework;
using Riptide;
using Src.NetworkingModule.Monobehaviours;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class PeerMock : Client
    {
        public int UpdateCalls = 0;
        
        public override void Update()
        {
            UpdateCalls++;
        }
    }
    
    public class UnityPeerUpdaterTests
    {
        [Test]
        public void StartUpdating_ShouldThrowInvalidOperationException_IfPeerIsNotSet()
        {
            var gameObject = new GameObject();
            var unityPeerUpdater = gameObject.AddComponent<UnityPeerUpdater>();
            
            Assert.Throws<InvalidOperationException>(() => unityPeerUpdater.StartUpdating());
        }

        [UnityTest]
        public IEnumerator Updater_MustUpdatePeerAtLeastOnce_OneSecondAfterStart()
        {
            var peerMock = new PeerMock();
            var gameObject = new GameObject();
            var unityPeerUpdater = gameObject.AddComponent<UnityPeerUpdater>();
            unityPeerUpdater.SetPeer(peerMock);
            
            unityPeerUpdater.StartUpdating();
            yield return new WaitForSeconds(1);
            
            Assert.True(peerMock.UpdateCalls >= 1);
        }

        [UnityTest]
        public IEnumerator Updater_MustNotCallUpdate_IfStopped()
        {
            var peerMock = new PeerMock();
            var gameObject = new GameObject();
            var unityPeerUpdater = gameObject.AddComponent<UnityPeerUpdater>();
            unityPeerUpdater.SetPeer(peerMock);
            
            unityPeerUpdater.StartUpdating();
            unityPeerUpdater.StopUpdating();
            yield return new WaitForSeconds(1);
            
            Assert.True(peerMock.UpdateCalls == 0);
        }
    }
}