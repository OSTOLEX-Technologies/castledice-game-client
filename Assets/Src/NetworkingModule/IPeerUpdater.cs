using Riptide;

namespace Src.NetworkingModule
{
    public interface IPeerUpdater
    {
        public void SetPeer(Peer peer);

        public void StartUpdating();

        public void StopUpdating();
    }
}