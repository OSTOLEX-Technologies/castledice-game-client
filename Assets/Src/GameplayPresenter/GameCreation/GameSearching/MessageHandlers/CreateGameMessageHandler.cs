using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.GameCreation.GameSearching.MessageHandlers
{
    public static class CreateGameMessageHandler
    {
        private static ICreateGameDtoAccepter _dtoAccepter;

        public static void SetDtoAccepter(ICreateGameDtoAccepter dtoAccepter)
        {
            _dtoAccepter = dtoAccepter;
        }

        [MessageHandler((ushort)ServerToClientMessageType.CreateGame)]
        private static void HandleMessage(Message message)
        {
            var dto = message.GetCreateGameDTO();
            _dtoAccepter.AcceptCreateGameDto(dto);
        }
    }
}