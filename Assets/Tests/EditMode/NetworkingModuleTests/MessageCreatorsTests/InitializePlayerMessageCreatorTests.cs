using System;
using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Moq;
using NUnit.Framework;
using Riptide;
using Src.NetworkingModule.DTOCreators;
using Src.NetworkingModule.MessageCreators;

namespace Tests.EditMode.NetworkingModuleTests.MessageCreatorsTests
{
    public class InitializePlayerMessageCreatorTests
    {
        [Test]
        public async Task GetMessageAsync_ShouldReturnMessage_WithInitializePlayerDTO_FromCreator()
        {
            var dtoCreatorMock = new Mock<IInitializePlayerDtoCreator>();
            var expectedDTO = new InitializePlayerDTO(new Random().Next().ToString());
            dtoCreatorMock.Setup(creator => creator.CreateAsync()).ReturnsAsync(expectedDTO);
            var messageCreator = new InitializePlayerMessageCreatorBuilder
            {
                DtoCreator = dtoCreatorMock.Object
            }.Build();
            
            var actualMessage = await messageCreator.GetMessageAsync();
            actualMessage.GetByte();
            var actualDTO = actualMessage.GetInitializePlayerDTO();
            
            Assert.AreEqual(expectedDTO, actualDTO);
        }

        [Test]
        public async Task GetMessageAsync_ShouldReturnMessage_WithMessageReliableMessageSendMode()
        {
            var messageCreator = new InitializePlayerMessageCreatorBuilder().Build();
            
            var actualMessage = await messageCreator.GetMessageAsync();
            var actualSendMode = actualMessage.SendMode;
            
            Assert.AreEqual(MessageSendMode.Reliable, actualSendMode);
        }

        [Test]
        public async Task GetMessageAsync_ShouldReturnMessage_WithInitializePlayerMessageType()
        {
            var messageCreator = new InitializePlayerMessageCreatorBuilder().Build();
            
            var actualMessage = await messageCreator.GetMessageAsync();
            var actualMessageType = (ClientToServerMessageType)actualMessage.GetByte();
            
            Assert.AreEqual(ClientToServerMessageType.InitializePlayer, actualMessageType);
        }

        private class InitializePlayerMessageCreatorBuilder
        {
            public IInitializePlayerDtoCreator DtoCreator { get; set; }
            
            public InitializePlayerMessageCreatorBuilder()
            {
                var dtoCreatorMock = new Mock<IInitializePlayerDtoCreator>();
                dtoCreatorMock.Setup(creator => creator.CreateAsync()).ReturnsAsync(new InitializePlayerDTO(new Random().Next().ToString()));
                DtoCreator = dtoCreatorMock.Object;
            }
            
            public InitializePlayerMessageCreator Build()
            {
                return new InitializePlayerMessageCreator(DtoCreator);
            }
        }
    }
}