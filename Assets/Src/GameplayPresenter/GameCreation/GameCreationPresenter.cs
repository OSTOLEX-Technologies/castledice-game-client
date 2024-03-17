using System;
using System.Threading.Tasks;
using castledice_game_data_logic;
using castledice_game_logic;
using Src.Auth.TokenProviders;
using Src.Caching;
using Src.Constants;
using Src.GameplayView.GameCreation;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreationPresenter
    {
        public event EventHandler GameCreated;
        
        private readonly IGameSearcher _gameSearcher;
        private readonly IGameCreator _gameCreator;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IGameCreationView _view;
        private bool _gameCreationInProcess;

        public GameCreationPresenter(IGameSearcher gameSearcher, IGameCreator gameCreator, IAccessTokenProvider accessTokenProvider, IGameCreationView view)
        {
            _gameSearcher = gameSearcher;
            _gameCreator = gameCreator;
            _accessTokenProvider = accessTokenProvider;
            _view = view;
            view.CancelCreationChosen += OnCancelGame;
            view.CreateGameChosen += OnCreateGame;
        }

        
        //TODO: Refactor this method as it is too long.
        public virtual async Task CreateGame()
        {
            var accessToken = await _accessTokenProvider.GetAccessTokenAsync();
            
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
                if (Singleton<Game>.Registered) //TODO: Refactor this and get rid of singleton
                {
                    Singleton<Game>.Unregister();
                }

                if (Singleton<GameStartData>.Registered)
                {
                    Singleton<GameStartData>.Unregister();
                }
                Singleton<Game>.Register(game);
                Singleton<GameStartData>.Register(gameStartData);
                GameCreated?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual async Task CancelGame()
        {
            if (!_gameCreationInProcess) return;
            
            _view.ShowCancelationMessage(MessagesStrings.GameSearchCancelationMessage);
            
            var accessToken = await _accessTokenProvider.GetAccessTokenAsync();
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
