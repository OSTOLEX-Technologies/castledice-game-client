using System.Collections.Generic;
using castledice_game_logic.GameObjects;

namespace Src.GameplayView.ContentVisuals.VisualsCreation.TreeVisualCreation
{
    public class CachingTreeVisualCreator : ITreeVisualCreator
    {
        private readonly ITreeVisualCreator _treeVisualCreator;
        private readonly Dictionary<Tree, TreeVisual> _cache = new();
        
        public CachingTreeVisualCreator(ITreeVisualCreator treeVisualCreator)
        {
            _treeVisualCreator = treeVisualCreator;
        }
        
        public TreeVisual GetTreeVisual(Tree tree)
        {
            if (_cache.TryGetValue(tree, out var visual))
            {
                return visual;
            }
            var treeVisual = _treeVisualCreator.GetTreeVisual(tree);
            _cache.Add(tree, treeVisual);
            return treeVisual;
        }
    }
}