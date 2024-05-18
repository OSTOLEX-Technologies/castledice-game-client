using Src.PlayerInput;
using UnityEngine;

namespace Src.Components
{
    public class BlockableRaycasterHandler : MonoBehaviour
    {
        private BlockableRaycaster3D _raycaster;

        public bool IsBlocked => _raycaster.Blocked;

        public void Init(BlockableRaycaster3D raycaster)
        {
            _raycaster = raycaster;
        }

        public void BlockRaycast()
        {
            _raycaster.Block();
        }

        public void UnblockRaycast()
        {
            _raycaster.Unblock();
        }
    }
}