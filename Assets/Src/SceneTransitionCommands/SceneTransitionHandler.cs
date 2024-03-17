using Src.Components;
using Src.LoadingScenes;

namespace Src.SceneTransitionCommands
{
    public class SceneTransitionHandler : ISceneTransitionHandler
    {
        private readonly SceneLoader _sceneLoader;
        private readonly SceneType _sceneType;

        public SceneTransitionHandler(SceneLoader sceneLoader, SceneType sceneType)
        {
            _sceneLoader = sceneLoader;
            _sceneType = sceneType;
        }

        public void HandleTransitionCommand()
        {
            _sceneLoader.LoadSceneWithTransition(_sceneType);
        }
    }
}
