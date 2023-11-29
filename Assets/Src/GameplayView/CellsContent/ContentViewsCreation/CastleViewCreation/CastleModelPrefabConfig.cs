using System;
using Src.GameplayView.PlayersColors;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.CastleViewCreation
{
    [CreateAssetMenu(fileName = "CastleModelPrefabConfig", menuName = "Configs/Content/Castle/CastleModelPrefabConfig")]
    public class CastleModelPrefabConfig : ScriptableObject, ICastleModelPrefabProvider
    {
        [SerializeField] private GameObject redCastleModelPrefab;
        [SerializeField] private GameObject blueCastleModelPrefab;
        
        public GameObject GetCastleModelPrefab(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.Red:
                    return redCastleModelPrefab;
                case PlayerColor.Blue:
                    return blueCastleModelPrefab;
                default:
                    throw new ArgumentException("No prefab for color " + color);
            }
        }
    }
}