using Src.Constants;
using UnityEngine;

namespace Src.PlayerInput
{
    public class RaycastHitProvider : IRaycastHitsProvider
    {
        public RaycastHit[] GetHitsForRay(Ray ray)
        {
            var hits = new RaycastHit[RaycastConstants.MaxRaycastHits];
            Physics.RaycastNonAlloc(ray, hits, RaycastConstants.MaxRaycastDistance);
            return hits;
        }
    }
}