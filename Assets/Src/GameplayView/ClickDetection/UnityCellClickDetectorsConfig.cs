using UnityEngine;

namespace Src.GameplayView.ClickDetection
{
    [CreateAssetMenu(fileName = "UnityCellClickDetectorsConfig", menuName = "Configs/UnityCellClickDetectorsConfig", order = 2)]
    public class UnityCellClickDetectorsConfig : ScriptableObject, IUnityCellClickDetectorsConfig
    {
        [SerializeField] private UnityCellClickDetector detectorPrefab;
        public UnityCellClickDetector DetectorPrefab => detectorPrefab;
    }
}