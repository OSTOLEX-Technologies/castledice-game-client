using UnityEngine;

namespace Src.General.TextAssetLoading
{
    public interface ITextAssetLoadingConfig
    {
        public TextAsset GetAsset(TextAssetType type);
    }
}