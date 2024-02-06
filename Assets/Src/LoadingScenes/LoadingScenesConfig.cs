using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Src.LoadingScenes
{
    [CreateAssetMenu(fileName = "LoadingScenesConfig", menuName = "Configs/LoadingScenes/LoadingScenesConfig")]

    public class LoadingScenesConfig : ScriptableObject, ILoadingScenesConfig
    {
        [Serializable]
        private struct SceneAssetInfo
        {
            public ESceneType sceneType;
            public SceneAsset sceneAsset;
        }

        [SerializeField, InspectorName("Transition Scene")]
        private SceneAsset transitionSceneAsset;

        [SerializeField, InspectorName("Scenes")]
        private List<SceneAssetInfo> scenesAssets;

        public string TransitionSceneName => transitionSceneAsset.name;

        public string GetSceneName(ESceneType sceneType)
        {
            return FindSceneInList(sceneType).name;
        }

        private SceneAsset FindSceneInList(ESceneType type)
        {
            var selected = scenesAssets.Where(asset => asset.sceneType == type).ToList();

            if (!selected.Any())
            {
                throw new InvalidOperationException("No scene assets was defined with such a type: " + type);
            }

            return selected.First().sceneAsset;
        }
    }
}