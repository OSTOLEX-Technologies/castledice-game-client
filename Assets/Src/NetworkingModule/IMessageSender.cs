using Riptide;

namespace Src.NetworkingModule
{
    public interface IMessageSender
    {
        public void Send(Message message);
    }
}