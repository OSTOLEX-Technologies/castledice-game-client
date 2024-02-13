using UnityEngine;

namespace Src.GameplayView.Highlights
{
    [CreateAssetMenu(fileName = "UnderlinePrefabConfig", menuName = "Configs/UnderlinePrefabConfig")]
    public class ColoredHighlightPrefabConfig : ScriptableObject, IColoredHighlightPrefabProvider
    {
        [SerializeField] private ColoredHighlight coloredHighlightPrefab;
        
        public ColoredHighlight GetHighlightPrefab()
        {
            return coloredHighlightPrefab;
        }
    }

}