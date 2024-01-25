using UnityEngine;

namespace Src.GameplayView.UnitsUnderlines
{
    [CreateAssetMenu(fileName = "UnderlinePrefabConfig", menuName = "Configs/UnderlinePrefabConfig")]
    public class UnderlinePrefabConfig : ScriptableObject, IUnderlinePrefabProvider
    {
        [SerializeField] private Underline underlinePrefab;
        
        public Underline GetUnderlinePrefab()
        {
            return underlinePrefab;
        }
    }

}