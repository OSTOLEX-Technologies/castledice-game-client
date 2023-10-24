using Src.GameplayView.CellsContent.ContentViews;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    [CreateAssetMenu(fileName = "CommonContentViewPrefabConfig", menuName = "Configs/CommonContentViewPrefabConfig", order = 1)]
    public class UnityCommonContentViewPrefabConfig : ScriptableObject, ICommonContentViewPrefabProvider
    {
        [SerializeField] private TreeView treePrefab;

        public TreeView TreePrefab => treePrefab;
    }
}