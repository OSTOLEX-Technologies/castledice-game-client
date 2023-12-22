using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;
using Src.NetworkingModule.DTOAccepters;

namespace Src.NetworkingModule.MessageHandlers
{
    public static class GameCreationMessageHandler
    {
        private static IGameCreationDTOAccepter _dtoAccepter;

        public static void SetDTOAccepter(IGameCreationDTOAccepter accepter)
        {
            _dtoAccepter = accepter;
        }

        [MessageHandler((ushort) ServerToClientMessageType.CreateGame)]
        private static void HandleCreatGameMessage(Message message)
        {
            _dtoAccepter.AcceptCreateGameDTO(message.GetCreateGameDTO());
        }

        [MessageHandler((ushort) ServerToClientMessageType.CancelGame)]
        private static void HandleCancelGameMessage(Message message)
        {
            _dtoAccepter.AcceptCancelGameResultDTO(message.GetCancelGameResultDTO());
        }
    }
}