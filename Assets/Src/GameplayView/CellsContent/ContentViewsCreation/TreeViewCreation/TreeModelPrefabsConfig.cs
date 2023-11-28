using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation
{
    [CreateAssetMenu(fileName = "TreeModelPrefabsConfig", menuName = "Configs/Content/Trees/TreeModelPrefabsConfig")]
    public class TreeModelPrefabsConfig : ScriptableObject, ITreeModelPrefabsListProvider
    {
        [SerializeField] private List<GameObject> treeModelPrefabsList;
        
        public List<GameObject> GetTreeModelPrefabsList()
        {
            return treeModelPrefabsList;
        }
    }
}