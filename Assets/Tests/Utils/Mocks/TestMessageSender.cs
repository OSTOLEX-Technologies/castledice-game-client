using Riptide;
using Src.NetworkingModule;

namespace Tests.Mocks
{
    public class TestMessageSender : IMessageSender
    {
        public Message SentMessage { get; private set; }
        
        public void Send(Message message)
        {
            SentMessage = message;
        }
    }
}