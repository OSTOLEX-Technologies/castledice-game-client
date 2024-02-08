using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Src.TextAssetLoading
{
    [CreateAssetMenu(fileName = "TextAssetLoadingConfig", menuName = "Configs/w/TextAssetLoading")]
    
    public class TextAssetLoadingConfig : ScriptableObject, ITextAssetLoadingConfig
    {
        [Serializable]
        private struct TextAssetInfo
        {
            public TextAssetType assetType;
            public TextAsset textAsset;
        }

        [SerializeField, InspectorName("Assets")]
        private List<TextAssetInfo> textAssets;

        public TextAsset GetAsset(TextAssetType type)
        {
            return FindTextAssetInList(type);
        }

        private TextAsset FindTextAssetInList(TextAssetType type)
        {
            var selected = textAssets.Where(asset => asset.assetType == type).ToList();

            if (!selected.Any())
            {
                throw new InvalidOperationException("No text assets was defined with such a type: " + type);
            }

            return selected.First().textAsset;
        }
    }
}