using System;
using Src.Components;
using Src.LoadingScenes;

namespace Src.SceneTransitionCommands
{
    public class AuthSceneTransitionCommandHandler : ISceneTransitionCommand
    {
        private readonly SceneLoader _sceneLoader;
        private readonly SceneType _sceneType;

        public AuthSceneTransitionCommandHandler(SceneLoader sceneLoader, SceneType sceneType)
        {
            _sceneLoader = sceneLoader;
            _sceneType = sceneType;
        }

        public void HandleTransitionCommand(object signalSender, EventArgs args)
        {
            _sceneLoader.LoadSceneWithTransition(_sceneType);
        }
    }
}
