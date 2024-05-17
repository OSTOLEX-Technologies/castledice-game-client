using castledice_game_logic;
using UnityEngine;

namespace Src.Tutorial.ActionPointsGiving
{
    public class TutorialActionPointsChanger : MonoBehaviour
    {
        private Game _game;

        public void Init(Game game)
        {
            _game = game;
        }
        
        public void GiveActionPointsToPlayer(int playerId, int actionPointsAmount)
        {
            _game.GiveActionPointsToPlayer(playerId, actionPointsAmount);
        }
        
        public void TakeActionPointsFromPlayer(int playerId, int actionPointsAmount)
        {
            _game.TakeActionPointsFromPlayer(playerId, actionPointsAmount);
        }
    }
}