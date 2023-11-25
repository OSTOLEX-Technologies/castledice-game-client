using casltedice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using NUnit.Framework;
using Src.NetworkingModule;
using Tests.Utils.Mocks;

namespace Tests.EditMode.NetworkingModuleTests
{
    public class PlayerInitializerTests
    {
        [TestCase("token1")]
        [TestCase("someToken")]
        [TestCase("12345")]
        public void InitializePlayer_ShouldSendMessage_WithInitializePlayerDTO(string playerToken)
        {
            var messageSender = new TestMessageSender();
            var playerInitializer = new PlayerInitializer(messageSender);
            var expectedDTO = new InitializePlayerDTO(playerToken);
            
            playerInitializer.InitializePlayer(playerToken);
            var message = messageSender.SentMessage;
            message.GetByte();
            message.GetByte();
            var actualDTO = message.GetInitializePlayerDTO();
            
            Assert.AreEqual(expectedDTO, actualDTO);
        }

        [Test]
        public void InitializePlayer_ShouldSendMessage_WithInitializePlayerMessageId()
        {
            var messageSender = new TestMessageSender();
            var playerInitializer = new PlayerInitializer(messageSender);
            
            playerInitializer.InitializePlayer("sometoken");
            var message = messageSender.SentMessage;
            var actualMessageId = message.GetUShort();
            
            Assert.AreEqual((ushort)ClientToServerMessageType.InitializePlayer, actualMessageId);
        }
    }
}