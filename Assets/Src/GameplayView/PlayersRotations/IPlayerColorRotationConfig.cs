using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations
{
    public interface IPlayerColorRotationConfig
    {
        public Vector3 GetRotation(PlayerColor playerColor);
    }
}