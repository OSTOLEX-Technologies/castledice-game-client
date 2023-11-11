namespace Src.NetworkingModule
{
    public class ReadinessSender
    {
        private readonly IMessageSender _messageSender;
        
        public ReadinessSender(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void SendPlayerReadiness(string verificationKey)
        {
            
        }
    }
}