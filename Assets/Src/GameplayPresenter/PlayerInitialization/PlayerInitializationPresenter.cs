using System;
using System.Threading.Tasks;
using Riptide;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.NetworkingModule;
using Src.NetworkingModule.DTOCreators;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public class PlayerInitializationPresenter : IPlayerInitializationPresenter, IDisposable
    {
        private readonly IPlayerInitializationView _view;
        private readonly IInitializePlayerDtoSender _dtoSender;
        private readonly IPlayerInitializationResultEventsEmitter _initializationResultEventsEmitter;
        private readonly IInitializePlayerDtoCreator _dtoCreator;
        private readonly IPlayerInitializationSaver _initializationSaver;
        private readonly IDisconnectedEventEmitter _disconnectedEventEmitter;

        public PlayerInitializationPresenter(IPlayerInitializationView view, IInitializePlayerDtoSender dtoSender, IPlayerInitializationResultEventsEmitter initializationResultEventsEmitter, IInitializePlayerDtoCreator dtoCreator, IPlayerInitializationSaver initializationSaver, IDisconnectedEventEmitter disconnectedEventEmitter)
        {
            _view = view;
            _dtoSender = dtoSender;
            _initializationResultEventsEmitter = initializationResultEventsEmitter;
            _initializationResultEventsEmitter.InitializationSucceed += OnInitializationSucceed;
            _initializationResultEventsEmitter.InitializationFailed += OnInitializationFailed;
            _dtoCreator = dtoCreator;
            _initializationSaver = initializationSaver;
            _disconnectedEventEmitter = disconnectedEventEmitter;
            _disconnectedEventEmitter.Disconnected += OnDisconnected;
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs disconnectedEventArgs)
        {
            _view.HideProcessMessage();
        }

        private void OnInitializationFailed(object sender, EventArgs e)
        {
            _view.HideProcessMessage();
            _initializationSaver.SetInitialization(false);
            _view.ShowFailureMessage();
        }

        private void OnInitializationSucceed(object sender, EventArgs e)
        {
            _view.HideProcessMessage();
            _initializationSaver.SetInitialization(true);
        }
        

        public async Task StartInitializationAsync()
        {
            if (!_dtoSender.CanSend) return;
            _view.ShowProcessMessage();
            var dto = await _dtoCreator.CreateAsync();
            _dtoSender.SendDto(dto);
        }

        public void Dispose()
        {
            _initializationResultEventsEmitter.InitializationSucceed -= OnInitializationFailed;
            _initializationResultEventsEmitter.InitializationSucceed -= OnInitializationSucceed;
            _disconnectedEventEmitter.Disconnected -= OnDisconnected;
        }
    }
}