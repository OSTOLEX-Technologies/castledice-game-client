using castledice_game_logic.GameObjects;
using Src.GameplayView.CellsContent.ContentAudio.TreeAudio;
using UnityEngine;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentViews
{
    public class CastleView : ContentView
    {
        private CastleGO _castle;
        private ICastleAudio _audio;

        public override Content Content => _castle;

        public void Init(CastleGO castle, GameObject model)
        {
            Init(model);
            _audio = gameObject.GetComponent<ICastleAudio>();
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
            _audio.PlayDestroySound();
        }
    }
}