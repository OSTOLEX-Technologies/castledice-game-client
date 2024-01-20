using UnityEngine;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.CastleVisualCreation
{
    [CreateAssetMenu(fileName = "CastleVisualPrefabsConfig", menuName = "Configs/ContentVisuals/CastleVisualPrefabsConfig")]
    public class CastleVisualPrefabConfig : ScriptableObject, ICastleVisualPrefabProvider
    {
        [SerializeField] private CastleVisual castleVisualPrefab;
        
        public CastleVisual GetCastleVisualPrefab()
        {
            return castleVisualPrefab;
        }
    }
}