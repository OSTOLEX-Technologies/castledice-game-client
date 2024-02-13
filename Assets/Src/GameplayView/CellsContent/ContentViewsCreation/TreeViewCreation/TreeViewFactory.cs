using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentViews;
using Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation;

namespace Src.GameplayView.CellsContent.ContentViewsCreation.TreeViewCreation
{
    public class TreeViewFactory : ITreeViewFactory
    {
        private readonly ITreeVisualCreator _visualCreator;
        private readonly TreeView _prefab;
        private readonly IInstantiator _instantiator;

        public TreeViewFactory(ITreeVisualCreator visualCreator, TreeView prefab, IInstantiator instantiator)
        {
            _visualCreator = visualCreator;
            _prefab = prefab;
            _instantiator = instantiator;
        }

        public TreeView GetTreeView(Tree tree)
        {
            var view = _instantiator.Instantiate(_prefab);
            var visual = _visualCreator.GetTreeVisual(tree);
            view.Init(tree, visual);
            return view;
        }
    }
}