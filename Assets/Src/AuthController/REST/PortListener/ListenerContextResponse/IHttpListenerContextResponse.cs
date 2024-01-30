using System.Net;
using System.Threading.Tasks;

namespace Src.AuthController.REST.PortListener.ListenerContextResponse
{
    public interface IHttpListenerContextResponse
    {
        public Task SendResponse(HttpListenerContext context, string responseHtml);
    }
}