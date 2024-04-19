using System;
using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.PlayerInitialization.NetworkBridges;
using Src.NetworkingModule;

namespace Tests.EditMode.GameplayPresenterTests.PlayerInitializationTests.NetworkBridgesTests
{
    public class InitializePlayerDtoSenderTests
    {
        [Test]
        public void SendDto_ShouldSendMessage_WithGivenDto()
        {
            var expectedDto = new InitializePlayerDTO(new Random().Next().ToString());
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(x => x.Send(It.IsAny<Message>()))
                .Callback<Message>(message => sentMessage = message);
            var sender = new InitializePlayerDtoSender(messageSenderMock.Object);
            
            sender.SendDto(expectedDto);
            sentMessage.GetByte();
            var actualDto = sentMessage.GetInitializePlayerDTO();
            
            Assert.AreEqual(expectedDto.VerificationKey, actualDto.VerificationKey);
        }

        [Test]
        public void SendDto_ShouldSendMessage_WithReliableSendMode()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(x => x.Send(It.IsAny<Message>()))
                .Callback<Message>(message => sentMessage = message);
            var sender = new InitializePlayerDtoSender(messageSenderMock.Object);
            
            sender.SendDto(new InitializePlayerDTO("test"));
            
            Assert.AreEqual(MessageSendMode.Reliable, sentMessage.SendMode);
        }

        [Test]
        public void SendDto_ShouldSendMessage_WithInitializePlayerMessageType()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(x => x.Send(It.IsAny<Message>()))
                .Callback<Message>(message => sentMessage = message);
            var sender = new InitializePlayerDtoSender(messageSenderMock.Object);
            
            sender.SendDto(new InitializePlayerDTO("test"));
            var actualMessageType = sentMessage.GetByte();
            
            Assert.AreEqual((ushort)ClientToServerMessageType.InitializePlayer, actualMessageType);
        }
        
        [Test]
        [Repeat(50)]
        public void CanSend_ShouldReturnMessageSenderCanSend()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            var expected = new Random().Next() % 2 == 0;
            messageSenderMock.Setup(x => x.CanSend).Returns(expected);
            var sender = new InitializePlayerDtoSender(messageSenderMock.Object);
            
            var actual = sender.CanSend;
            
            Assert.AreEqual(expected, actual);
        }
    }
}