using UnityEngine;
using Vector2Int = castledice_game_logic.Math.Vector2Int;

namespace Src.GameplayView.ClickDetection
{
    public class CellClickDetectorsFactory : MonoBehaviour, ICellClickDetectorsFactory
    {
        private ICellClickDetectorsConfig _config;
        
        public void Init(ICellClickDetectorsConfig config)
        {
            _config = config;
        }
        
        public CellClickDetector GetDetector(Vector2Int position)
        {
            var detector = Instantiate(_config.DetectorPrefab);
            detector.Init(position);
            return detector;
        }
    }
}