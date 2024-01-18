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
            _audio = castleAudio;
            _castle = castle;
            _castle.Hit += OnHit;
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
            Model.SetActive(false);
        }

        private void OnHit(object sender, int damage)
        {
            _audio.PlayHitSound();
        }
    }
}