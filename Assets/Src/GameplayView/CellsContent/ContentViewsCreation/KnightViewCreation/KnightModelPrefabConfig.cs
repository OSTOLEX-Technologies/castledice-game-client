using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.KnightViewCreation
{
    [CreateAssetMenu(fileName = "KnightModelPrefabConfig", menuName = "Configs/Content/Knight/KnightModelPrefabConfig")]

    public class KnightModelPrefabConfig : ScriptableObject, IKnightModelPrefabProvider
    {
        [SerializeField] private GameObject redKnightModelPrefab;
        [SerializeField] private GameObject blueKnightModelPrefab;
        
        public GameObject GetKnightModelPrefab(PlayerColor color)
        {
            return color switch
            {
                PlayerColor.Red => redKnightModelPrefab,
                PlayerColor.Blue => blueKnightModelPrefab,
                _ => throw new ArgumentException("No prefab for player color: " + color)
            };
        }
    }
}