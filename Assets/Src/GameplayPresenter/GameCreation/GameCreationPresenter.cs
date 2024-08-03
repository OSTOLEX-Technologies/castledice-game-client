using System;
using castledice_game_data_logic;
using Src.Components;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.GameSearching;
using Src.GameplayPresenter.GameCreation.Timeout;
using Src.General.LoadingScenes;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreationPresenter : IDisposable
    {
        private readonly IGameCreationView _view;
        private readonly IGameSearcher _gameSearcher;
        private readonly IGameCreator _gameCreator;
        private readonly IGameCreationHandler _gameCreationHandler;
        private readonly ITimeout _timeout;
        private readonly ISceneLoader _sceneLoader;

        private bool _timeIsOut;
        private bool _gameFound;

        public GameCreationPresenter(
            IGameCreationView view, 
            IGameSearcher gameSearcher, 
            IGameCreator gameCreator, 
            IGameCreationHandler gameCreationHandler,
            ITimeout timeout,
            ISceneLoader sceneLoader)
        {
            _view = view;
            _view.PlayChosen += OnPlayChosen;
            _view.CancelChosen += OnCancelChosen;
            _gameSearcher = gameSearcher;
            _gameSearcher.GameFound += OnGameFound;
            _gameSearcher.CancellationApproved += OnCancellationApproved;
            _gameSearcher.SearchFailed += OnSearchFailed;
            _gameSearcher.CancellationFailed += OnCancellationFailed;
            _gameCreator = gameCreator;
            _gameCreationHandler = gameCreationHandler;
            _timeout = timeout;
            _timeout.TimeOut += OnTimeOut;
            _sceneLoader = sceneLoader;
        }

        private async void OnPlayChosen()
        {
            _timeIsOut = false;
            _gameFound = false;
            _timeout.StartCountdown();
            _view.ShowMatchmakingScreen();
            await _gameSearcher.SearchAsync();
        }

        private async void OnTimeOut()
        {
            if (_gameFound) return;
            _timeIsOut = true;
            await _gameSearcher.CancelAsync();
        }

        private async void OnCancelChosen()
        {
            if (_timeIsOut) return;
            _timeout.CancelCountdown();
            _view.ShowCancellationScreen();
            await _gameSearcher.CancelAsync();
        }
        private void OnGameFound(GameStartData startData)
        {
            _gameFound = true;
            var game = _gameCreator.CreateGame(startData);
            _gameCreationHandler.HandleCreatedGame(game, startData);
        }
        
        private void OnCancellationApproved()
        {
            if (_timeIsOut)
            {
                _sceneLoader.LoadSceneWithTransition(SceneType.PVE);
                return;
            }

            _view.HideMatchmakingScreen();
            _view.HideCancellationScreen();
        }
        
        private void OnCancellationFailed()
        {
            _view.HideMatchmakingScreen();
            _view.HideCancellationScreen();
        }
        
        private void OnSearchFailed(SearchFailReason reason)
        {
            _view.HideMatchmakingScreen();
            _view.ShowFail(reason);
        }

        public void Dispose()
        {
            _view.PlayChosen -= OnPlayChosen;
            _view.CancelChosen -= OnCancelChosen;
            _gameSearcher.GameFound -= OnGameFound;
            _gameSearcher.CancellationApproved -= OnCancellationApproved;
            _gameSearcher.CancellationFailed -= OnCancellationFailed;
            _gameSearcher.SearchFailed -= OnSearchFailed;
            _timeout.TimeOut -= OnTimeOut;
        }
    }
}