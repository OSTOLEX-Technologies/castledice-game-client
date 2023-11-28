using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    [CreateAssetMenu(fileName = "KnightModelConfig", menuName = "Configs/Content/Knight/KnightModelConfig")]
    public class KnightModelConfig : ScriptableObject, IKnightModelProvider
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