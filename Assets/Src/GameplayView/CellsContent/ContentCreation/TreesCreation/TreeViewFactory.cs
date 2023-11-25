using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;

namespace Src.GameplayView.CellsContent.ContentCreation.TreesCreation
{
    public class TreeViewFactory : ITreeViewFactory
    {
        private readonly ITreeModelProvider _modelProvider;
        private readonly TreeView _prefab;
        private readonly IInstantiator _instantiator;

        public TreeViewFactory(ITreeModelProvider modelProvider, TreeView prefab, IInstantiator instantiator)
        {
            _modelProvider = modelProvider;
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public TreeView GetTreeView(Tree tree)
        {
            var model = _modelProvider.GetTreeModel();
            var view = _instantiator.Instantiate(_prefab);
            view.Init(tree, model);
            return view;
        }
    }
}