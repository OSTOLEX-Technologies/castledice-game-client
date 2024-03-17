using castledice_game_logic;
using Src.GameplayPresenter.GameCreation;

namespace Src.Tutorial
{
    public class TutorialGameCreator
    {
        private readonly IGameCreator _gameCreator;
        private readonly ITutorialGameStartDataProvider _gameStartDataProvider;
        private readonly int _playerId;
        private readonly int _enemyId;
        
        public TutorialGameCreator(IGameCreator gameCreator, ITutorialGameStartDataProvider gameStartDataProvider, int playerId, int enemyId)
        {
            _gameCreator = gameCreator;
            _gameStartDataProvider = gameStartDataProvider;
            _playerId = playerId;
            _enemyId = enemyId;
        }
        
        public Game CreateGame()
        {
            var gameStartData = _gameStartDataProvider.GetGameStartData(_playerId, _enemyId);
            return _gameCreator.CreateGame(gameStartData);
        }
    }
}