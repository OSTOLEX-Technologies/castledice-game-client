using castledice_game_logic.GameObjects;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class KnightView : ContentView
    {
        private Knight _knight;
        
        public override Content Content => _knight;

        public void Init(Knight knight)
        {
            _knight = knight;
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