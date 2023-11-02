using casltedice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class MoveFromServerMessageHandler
    {
        private static IMoveFromServerDTOAccepter _DTOAccepter;
        
        public static void SetDTOAccepter(IMoveFromServerDTOAccepter accepter)
        {
            _DTOAccepter = accepter;
        }

        [MessageHandler((ushort)ServerToClientMessageType.MakeMove)]
        private static void HandleMoveFromServerMessage(Message message)
        {
            _DTOAccepter.AcceptMoveFromServerDTO(message.GetMoveFromServerDTO());
        }
    }
}