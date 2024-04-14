using castledice_game_data_logic;
using Src.GameplayPresenter.GameCreation.CreationHandling;
using Src.GameplayPresenter.GameCreation.Creators.GameCreator;
using Src.GameplayPresenter.GameCreation.GameSearching;

namespace Src.GameplayPresenter.GameCreation
{
    public class GameCreationPresenter
    {
        private readonly IGameCreationView _view;
        private readonly IGameSearcher _gameSearcher;
        private readonly IGameCreator _gameCreator;
        private readonly IGameCreationHandler _gameCreationHandler;

        public GameCreationPresenter(IGameCreationView view, IGameSearcher gameSearcher, IGameCreator gameCreator, IGameCreationHandler gameCreationHandler)
        {
            _view = view;
            _view.PlayChosen += OnPlayChosen;
            _view.CancelChosen += OnCancelChosen;
            _gameSearcher = gameSearcher;
            _gameSearcher.GameFound += OnGameFound;
            _gameSearcher.CancellationApproved += OnCancellationApproved;
            _gameSearcher.SearchFailed += OnSearchFailed;
            _gameCreator = gameCreator;
            _gameCreationHandler = gameCreationHandler;
        }

        private void OnPlayChosen()
        {
            _view.ShowMatchmakingScreen();
            _gameSearcher.Search();
        }
        private void OnCancelChosen()
        {
            _view.ShowCancellationScreen();
            _gameSearcher.Cancel();
        }
        private void OnGameFound(GameStartData startData)
        {
            var game = _gameCreator.CreateGame(startData);
            _gameCreationHandler.HandleCreatedGame(game, startData);
        }
        
        private void OnCancellationApproved()
        {
            _view.HideMatchmakingScreen();
            _view.HideCancellationScreen();
        }
        private void OnSearchFailed(GameSearchFailReason reason)
        {
            _view.ShowFail(reason);
        }
    }
}