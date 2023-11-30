using System;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations.RotationsByOrder
{
    [CreateAssetMenu(fileName = "PlayerOrderRotationConfig", menuName = "Configs/PlayerRotation/PlayerOrderRotationConfig")]

    public class ScriptablePlayerOrderRotationConfig : ScriptableObject, IPlayerOrderRotationConfig
    {
        [SerializeField] private Vector3 firstPlayerRotation;
        [SerializeField] private Vector3 secondPlayerRotation;
        
        public Vector3 GetRotation(int playerOrder)
        {
            return playerOrder switch
            {
                1 => firstPlayerRotation,
                2 => secondPlayerRotation,
                _ => throw new ArgumentException("No rotation for player with order number " + playerOrder + ".")
            };
        }
    }
}