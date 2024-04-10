using castledice_events_logic.ClientToServer;

namespace Src.GameplayPresenter.PlayerInitialization.NetworkBridges
{
    public interface IInitializePlayerDtoSender
    {
        public void SendDto(InitializePlayerDTO dto);
        public bool CanSend { get; }
    }
}