using System;
using Riptide;
using UnityEngine;

namespace Src.NetworkingModule.Monobehaviours
{
    public class UnityPeerUpdater : MonoBehaviour, IPeerUpdater
    {
        private Peer _peer;
        private bool _isUpdating;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void SetPeer(Peer peer)
        {
            _peer = peer;
        }

        public void StartUpdating()
        {
            if (_peer is null)
            {
                throw new InvalidOperationException("Peer object is not set.");
            }
            _isUpdating = true;
        }

        public void StopUpdating()
        {
            _isUpdating = false;
        }

        private void Update()
        {
            if (_isUpdating)
            {
                _peer.Update();
            }
        }
    }
}