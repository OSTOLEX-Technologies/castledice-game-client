using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Src.Auth.REST.PortListener.ListenerContextResponse
{
    public class HttpListenerContextResponse : IHttpListenerContextResponse
    {
        public async Task SendResponse(HttpListenerContext context, string responseHtml)
        {
            var buffer = Encoding.UTF8.GetBytes(responseHtml);

            context.Response.ContentLength64 = buffer.Length;
            var responseStream = context.Response.OutputStream;
            await responseStream.WriteAsync(buffer, 0, buffer.Length);
            
            responseStream.Close();

            context.Response.Close();
        }
    }
}