using System.Net;
using System.Text;

namespace Src.AuthController.REST.PortListener.ListenerContextResponse
{
    public class HttpListenerContextResponse : IHttpListenerContextResponse
    {
        public void SendResponse(HttpListenerContext context, string responseHtml)
        {
            var buffer = Encoding.UTF8.GetBytes(responseHtml);

            context.Response.ContentLength64 = buffer.Length;
            var stream = context.Response.OutputStream;
            stream.Write(buffer, 0, buffer.Length);

            context.Response.Close(); 
        }
    }
}