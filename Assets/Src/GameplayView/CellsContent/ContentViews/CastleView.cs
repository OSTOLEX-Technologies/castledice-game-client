using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using Src.GameplayView.ContentVisuals;
using CastleEntity = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class CastleView : ContentView
    {
        private CastleEntity _castle;
        private CastleAudio _audio;
        private CastleVisual _visual;
        
        public override Content Content => _castle;

        public void Init(CastleEntity castle, CastleVisual visual, CastleAudio audio)
        {
            _audio = audio;
            _audio.DestroySoundPlayed += OnDestroySoundPlayed;
            _castle = castle;
            _castle.Hit += OnHit;
            _visual = visual;
            SetAsChildAndCenter(visual.gameObject);
            SetAsChildAndCenter(audio.gameObject);
        }

        public override void StartView()
        {

        }

        public override void UpdateView()
        {

        }

        public override void DestroyView()
        {
            _audio.PlayDestroySound();
            _visual.gameObject.SetActive(false);
        }

        private void OnHit(object sender, int damage)
        {
            _audio.PlayHitSound();
        }
        
        private void OnDestroySoundPlayed()
        {
            _audio.DestroySoundPlayed -= OnDestroySoundPlayed;
            Destroy(gameObject);
        }
    }
}