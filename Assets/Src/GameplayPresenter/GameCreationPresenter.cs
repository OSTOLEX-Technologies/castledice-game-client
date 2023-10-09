using System;
using System.Threading.Tasks;
using Src.Constants;
using Src.GameplayView;

namespace Src.GameplayPresenter
{
    public class GameCreationPresenter
    {
        private readonly GameSearcher _gameSearcher;
        private readonly GameInitializer _gameInitializer;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly GameHolder _gameHolder;
        private readonly GameCreationView _view;
        private bool _gameCreationInProcess;

        public GameCreationPresenter(GameSearcher gameSearcher, GameInitializer gameInitializer, PlayerDataProvider playerDataProvider, GameHolder gameHolder, GameCreationView view)
        {
            _gameSearcher = gameSearcher;
            _gameInitializer = gameInitializer;
            _playerDataProvider = playerDataProvider;
            _gameHolder = gameHolder;
            _view = view;
            view.CancelCreationChosen += OnCancelGame;
            view.CreateGameChosen += OnCreateGame;
        }

        public virtual async Task CreateGame()
        {
            var isAuthorized = _playerDataProvider.IsAuthorized();
            if (!isAuthorized)
            {
                _view.ShowNonAuthorizedMessage(MessagesStrings.NonAuthorizedMessage);
                return;
            }
            var accessToken = _playerDataProvider.GetAccessToken();
            
            _view.ShowCreationProcessScreen();
            _gameCreationInProcess = true;
            var gameSearchResult = await _gameSearcher.SearchGameAsync(accessToken);
            _gameCreationInProcess = false;
            _view.HideCreationProcessScreen();
            
            if (gameSearchResult.Status == GameSearchResult.ResultStatus.Canceled)
            {
                _view.HideCancelationMessage();
            }
            else if (gameSearchResult.Status == GameSearchResult.ResultStatus.Success)
            {
                var gameStartData = gameSearchResult.GameStartData;
                var game = _gameInitializer.InitializeGame(gameStartData);
                _gameHolder.Game = game;
            }
        }

        public virtual async Task CancelGame()
        {
            if (!_gameCreationInProcess) return;
            
            _view.ShowCancelationMessage(MessagesStrings.GameSearchCancelationMessage);
            
            var accessToken = _playerDataProvider.GetAccessToken();
            var canceled = await _gameSearcher.CancelGameSearch(accessToken);
            if (!canceled)
            {
                _view.HideCancelationMessage();
            }
        }
        
        private async void OnCreateGame(object sender, EventArgs e)
        {
            await CreateGame();
        }

        private async void OnCancelGame(object sender, EventArgs e)
        {
            await CancelGame();
        }
    }
}
