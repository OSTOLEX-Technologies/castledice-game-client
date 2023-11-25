﻿using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation.TreesCreation
{
    [CreateAssetMenu(fileName = "UnityTreeModelConfig", menuName = "Configs/Content/UnityTreeModelConfig")]
    public class UnityTreeModelConfig : ScriptableObject, ITreeModelProvider
    {
        [SerializeField] private GameObject treeModel;
        
        public GameObject GetTreeModel()
        {
            return treeModel;
        }
    }
}