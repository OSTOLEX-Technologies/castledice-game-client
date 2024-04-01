using System;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.ServerConnection;
using Src.NetworkingModule;
using UnityEngine;
using UnityEngine.UI;

namespace Tests.EditMode.GameplayPresenterTests.ServerConnectionTests
{
    public class ConnectButtonHandlerTests
    {
        [Test]
        public void Handler_ShouldCallConnectToServer_IfButtonClicked()
        {
            var button = new GameObject().AddComponent<Button>();
            var presenterMock = new Mock<IServerConnectionPresenter>();
            var handler = new ConnectButtonHandlerBuilder
            {
                Button = button,
                ServerConnectionPresenter = presenterMock.Object
            }.Build();

            button.onClick.Invoke();
            
            presenterMock.Verify(x => x.ConnectToServer(), Times.Once);
        }

        [Test]
        public void Handler_ShouldSetConnectButtonActive_IfConnectionFailed()
        {
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(false);
            var clientWrapperMock = new Mock<IClientWrapper>();
            var handler = new ConnectButtonHandlerBuilder
            {
                Button = button,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.ConnectionFailed += null, new ConnectionFailedEventArgs(RejectReason.Custom, null));
            
            Assert.IsTrue(button.gameObject.activeSelf);
        }

        [Test]
        public void Handler_ShouldSetConnectButtonActive_IfDisconnected()
        {
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(false);
            var clientWrapperMock = new Mock<IClientWrapper>();
            var handler = new ConnectButtonHandlerBuilder
            {
                Button = button,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.Disconnected += null, new DisconnectedEventArgs(DisconnectReason.Disconnected, null));
            
            Assert.IsTrue(button.gameObject.activeSelf);
        }
        
        [Test]
        public void Handler_ShouldSetConnectButtonInactive_IfConnected()
        {
            var button = new GameObject().AddComponent<Button>();
            button.gameObject.SetActive(true);
            var clientWrapperMock = new Mock<IClientWrapper>();
            var handler = new ConnectButtonHandlerBuilder
            {
                Button = button,
                ClientWrapper = clientWrapperMock.Object
            }.Build();
            
            clientWrapperMock.Raise(x => x.Connected += null, EventArgs.Empty);
            
            Assert.IsFalse(button.gameObject.activeSelf);
        }

        private class ConnectButtonHandlerBuilder
        {
            public Button Button { get; set; }
            public IServerConnectionPresenter ServerConnectionPresenter { get; set; }
            public IClientWrapper ClientWrapper { get; set; }
            
            public ConnectButtonHandlerBuilder()
            {
                Button = new GameObject().AddComponent<Button>();
                ServerConnectionPresenter = new Mock<IServerConnectionPresenter>().Object;
                ClientWrapper = new Mock<IClientWrapper>().Object;
            }
            
            public ConnectButtonHandler Build()
            {
                return new ConnectButtonHandler(Button, ServerConnectionPresenter, ClientWrapper);
            }
        }
    }
}