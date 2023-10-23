using castledice_game_logic.GameObjects;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class CastleView : ContentView
    {
        private CastleGO _castle;

        public override Content Content => _castle;

        public void Init(CastleGO castle)
        {
            _castle = castle;
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