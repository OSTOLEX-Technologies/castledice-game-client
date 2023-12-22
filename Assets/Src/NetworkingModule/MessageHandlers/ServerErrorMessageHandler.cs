using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class ServerErrorMessageHandler
    {
        private static IServerErrorDTOAccepter _DTOAccepter;
        
        public static void SetAccepter(IServerErrorDTOAccepter accepter)
        {
            _DTOAccepter = accepter;
        }
        
        [MessageHandler((ushort) ServerToClientMessageType.Error)]
        private static void HandleServerErrorMessage(Message message)
        {
            _DTOAccepter.AcceptServerErrorDTO(message.GetServerErrorDTO());
        }
    }
}