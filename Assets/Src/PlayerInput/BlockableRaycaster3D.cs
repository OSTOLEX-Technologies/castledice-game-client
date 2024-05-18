using System.Collections.Generic;
using UnityEngine;

namespace Src.PlayerInput
{
    public class BlockableRaycaster3D : IRaycaster
    {
        private readonly IRaycaster _raycaster;
        public bool Blocked { get; private set; }

        public BlockableRaycaster3D(IRaycaster raycaster)
        {
            _raycaster = raycaster;
        }
        
        public void Block()
        {
            Blocked = true;
        }
        
        public void Unblock()
        {
            Blocked = false;
        }

        public List<T> GetRayIntersections<T>(Ray ray)
        {
            return Blocked ? new List<T>() : _raycaster.GetRayIntersections<T>(ray);
        }
    }
}