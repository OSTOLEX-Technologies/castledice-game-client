using Src.TextAssetLoading;
using UnityEngine;

namespace Src.Components
{
    public class TextAssetResourceLoader : MonoBehaviour
    {
        [SerializeField, InspectorName("Text Asset Loading Config")] 
        private TextAssetLoadingConfig textAssetLoadingConfig;
        
        public string GetAssetContent(TextAssetType type)
        {
            return textAssetLoadingConfig.GetAsset(type).text;
        }
    }
}