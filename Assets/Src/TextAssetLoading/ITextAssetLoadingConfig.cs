using UnityEngine;

namespace Src.TextAssetLoading
{
    public interface ITextAssetLoadingConfig
    {
        public TextAsset GetAsset(TextAssetType type);
    }
}