using System;
using Src.Components;
using Src.General.LoadingScenes;

namespace Src.GameplayPresenter.EnemyDisconnect
{
    public class EnemyDisconnectPresenter : IDisposable
    {
        private readonly IEnemyDisconnectView _view;
        private readonly IEnemyDisconnectedEventEmitter _eventEmitter;
        private readonly ISceneLoader _sceneLoader;

        public EnemyDisconnectPresenter(IEnemyDisconnectView view, IEnemyDisconnectedEventEmitter eventEmitter, ISceneLoader sceneLoader)
        {
            _view = view;
            _view.OkPressed += OnOkPressed;
            _eventEmitter = eventEmitter;
            _eventEmitter.EnemyDisconnected += OnEnemyDisconnected;
            _sceneLoader = sceneLoader;
        }

        private void OnOkPressed()
        {
            _sceneLoader.LoadSceneWithTransition(SceneType.MainMenu);
        }

        private void OnEnemyDisconnected()
        {
            _view.ShowPopup();
        }

        public void Dispose()
        {
            _view.OkPressed -= OnOkPressed;
            _eventEmitter.EnemyDisconnected -= OnEnemyDisconnected;
        }
    }
}