using System.Threading.Tasks;
using castledice_events_logic.ClientToServer;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOCreators;

namespace Src.NetworkingModule.MessageCreators
{
    public class InitializePlayerMessageCreator : IAsyncMessageCreator
    {
        private readonly IAsyncInitializePlayerDTOCreator _dtoCreator;

        public InitializePlayerMessageCreator(IAsyncInitializePlayerDTOCreator dtoCreator)
        {
            _dtoCreator = dtoCreator;
        }

        public async Task<Message> GetMessageAsync()
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientToServerMessageType.InitializePlayer);    
            var dto = await _dtoCreator.GetDTOAsync();
            message.AddInitializePlayerDTO(dto);
            return message;
        }

    }
}