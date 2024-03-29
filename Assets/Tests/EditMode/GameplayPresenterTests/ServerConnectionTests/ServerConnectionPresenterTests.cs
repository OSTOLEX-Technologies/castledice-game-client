using System;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.ServerConnection;
using Src.GameplayView.ServerConnection;
using Src.NetworkingModule;

namespace Tests.EditMode.GameplayPresenterTests.ServerConnectionTests
{
    public class ServerConnectionPresenterTests
    {
        [Test]
        public void TryConnectToServer_ShouldShowConnectingMessage_OnView()
        {
            var viewMock = new Mock<IServerConnectionView>();
            var presenter = new ServerConnectionPresenterBuilder
            {
                View = viewMock.Object
            }.Build();
            
            presenter.TryConnectToServer();
            
            viewMock.Verify(x => x.ShowConnectingMessage(), Times.Once);
        }

        [Test]
        public void TryConnectToServer_ShouldCallConnectOnClientWrapper_WithParametersFromConnectionConfig()
        {
            var rnd = new System.Random();
            var hostAddress = rnd.Next().ToString();
            var maxConnectionAttempts = rnd.Next();
            var connectionConfigMock = new Mock<IServerConnectionConfig>();
            connectionConfigMock.Setup(x => x.HostAddress).Returns(hostAddress);
            connectionConfigMock.Setup(x => x.MaxConnectionAttempts).Returns(maxConnectionAttempts);
            var clientWrapperMock = new Mock<IClientWrapper>();
            var presenter = new ServerConnectionPresenterBuilder
            {
                ConnectionConfig = connectionConfigMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            presenter.TryConnectToServer();
            
            clientWrapperMock.Verify(x => x.Connect(hostAddress, maxConnectionAttempts, It.IsAny<byte>(), It.IsAny<Message>()), Times.Once);
        }
        

        [Test]
        public void TryConnectToServer_ShouldCallConnectOnClientWrapper_Once()
        {
            var clientWrapperMock = new Mock<IClientWrapper>();
            var presenter = new ServerConnectionPresenterBuilder
            {
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            presenter.TryConnectToServer();
            
            clientWrapperMock.Verify(x => x.Connect(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<byte>(), It.IsAny<Message>()), Times.Once);
        }
        
        [Test]
        public void Presenter_ShouldCallHideConnectingMessageOnView_IfClientConnected()
        {
            var viewMock = new Mock<IServerConnectionView>();
            var clientWrapperMock = new Mock<IClientWrapper>();
            var presenter = new ServerConnectionPresenterBuilder
            {
                View = viewMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.Connected += null, EventArgs.Empty);
            
            viewMock.Verify(x => x.HideConnectingMessage(), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallHideConnectingMessageOnView_IfConnectionFailed()
        {
            var viewMock = new Mock<IServerConnectionView>();
            var clientWrapperMock = new Mock<IClientWrapper>();
            var presenter = new ServerConnectionPresenterBuilder
            {
                View = viewMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.ConnectionFailed += null, new ConnectionFailedEventArgs(It.IsAny<RejectReason>(), It.IsAny<Message>()));
            
            viewMock.Verify(x => x.HideConnectingMessage(), Times.Once);
        }

        [Test]
        public void Presenter_ShouldCallShowConnectionFailedMessageOnView_WithRejectReason_IfConnectionFailed()
        {
            var viewMock = new Mock<IServerConnectionView>();
            var clientWrapperMock = new Mock<IClientWrapper>();
            var rejectReason = GetRandomRejectReason();
            var presenter = new ServerConnectionPresenterBuilder
            {
                View = viewMock.Object,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.ConnectionFailed += null, new ConnectionFailedEventArgs(rejectReason, It.IsAny<Message>()));
            
            viewMock.Verify(x => x.ShowConnectionFailedMessage(rejectReason), Times.Once);
        }
        
        private static RejectReason GetRandomRejectReason()
        {
            var values = Enum.GetValues(typeof(RejectReason));
            return (RejectReason)values.GetValue(new System.Random().Next(values.Length));
        }

        private class ServerConnectionPresenterBuilder
        {
            public IServerConnectionView View { get; set; }
            public IClientWrapper ClientWrapper { get; set; }
            public IServerConnectionConfig ConnectionConfig { get; set; }
    
            public ServerConnectionPresenterBuilder()
            {
                var viewMock = new Mock<IServerConnectionView>();
                View = viewMock.Object;
                var clientWrapperMock = new Mock<IClientWrapper>();
                ClientWrapper = clientWrapperMock.Object;
                var connectionConfigMock = new Mock<IServerConnectionConfig>();
                ConnectionConfig = connectionConfigMock.Object;
            }
            
            public ServerConnectionPresenter Build()
            {
                return new ServerConnectionPresenter(View, ClientWrapper, ConnectionConfig);
            }
        }
    }
}