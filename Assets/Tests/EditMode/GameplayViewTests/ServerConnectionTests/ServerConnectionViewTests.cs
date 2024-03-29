using System;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayView.ServerConnection;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tests.EditMode.GameplayViewTests.ServerConnectionTests
{
    public class ServerConnectionViewTests
    {
        [Test]
        public void ShowConnectingMessage_ShouldSetConnectingMessageActive()
        {
            var connectingMessage = new GameObject();
            connectingMessage.SetActive(false);
            var view = new ServerConnectionViewBuilder
                {
                    ConnectionMessage = connectingMessage
                }.Build();
            
            view.ShowConnectingMessage();
            
            Assert.IsTrue(connectingMessage.activeSelf);
        }
        
        [Test]
        public void HideConnectingMessage_ShouldSetConnectingMessageInactive()
        {
            var connectingMessage = new GameObject();
            connectingMessage.SetActive(true);
            var view = new ServerConnectionViewBuilder
                {
                    ConnectionMessage = connectingMessage
                }.Build();
            
            view.HideConnectingMessage();
            
            Assert.IsFalse(connectingMessage.activeSelf);
        }
        
        [Test]
        public void ShowConnectionFailedMessage_ShouldSetConnectionFailedMessageActive()
        {
            var connectionFailedMessage = new GameObject();
            connectionFailedMessage.SetActive(false);
            var view = new ServerConnectionViewBuilder
                {
                    ConnectionFailedMessage = connectionFailedMessage
                }.Build();
            
            view.ShowConnectionFailedMessage(It.IsAny<RejectReason>());
            
            Assert.IsTrue(connectionFailedMessage.activeSelf);
        }
        
        [Test]
        public void ShowConnectionFailedMessage_ShouldSetConnectionFailedReasonText_AccordingToConfigAndRejectReason()
        {
            var connectionFailedReasonText = new GameObject().AddComponent<TextMeshProUGUI>();
            var rejectReasonMessagesConfig = new Mock<IRejectReasonMessagesConfig>();
            var rejectReason = GetRandomRejectReason();
            var rejectReasonMessage = "Some message";
            rejectReasonMessagesConfig.Setup(x => x.GetRejectReasonMessage(rejectReason)).Returns(rejectReasonMessage);
            var view = new ServerConnectionViewBuilder
                {
                    ConnectionFailedMessageText = connectionFailedReasonText,
                    RejectReasonMessagesConfig = rejectReasonMessagesConfig.Object
                }.Build();
            
            view.ShowConnectionFailedMessage(rejectReason);
            
            Assert.AreEqual(rejectReasonMessage, connectionFailedReasonText.text);
        }

        
        private static RejectReason GetRandomRejectReason()
        {
            return (RejectReason) Random.Range(0, Enum.GetValues(typeof(RejectReason)).Length);
        }
        
        private class ServerConnectionViewBuilder
        {
            public GameObject ConnectionMessage { get; set; }
            public GameObject ConnectionFailedMessage { get; set; }
            public TextMeshProUGUI ConnectionFailedMessageText { get; set; }
            public IRejectReasonMessagesConfig RejectReasonMessagesConfig { get; set; }
            
            public ServerConnectionViewBuilder()
            {
                ConnectionMessage = new GameObject();
                ConnectionFailedMessage = new GameObject();
                ConnectionFailedMessageText = new GameObject().AddComponent<TextMeshProUGUI>();
                RejectReasonMessagesConfig = new Mock<IRejectReasonMessagesConfig>().Object;
            }
            
            public ServerConnectionView Build()
            {
                return new ServerConnectionView(ConnectionMessage, ConnectionFailedMessage, ConnectionFailedMessageText, RejectReasonMessagesConfig);
            }
        }
    }
}