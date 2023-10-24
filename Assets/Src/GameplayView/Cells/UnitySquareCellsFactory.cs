using UnityEngine;

namespace Src.GameplayView.Cells
{
    public class UnitySquareCellsFactory : MonoBehaviour, ISquareCellsFactory
    {
        private UnitySquareCellAssetsConfig _config;

        public void Init(UnitySquareCellAssetsConfig config)
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