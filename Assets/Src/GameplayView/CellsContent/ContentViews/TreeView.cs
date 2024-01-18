using castledice_game_logic.GameObjects;
using UnityEngine;
using Tree = castledice_game_logic.GameObjects.Tree;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class TreeView : ContentView
    {
        private Tree _tree;
        private GameObject _model;
        
        public override Content Content => _tree;
        
        public void Init(Tree tree, GameObject model)
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