using castledice_game_logic;
using castledice_game_logic.GameObjects;
using UnityEngine;

namespace Src.GameplayView.CellsContent.ContentViews
{
    //TODO: Refactor this class
    public class KnightView : ContentView
    {
        [SerializeField] private int rotationIfFirst;
        [SerializeField] private int rotationIfSecond;
        private Knight _knight;
        
        public override Content Content => _knight;

        public void Init(Knight knight)
        {
            _knight = knight;
        }
        
        public override void StartView()
        {
            var game = Singleton<Game>.Instance;
            var playerIndex = game.GetAllPlayersIds().IndexOf(_knight.GetOwner().Id);
            var rotation = playerIndex == 0 ? rotationIfFirst : rotationIfSecond;
            transform.localEulerAngles = new Vector3(0, rotation, 0);
        }

        public override void UpdateView()
        {

        }

        public override void DestroyView()
        {
            Destroy(gameObject);
        }
    }
}