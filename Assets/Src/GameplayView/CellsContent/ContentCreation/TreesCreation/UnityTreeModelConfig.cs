using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation.TreesCreation
{
    public class UnityTreeModelConfig : ScriptableObject, ITreeModelProvider
    {
        [SerializeField] private GameObject treeModel;
        
        public GameObject GetTreeModel()
        {
            return treeModel;
        }
    }
}