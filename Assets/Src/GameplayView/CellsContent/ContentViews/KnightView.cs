using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class KnightView : ContentView
    {
        private IKnightAudio _audio;
        private Knight _knight;
        
        public override Content Content => _knight;

        public void Init(Knight knight, GameObject model, Vector3 rotation)
        {
            Init(model);
            _knight = knight;
            Model.transform.localEulerAngles = rotation;
            _audio = gameObject.GetComponent<IKnightAudio>();
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
        }
    }
}