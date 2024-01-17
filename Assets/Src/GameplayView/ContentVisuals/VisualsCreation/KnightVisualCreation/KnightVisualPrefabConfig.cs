using UnityEngine;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.KnightVisualCreation
{
    [CreateAssetMenu(fileName = "KnightVisualPrefabConfig", menuName = "Configs/ContentVisuals/KnightVisualPrefabConfig")]
    public class KnightVisualPrefabConfig : ScriptableObject, IKnightVisualPrefabProvider
    {
        [SerializeField] private KnightVisual knightVisualPrefab;
        
        public KnightVisual GetKnightVisualPrefab()
        {
            return knightVisualPrefab;
        }
    }
}