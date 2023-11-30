using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.KnightAudio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class KnightView : ContentView
    {
        private KnightAudio _audio;
        private Knight _knight;
        
        public override Content Content => _knight;

        public void Init(Knight knight, GameObject model, Vector3 rotation, KnightAudio knightAudio)
        {
            Init(model);
            _knight = knight;
            Model.transform.localEulerAngles = rotation;
            _audio = knightAudio;
            var audioTransform = knightAudio.transform;
            audioTransform.SetParent(transform);
            audioTransform.localPosition = Vector3.zero;
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
            Model.SetActive(false);
        }
    }
}