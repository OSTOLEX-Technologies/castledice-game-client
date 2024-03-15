using castledice_game_logic;
using Src.GameplayPresenter.GameCreation;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.CellsGeneratorCreators;
using Src.GameplayPresenter.GameCreation.Creators.BoardConfigCreators.ContentSpawnersCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlaceablesConfigCreators;
using Src.GameplayPresenter.GameCreation.Creators.PlayersListCreators;
using Src.GameplayPresenter.GameCreation.Creators.TscConfigCreators;
using Src.TimeManagement;
using Src.Tutorial;
using UnityEngine;

namespace Src.ScenesInitializers
{
    public class TutorialSceneInitializer : MonoBehaviour
    {
        [Header("Ids")]
        [SerializeField] private int playerId = 0;
        [SerializeField] private int enemyId = 1;
        private Player _player;
        private Player _enemy;
        
        [Header("Game data config")]
        [SerializeField] private TutorialGameStartDataConfig gameStartDataConfig;
        private Game _game;

        [Header("Cameras")]
        [SerializeField] private Camera playerCamera;
        
        
        private void Start()
        {
            SetUpGameAndPlayers();
        }

        private void SetUpGameAndPlayers()
        {
            var gameCreator = GetGameCreator();
            _game = gameCreator.CreateGame();
            _player = _game.GetPlayer(playerId);
            _enemy = _game.GetPlayer(enemyId);
        }

        private TutorialGameCreator GetGameCreator()
        {
            var playerTimerCreator = new InfinityPlayerTimerCreator();
            var playerCreator = new PlayerCreator(playerTimerCreator);
            var playersListCreator = new PlayersListCreator(playerCreator);
            var contentToCoordinateCreator = new ContentToCoordinateCreator();
            var spawnersCreator = new CoordinateContentSpawnerCreator(contentToCoordinateCreator);
            var cellsGeneratorCreator = new MatrixCellsGeneratorCreator();
            var boardConfigCreator = new BoardConfigCreator(spawnersCreator, cellsGeneratorCreator);
            var placeablesConfigCreator = new PlaceablesConfigCreator();
            var turnSwitchConditionsConfigCreator = new TurnSwitchConditionsConfigCreator();
            var gameConstructorWrapper = new GameConstructorWrapper();
            var gameBuilder = new GameBuilder(gameConstructorWrapper);
            var gameCreator = new GameCreator(playersListCreator, boardConfigCreator, placeablesConfigCreator, turnSwitchConditionsConfigCreator, gameBuilder);
            var tutorialGameCreator = new TutorialGameCreator(gameCreator, gameStartDataConfig, playerId, enemyId);
            return tutorialGameCreator;
        }
    }
}
