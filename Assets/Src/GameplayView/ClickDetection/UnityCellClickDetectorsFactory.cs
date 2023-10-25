using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.ClickDetection
{
    public class UnityCellClickDetectorsFactory : MonoBehaviour, IUnityCellClickDetectorsFactory
    {
        private IUnityCellClickDetectorsConfig _config;
        
        public void Init(IUnityCellClickDetectorsConfig config)
        {
            _config = config;
        }
        
        public UnityCellClickDetector GetDetector(Vector2Int position)
        {
            var detector = Instantiate(_config.DetectorPrefab);
            detector.Init(position);
            return detector;
        }
    }
}