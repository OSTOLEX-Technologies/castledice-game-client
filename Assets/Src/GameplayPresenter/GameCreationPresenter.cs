using System;
using System.Threading.Tasks;
using Src.Constants;
using Src.GameplayView;

namespace Src.GameplayPresenter
{
    public class GameCreationPresenter
    {
        private readonly IGameSearcher _gameSearcher;
        private readonly IGameCreator _gameCreator;
        private readonly IPlayerDataProvider _playerDataProvider;
        private readonly GameHolder _gameHolder;
        private readonly GameCreationView _view;
        private bool _gameCreationInProcess;

        public GameCreationPresenter(IGameSearcher gameSearcher, IGameCreator gameCreator, IPlayerDataProvider playerDataProvider, GameHolder gameHolder, GameCreationView view)
        {
            _gameSearcher = gameSearcher;
            _gameCreator = gameCreator;
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
                var game = _gameCreator.CreateGame(gameStartData);
                _gameHolder.Game = game;
            }
        }

        public virtual async Task CancelGame()
        {
            if (!_gameCreationInProcess) return;
            
            _view.ShowCancelationMessage(MessagesStrings.GameSearchCancelationMessage);
            
            var accessToken = _playerDataProvider.GetAccessToken();
            var canceled = await _gameSearcher.CancelGameSearchAsync(accessToken);
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
