using UnityEngine;

namespace Src.PlayerInput
{
    public interface IRaycastHitsProvider
    {
        RaycastHit[] GetHitsForRay(Ray ray);
    }
}