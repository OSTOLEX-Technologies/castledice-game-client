using System.Net;

namespace Src.AuthController.REST.PortListener.ListenerContextResponse
{
    public interface IHttpListenerContextResponse
    {
        public void SendResponse(HttpListenerContext context, string responseHtml);
    }
}