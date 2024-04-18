using castledice_game_data_logic;
using castledice_game_logic;
using Src.Components;
using Src.General.LoadingScenes;

namespace Src.GameplayPresenter.GameCreation.CreationHandling
{
    public class SceneLoadingGameCreationHandler : IGameCreationHandler
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly SceneType _sceneType;

        public SceneLoadingGameCreationHandler(ISceneLoader sceneLoader, SceneType sceneType)
        {
            _sceneLoader = sceneLoader;
            _sceneType = sceneType;
        }

        public void HandleCreatedGame(Game game, GameStartData gameStartData)
        {
            _sceneLoader.LoadSceneWithTransition(_sceneType);
        }
    }
}