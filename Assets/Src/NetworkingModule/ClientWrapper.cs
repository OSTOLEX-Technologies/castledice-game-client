using Riptide;

namespace Src.NetworkingModule
{
    public class ClientWrapper : IMessageSender
    {
        public Client Client { get; private set; }

        public ClientWrapper(Client client)
        {
            Client = client;
        }
         
        public void Send(Message message)
        {
            Client.Send(message);
        }
    }
}