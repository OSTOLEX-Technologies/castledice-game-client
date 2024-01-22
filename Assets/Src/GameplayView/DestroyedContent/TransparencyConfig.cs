using UnityEngine;

namespace Src.GameplayView.DestroyedContent
{
    [CreateAssetMenu(fileName = "TransparencyConfig", menuName = "Configs/TransparencyConfig", order = 1)]
    public class TransparencyConfig : ScriptableObject, ITransparencyConfig
    {
        [SerializeField] [Range(0, 1)] private float transparency;
        
        public float GetTransparency()
        {
            return transparency;
        }
    }
}