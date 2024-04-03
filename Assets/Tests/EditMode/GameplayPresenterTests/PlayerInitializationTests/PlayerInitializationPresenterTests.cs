using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Src.Auth.TokenProviders;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;

namespace Tests.EditMode.GameplayPresenterTests.PlayerInitializationTests
{
    public class PlayerInitializationPresenterTests
    {
        [Test]
        public async Task StartInitialization_ShouldCall_ShowProcessMessage_OnView()
        {
            var viewMock = new Mock<IPlayerInitializationView>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                View = viewMock.Object
            }.Build();

            await presenter.StartInitialization();
            
            viewMock.Verify(x => x.ShowProcessMessage());
        }

        [Test]
        public async Task Presenter_ShouldCall_HideProcessMessage_OnView_IfInitializationSucceed()
        {
            var viewMock = new Mock<IPlayerInitializationView>();
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                View = viewMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();

            eventsEmitterMock.Raise(
                x => x.InitializationSucceed += 
                    null, viewMock.Object, EventArgs.Empty);
            
            viewMock.Verify(v => v.HideProcessMessage());
        } 

        private class PlayerInitializationPresenterBuilder
        {
            public IPlayerInitializationView View = new Mock<IPlayerInitializationView>().Object;
            public IInitializePlayerDtoSender DtoSender = new Mock<IInitializePlayerDtoSender>().Object;
            public IPlayerInitializationResultEventsEmitter InitializationResultEventsEmitter = new Mock<IPlayerInitializationResultEventsEmitter>().Object;
            public IAccessTokenProvider AccessTokenProvider = new Mock<IAccessTokenProvider>().Object;
            public IPlayerInitializationSaver InitializationSaver = new Mock<IPlayerInitializationSaver>().Object;

            public PlayerInitializationPresenter Build()
            {
                return new PlayerInitializationPresenter(
                    View,
                    DtoSender,
                    InitializationResultEventsEmitter,
                    AccessTokenProvider,
                    InitializationSaver);
            }
        }
    }
}