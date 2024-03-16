using UnityEngine;

namespace Src.GameplayView.ClickDetection
{
    [CreateAssetMenu(fileName = "CellClickDetectorsConfig", menuName = "Configs/CellClickDetectorsConfig", order = 2)]
    public class CellClickDetectorsConfig : ScriptableObject, ICellClickDetectorsConfig
    {
        [SerializeField] private CellClickDetector detectorPrefab;
        public CellClickDetector DetectorPrefab => detectorPrefab;
    }
}