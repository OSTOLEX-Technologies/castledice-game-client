using System;
using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.GameplayPresenter.GameCreation.GameSearching.GameRequesting;
using Src.NetworkingModule;

namespace Tests.EditMode.GameplayPresenterTests.GameCreationTests.GameSearchingTests.GameRequestingTests
{
    public class RequestGameDtoSenderTests
    {
        [Test]
        public void SendDto_ShouldSendMessage_WithReliableSendMode()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new RequestGameDtoSender(messageSenderMock.Object);

            sender.SendDto(new RequestGameDTO(new Random().Next().ToString()));
            
            Assert.AreEqual(MessageSendMode.Reliable, sentMessage.SendMode);
        }
        
        [Test]
        public void SendDto_ShouldSendMessage_WithRequestGameMessageType()
        {
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new RequestGameDtoSender(messageSenderMock.Object);
            
            sender.SendDto(new RequestGameDTO(new Random().Next().ToString()));

            var messageType = (ClientToServerMessageType)sentMessage.GetByte();
            Assert.AreEqual(ClientToServerMessageType.RequestGame, messageType);
        }

        [Test]
        public void SendDto_ShouldSendMessage_WithGivenDto()
        {
            var expectedDto = new RequestGameDTO(new Random().Next().ToString());
            var messageSenderMock = new Mock<IMessageSender>();
            Message sentMessage = null;
            messageSenderMock.Setup(sender => sender.Send(It.IsAny<Message>()))
                .Callback<Message>((message) => sentMessage = message);
            var sender = new RequestGameDtoSender(messageSenderMock.Object);
            
            sender.SendDto(expectedDto);

            sentMessage.GetByte();
            var sentDto = sentMessage.GetRequestGameDTO();
            Assert.AreEqual(expectedDto, sentDto);
        }
    }
}