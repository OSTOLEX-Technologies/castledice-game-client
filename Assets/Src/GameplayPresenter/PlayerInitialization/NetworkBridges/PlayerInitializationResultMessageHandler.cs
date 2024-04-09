using castledice_events_logic.ServerToClient;
using castledice_riptide_dto_adapters.Extensions;
using Riptide;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public static class PlayerInitializationResultMessageHandler
    {
        private static PlayerInitializationResultDtoAccepter _dtoAccepter;
        
        public static void SetDtoAccepter(PlayerInitializationResultDtoAccepter dtoAccepter)
        {
            _dtoAccepter = dtoAccepter;
        }
        
        [MessageHandler((ushort)ServerToClientMessageType.InitializationResult)]
        public static void HandleMessage(Message message)
        {
            var dto = message.GetPlayerInitializationResultDTO();
            _dtoAccepter.AcceptDto(dto);
        }
    }
}