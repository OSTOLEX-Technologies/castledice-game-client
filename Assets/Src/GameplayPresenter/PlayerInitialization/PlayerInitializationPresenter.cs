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
        private readonly IPlayerInitializationResultEventsEmitter _initializationResultEventsEmitter;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IPlayerInitializationSaver _initializationSaver;

        public PlayerInitializationPresenter(IPlayerInitializationView view, IInitializePlayerDtoSender dtoSender, IPlayerInitializationResultEventsEmitter initializationResultEventsEmitter, IAccessTokenProvider accessTokenProvider, IPlayerInitializationSaver initializationSaver)
        {
            _view = view;
            _dtoSender = dtoSender;
            _initializationResultEventsEmitter = initializationResultEventsEmitter;
            _initializationResultEventsEmitter.InitializationSucceed += OnInitializationSucceed;
            _accessTokenProvider = accessTokenProvider;
            _initializationSaver = initializationSaver;
        }

        private void OnInitializationSucceed(object sender, EventArgs e)
        {
            _view.HideProcessMessage();
        }

        public async Task StartInitialization()
        {
            _view.ShowProcessMessage();
        }
    }
}