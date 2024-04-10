using System;
using System.Threading.Tasks;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using IInitializePlayerDtoCreator = Src.NetworkingModule.DTOCreators.IInitializePlayerDtoCreator;

namespace Src.GameplayPresenter.PlayerInitialization
{
    public class PlayerInitializationPresenter
    {
        private readonly IPlayerInitializationView _view;
        private readonly IInitializePlayerDtoSender _dtoSender;
        private readonly IPlayerInitializationResultEventsEmitter _initializationResultEventsEmitter;
        private readonly IInitializePlayerDtoCreator _dtoCreator;
        private readonly IPlayerInitializationSaver _initializationSaver;

        public PlayerInitializationPresenter(IPlayerInitializationView view, IInitializePlayerDtoSender dtoSender, IPlayerInitializationResultEventsEmitter initializationResultEventsEmitter, IInitializePlayerDtoCreator dtoCreator, IPlayerInitializationSaver initializationSaver)
        {
            _view = view;
            _dtoSender = dtoSender;
            _initializationResultEventsEmitter = initializationResultEventsEmitter;
            _initializationResultEventsEmitter.InitializationSucceed += OnInitializationSucceed;
            _initializationResultEventsEmitter.InitializationFailed += OnInitializationFailed;
            _dtoCreator = dtoCreator;
            _initializationSaver = initializationSaver;
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
        

        public async Task StartInitialization()
        {
            if (!_dtoSender.CanSend)
            {
                _view.ShowFailureMessage();
                return;
            }
            _view.ShowProcessMessage();
            var dto = await _dtoCreator.CreateAsync();
            _dtoSender.SendDto(dto);
        }
    }
}