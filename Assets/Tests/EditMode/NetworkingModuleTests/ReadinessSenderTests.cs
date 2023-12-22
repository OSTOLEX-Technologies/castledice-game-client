using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using NUnit.Framework;
using Src.NetworkingModule;
using Tests.Utils.Mocks;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class ReadinessSenderTests
    {
        [Test]
        [TestCase("someVerificationKey")]
        [TestCase("12345")]
        [TestCase("token1")]
        public void SendPlayerReadiness_ShouldSendMessage_WithPlayerReadyDTO(string verificationKey)
        {
            var messageSender = new TestMessageSender();
            var readinessSender = new ReadinessSender(messageSender);
            var expectedDTO = new PlayerReadyDTO(verificationKey);
            
            readinessSender.SendPlayerReadiness(verificationKey);
            var message = messageSender.SentMessage;
            message.GetByte();
            message.GetByte();
            var actualDTO = message.GetPlayerReadyDTO();
            
            Assert.AreEqual(expectedDTO, actualDTO);
        }
        
        [Test]
        public void SendPlayerReadiness_ShouldSendMessage_WithPlayerReadyMessageId()
        {
            var messageSender = new TestMessageSender();
            var readinessSender = new ReadinessSender(messageSender);
            
            readinessSender.SendPlayerReadiness("sometoken");
            var message = messageSender.SentMessage;
            var actualMessageId = message.GetUShort();
            
            Assert.AreEqual((ushort)ClientToServerMessageType.PlayerReady, actualMessageId);
        }
    }
}