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
            public SceneType sceneType;
            public string sceneName;
        }

        [SerializeField, InspectorName("Scenes")]
        private List<SceneAssetInfo> sceneNames;

        public string TransitionSceneName => "Transition";

        public string GetSceneName(SceneType sceneType)
        {
            return FindSceneInList(sceneType);
        }

        private string FindSceneInList(SceneType type)
        {
            var selected = sceneNames.Where(asset => asset.sceneType == type).ToList();

            if (!selected.Any())
            {
                throw new InvalidOperationException("No scene assets was defined with such a type: " + type);
            }

            return selected.First().sceneName;
        }
    }
}