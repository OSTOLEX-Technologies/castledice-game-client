using System;
using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using castledice_events_logic.ServerToClient;
using Moq;
using NUnit.Framework;
using Src.GameplayPresenter.PlayerInitialization;
using Src.GameplayPresenter.PlayerInitialization.Caching;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.NetworkingModule.DTOCreators;

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
        public async Task StartInitialization_ShouldSendDto_FromCreator_ViaSender()
        {
            var expectedDto = new InitializePlayerDTO("thisstringdoesntreallymatterhere");
            var senderMock = new Mock<IInitializePlayerDtoSender>();
            var dtoCreatorMock = new Mock<IInitializePlayerDtoCreator>();
            dtoCreatorMock.Setup(x => x.CreateAsync()).ReturnsAsync(expectedDto);
            var presenter = new PlayerInitializationPresenterBuilder
            {
                DtoSender = senderMock.Object,
                DtoCreator = dtoCreatorMock.Object
            }.Build();
            
            await presenter.StartInitialization();
            
            senderMock.Verify(x => x.SendDto(expectedDto));
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
        
        [Test]
        public async Task Presenter_ShouldNotCall_ShowProcessMessage_OnView_IfInitializationSucceed()
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
            
            viewMock.Verify(v => v.ShowProcessMessage(), Times.Never);
        }

        [Test]
        public async Task Presenter_ShouldSaveInitializationAsTrue_IfInitializationSucceed()
        {
            var saverMock = new Mock<IPlayerInitializationSaver>();
            var initialization = false;
            saverMock.Setup(x => x.SetInitialization(It.IsAny<bool>()))
                .Callback<bool>(x => initialization = x);
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                InitializationSaver = saverMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();
            
            eventsEmitterMock.Raise(
                x => x.InitializationSucceed += 
                    null, new object(), EventArgs.Empty);
            
            Assert.IsTrue(initialization);
        }
        
        [Test]
        public async Task Presenter_ShouldSaveInitializationAsFalse_IfInitializationFailed()
        {
            var saverMock = new Mock<IPlayerInitializationSaver>();
            var initialization = true;
            saverMock.Setup(x => x.SetInitialization(It.IsAny<bool>()))
                .Callback<bool>(x => initialization = x);
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                InitializationSaver = saverMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();
            
            eventsEmitterMock.Raise(
                x => x.InitializationFailed += 
                    null, new object(),  EventArgs.Empty);
            
            Assert.IsFalse(initialization);
        }
        
        [Test]
        public async Task Presenter_ShouldCall_HideProcessMessage_OnView_IfInitializationFailed()
        {
            var viewMock = new Mock<IPlayerInitializationView>();
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                View = viewMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();

            eventsEmitterMock.Raise(
                x => x.InitializationFailed += 
                    null, viewMock.Object, EventArgs.Empty);
            
            viewMock.Verify(v => v.HideProcessMessage());
        }
        
        [Test]
        public async Task Presenter_ShouldNotCall_ShowProcessMessage_OnView_IfInitializationFailed()
        {
            var viewMock = new Mock<IPlayerInitializationView>();
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                View = viewMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();

            eventsEmitterMock.Raise(
                x => x.InitializationFailed += 
                    null, viewMock.Object, EventArgs.Empty);
            
            viewMock.Verify(v => v.ShowProcessMessage(), Times.Never);
        }
        
        [Test]
        public async Task Presenter_ShouldCall_ShowFailureMessage_OnView_IfInitializationFailed()
        {
            var viewMock = new Mock<IPlayerInitializationView>();
            var eventsEmitterMock = new Mock<IPlayerInitializationResultEventsEmitter>();
            var presenter = new PlayerInitializationPresenterBuilder
            {
                View = viewMock.Object,
                InitializationResultEventsEmitter = eventsEmitterMock.Object,
            }.Build();

            eventsEmitterMock.Raise(
                x => x.InitializationFailed += 
                    null, viewMock.Object, EventArgs.Empty);
            
            viewMock.Verify(v => v.ShowFailureMessage());
        }

        private class PlayerInitializationPresenterBuilder
        {
            public IPlayerInitializationView View = new Mock<IPlayerInitializationView>().Object;
            public IInitializePlayerDtoSender DtoSender = new Mock<IInitializePlayerDtoSender>().Object;
            public IPlayerInitializationResultEventsEmitter InitializationResultEventsEmitter = new Mock<IPlayerInitializationResultEventsEmitter>().Object;
            public IInitializePlayerDtoCreator DtoCreator = new Mock<IInitializePlayerDtoCreator>().Object;
            public IPlayerInitializationSaver InitializationSaver = new Mock<IPlayerInitializationSaver>().Object;

            public PlayerInitializationPresenter Build()
            {
                return new PlayerInitializationPresenter(
                    View,
                    DtoSender,
                    InitializationResultEventsEmitter,
                    DtoCreator,
                    InitializationSaver);
            }
        }
    }
}