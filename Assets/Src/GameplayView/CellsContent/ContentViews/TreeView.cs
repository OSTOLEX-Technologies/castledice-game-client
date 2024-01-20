using castledice_game_logic.GameObjects;
using Src.GameplayView.ContentVisuals;
using Tree = castledice_game_logic.GameObjects.Tree;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class TreeView : ContentView
    {
        private Tree _tree;
        private TreeVisual _visual;
        
        public override Content Content => _tree;
        
        public void Init(Tree tree, TreeVisual visual)
        {
            _tree = tree;
            _visual = visual;
            SetAsChildAndCenter(_visual.gameObject);
        }
        
        public override void StartView()
        {
            
        }

        public override void UpdateView()
        {

        }

        public override void DestroyView()
        {

        }
    }
}