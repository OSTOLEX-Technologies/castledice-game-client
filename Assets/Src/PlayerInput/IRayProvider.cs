using UnityEngine;

namespace Src.PlayerInput
{
    public interface IRayProvider
    {
        Ray ScreenPointToRay(Vector2 screenPoint);
    }
}