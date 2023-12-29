using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.NetworkingModule
{
    public class PlayerInitializer
    {
        private readonly IMessageSender _messageSender;

        public PlayerInitializer(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void InitializePlayer(string verificationKey)
        {
            var dto = new InitializePlayerDTO(verificationKey);
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientToServerMessageType.InitializePlayer);
            message.AddInitializePlayerDTO(dto);
            _messageSender.Send(message);
        }
    }
}