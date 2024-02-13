using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using Src.GameplayView.ContentVisuals;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class KnightView : ContentView
    {
        private KnightAudio _audio;
        private KnightVisual _visual;
        private Knight _knight;
        
        public override Content Content => _knight;

        public void Init(Knight knight, KnightVisual visual, KnightAudio audio)
        {
            _knight = knight;
            _visual = visual;
            _audio = audio;
            _audio.DestroySoundPlayed += OnDestroySoundPlayed;
            SetAsChildAndCenter(visual.gameObject);
            SetAsChildAndCenter(audio.gameObject);
        }
        
        public override void StartView()
        {
            _audio.PlayPlaceSound();
        }

        public override void UpdateView()
        {

        }

        public override void DestroyView()
        {
            _audio.PlayDestroySound();
            _visual.gameObject.SetActive(false);
        }
        
        private void OnDestroySoundPlayed()
        {
            _audio.DestroySoundPlayed -= OnDestroySoundPlayed;
            Destroy(gameObject);
        }
    }
}