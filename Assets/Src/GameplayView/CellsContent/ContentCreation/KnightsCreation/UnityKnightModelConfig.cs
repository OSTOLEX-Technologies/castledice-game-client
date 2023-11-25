using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation.KnightsCreation
{
    public class UnityKnightModelConfig : ScriptableObject, IKnightModelProvider
    {
        [SerializeField] private GameObject redKnightModel;
        [SerializeField] private GameObject blueKnightModel;
        
        public GameObject GetKnightModel(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    return redKnightModel;
                case PlayerColor.Blue:
                    return blueKnightModel;
                default:
                    throw new ArgumentException("Unknown player color: " + color);
            }
        }
    }
}