using System;
using System.Threading.Tasks;
using castledice_events_logic.ServerToClient;
using castledice_game_data_logic;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;
using Src.General.PlayerInitialization;
using Src.NetworkingModule;

namespace Src.GameplayPresenter.GameCreation.GameSearching
{
    public class GameSearcher : IGameSearcher, ICreateGameDtoAccepter, ICancelGameResultDtoAccepter
    {
        public event Action<GameStartData> GameFound;
        public event Action<SearchFailReason> SearchFailed;
        public event Action CancellationApproved;

        private readonly IGameRequester _gameRequester;
        private readonly IGameCancelRequester _cancelRequester;
        private readonly IClientWrapper _clientWrapper;
        private readonly IPlayerInitializationProvider _initializationProvider;

        public GameSearcher(IGameRequester gameRequester, IGameCancelRequester cancelRequester, IClientWrapper clientWrapper, IPlayerInitializationProvider initializationProvider)
        {
            _gameRequester = gameRequester;
            _cancelRequester = cancelRequester;
            _clientWrapper = clientWrapper;
            _initializationProvider = initializationProvider;
        }

        public async Task SearchAsync()
        {
            if (!_clientWrapper.IsConnected)
            {
                SearchFailed?.Invoke(SearchFailReason.NotConnected);
                return;
            }
            if (!_initializationProvider.Initialized)
            {
                SearchFailed?.Invoke(SearchFailReason.NotInitialized);
                return;
            }
            await _gameRequester.RequestGameAsync();
        }

        public async Task CancelAsync()
        {
            if (!_clientWrapper.IsConnected || !_initializationProvider.Initialized) return;
            await _cancelRequester.RequestCancelAsync();
        }

        public void AcceptCreateGameDto(CreateGameDTO dto)
        {
            GameFound?.Invoke(dto.GameStartData);
        }

        public void AcceptCancelGameResultDto(CancelGameResultDTO cancelGameResultDto)
        {
            if (cancelGameResultDto.IsCanceled)
            {
                CancellationApproved?.Invoke();
            }
        }
    }
}