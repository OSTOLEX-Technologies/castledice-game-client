using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class SquareCellsFactory : MonoBehaviour, ISquareCellsFactory
    {
        private SquareCellAssetsConfig _config;

        public void Init(SquareCellAssetsConfig config)
        {
            _config = config;
        }
        
        public GameObject GetSquareCell(int assetId)
        {
            var prefab = _config.GetAssetPrefab(assetId);
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}