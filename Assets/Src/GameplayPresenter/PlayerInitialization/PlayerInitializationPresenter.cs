using System;
using System.Threading.Tasks;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public class PlayerInitializationPresenter
    {
        private readonly IPlayerInitializationView _view;
        private readonly IInitializePlayerDtoSender _dtoSender;
        private readonly IPlayerInitializationFinishedEventEmitter _initializationFinishedEventEmitter;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IPlayerInitializationSaver _initializationSaver;

        public PlayerInitializationPresenter(IPlayerInitializationView view, IInitializePlayerDtoSender dtoSender, IPlayerInitializationFinishedEventEmitter initializationFinishedEventEmitter, IAccessTokenProvider accessTokenProvider, IPlayerInitializationSaver initializationSaver)
        {
            _view = view;
            _dtoSender = dtoSender;
            _initializationFinishedEventEmitter = initializationFinishedEventEmitter;
            _accessTokenProvider = accessTokenProvider;
            _initializationSaver = initializationSaver;
        }

        public async Task StartInitialization()
        {
            throw default!;
        }
    }
}