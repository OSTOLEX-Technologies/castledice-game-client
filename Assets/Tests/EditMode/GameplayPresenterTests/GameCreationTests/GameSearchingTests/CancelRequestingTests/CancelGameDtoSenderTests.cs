using System;
using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.GameCreation.GameSearching.CancelRequesting;
using Src.NetworkingModule;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.CancelRequestingTests
{
    public class CancelGameDtoSenderTests
    {
        [Test]
        public void SendDto_ShouldSendMessage_WithReliableSendMode()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new CancelGameDtoSender(messageSenderMock.Object);
            
            sender.SendDto(new CancelGameDTO(new Random().Next().ToString()));
            
            Assert.AreEqual(MessageSendMode.Reliable, sentMessage.SendMode);
        }
        
        [Test]
        public void SendDto_ShouldSendMessage_WithCancelGameMessageType()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new CancelGameDtoSender(messageSenderMock.Object);
            
            sender.SendDto(new CancelGameDTO(new Random().Next().ToString()));

            var messageType = (ClientToServerMessageType)sentMessage.GetByte();
            Assert.AreEqual(ClientToServerMessageType.CancelGame, messageType);
        }
        
        [Test]
        public void SendDto_ShouldSendMessage_WithGivenDto()
        {
            var expectedDto = new CancelGameDTO(new Random().Next().ToString());
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new CancelGameDtoSender(messageSenderMock.Object);
            
            sender.SendDto(expectedDto);

            sentMessage.GetByte();
            var sentDto = sentMessage.GetCancelGameDTO();
            Assert.AreEqual(expectedDto, sentDto);
        }
    }
}