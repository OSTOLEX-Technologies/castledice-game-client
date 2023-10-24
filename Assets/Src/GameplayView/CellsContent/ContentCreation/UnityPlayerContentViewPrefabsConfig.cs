using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    [CreateAssetMenu(fileName = "PlayerContentViewPrefabsConfig", menuName = "Configs/PlayerContentViewPrefabsConfig", order = 1)]
    public class UnityPlayerContentViewPrefabsConfig : ScriptableObject, IPlayerContentViewPrefabsConfig
    {
        [SerializeField] private KnightView redKnightPrefab;
        [SerializeField] private KnightView blueKnightPrefab;
        [SerializeField] private CastleView redCastlePrefab;
        [SerializeField] private CastleView blueCastlePrefab;
        
        public KnightView RedKnightPrefab => redKnightPrefab;
        public KnightView BlueKnightPrefab => blueKnightPrefab;
        public CastleView RedCastlePrefab => redCastlePrefab;
        public CastleView BlueCastlePrefab => blueCastlePrefab;
    }
}