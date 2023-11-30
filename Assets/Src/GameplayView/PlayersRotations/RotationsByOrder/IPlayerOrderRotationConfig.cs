using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByOrder
{
    public interface IPlayerOrderRotationConfig
    {
        Vector3 GetRotation(int playerOrder);
    }
}