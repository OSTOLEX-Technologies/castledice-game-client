using UnityEngine;

namespace Src.General
{
    [CreateAssetMenu(menuName = "Configs/FloatValueConfig", fileName = "FloatValueConfig", order = 0)]
    public class FloatValueConfig : ScriptableObject
    {
        [SerializeField] private float value;
        
        public float Value => value;
    }
}