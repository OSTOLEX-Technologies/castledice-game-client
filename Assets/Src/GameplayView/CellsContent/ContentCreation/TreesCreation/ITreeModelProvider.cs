using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation.TreesCreation
{
    /// <summary>
    /// By model we mean some 3D, 2D or any other visual representation of tree.
    /// </summary>
    public interface ITreeModelProvider
    {
        GameObject GetTreeModel();
    }
}