using System.Collections.Generic;
using UnityEngine;

namespace Src.PlayerInput
{
    public interface IRaycaster
    {
        List<T> GetRayIntersections<T>(Ray ray);
    }
}