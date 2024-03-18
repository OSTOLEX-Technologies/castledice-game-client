using System.Collections.Generic;
using UnityEngine;

namespace Src.PlayerInput
{
    public class BlockableRaycaster3D : IRaycaster
    {
        private readonly IRaycaster _raycaster;
        private bool _blocked;
        
        public BlockableRaycaster3D(IRaycaster raycaster)
        {
            _raycaster = raycaster;
        }
        
        public void Block()
        {
            _blocked = true;
        }
        
        public void Unblock()
        {
            _blocked = false;
        }

        public List<T> GetRayIntersections<T>(Ray ray)
        {
            return _blocked ? new List<T>() : _raycaster.GetRayIntersections<T>(ray);
        }
    }
}