using System.Collections.Generic;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation
{
    public interface ITreeModelPrefabsListProvider
    {
        List<GameObject> GetTreeModelPrefabsList();
    }
}