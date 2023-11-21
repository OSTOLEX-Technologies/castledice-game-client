using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.PlayerInput
{
    public class Raycaster3D : IRaycaster
    {
        private readonly IRaycastHitsProvider _hitsProvider;

        public Raycaster3D(IRaycastHitsProvider hitsProvider)
        {
            _hitsProvider = hitsProvider;
        }

        public List<T> GetRayIntersections<T>(Ray ray)
        {
            var hits = _hitsProvider.GetHitsForRay(ray);

            return hits.Select(hit => hit.collider.GetComponent<T>()).Where(component => component != null).ToList();
        }
    }
}