using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByColor
{
    [CreateAssetMenu(fileName = "PlayerColorRotationConfig", menuName = "Configs/PlayerRotation/PlayerColorRotationConfig")]
    public class ScriptablePlayerColorRotationConfig : ScriptableObject, IPlayerColorRotationConfig
    {
        [SerializeField] private Vector3 blueRotation;
        [SerializeField] private Vector3 redRotation;
        
        public Vector3 GetRotation(PlayerColor playerColor)
        {
            return playerColor switch
            {
                PlayerColor.Blue => blueRotation,
                PlayerColor.Red => redRotation,
                _ => throw new InvalidOperationException("Unknown player color " + playerColor + ".")
            };
        }
    }
}