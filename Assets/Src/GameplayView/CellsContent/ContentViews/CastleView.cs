using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.CastleAudio;
using UnityEngine;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class CastleView : ContentView
    {
        private CastleGO _castle;
        private CastleAudio _audio;

        public override Content Content => _castle;

        public void Init(CastleGO castle, GameObject model, CastleAudio castleAudio)
        {
            Init(model);
            _audio = castleAudio;
            _castle = castle;
            var audioTransform = _audio.transform;
            audioTransform.SetParent(transform);
            audioTransform.localPosition = Vector3.zero;
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
        }
    }
}