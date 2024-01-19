using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation
{
    [CreateAssetMenu(fileName = "TreeVisualPrefabsConfig", menuName = "Configs/ContentVisuals/TreeVisualPrefabsConfig")]
    public class TreeVisualPrefabsConfig : ScriptableObject, ITreeVisualPrefabsListProvider
    {
        [SerializeField] private List<TreeVisual> treeVisualPrefabs = new();
        
        public List<TreeVisual> GetTreeVisualPrefabsList()
        {
            return treeVisualPrefabs;
        }
    }
}