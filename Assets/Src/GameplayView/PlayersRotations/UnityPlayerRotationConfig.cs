using System;
using System.Collections.Generic;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.PlayersRotations
{
    [CreateAssetMenu(fileName = "UnityPlayerRotationConfig", menuName = "Configs/UnityPlayerRotationConfig")]
    public class UnityPlayerRotationConfig : ScriptableObject, IPlayerRotationConfig
    {
        [SerializeField] private List<PlayerColorToRotation> colorsToRotations;
        
        public Dictionary<PlayerColor, Vector3> GetRotations()
        {
            var dictionary = new Dictionary<PlayerColor, Vector3>();
            foreach (var playerNumberToRotation in colorsToRotations)
            {
                dictionary.Add(playerNumberToRotation.playerColor, playerNumberToRotation.rotation);
            }
            return dictionary;
        }
    }

    [Serializable]
    public class PlayerColorToRotation
    {
        public PlayerColor playerColor;
        public Vector3 rotation;
    }
}