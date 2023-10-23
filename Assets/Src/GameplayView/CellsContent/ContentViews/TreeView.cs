using castledice_game_logic.GameObjects;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class TreeView : ContentView
    {
        private Tree _tree;
        
        public override Content Content => _tree;
        
        public void Init(Tree tree)
        {
            _tree = tree;
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