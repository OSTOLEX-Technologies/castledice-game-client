using System.Linq;
using System.Net;

namespace Src.Auth.REST.PortListener.ListenerContextInterpretation
{
    public class HttpListenerContextInterpreter : IHttpListenerContextInterpreter
    {
        public string Get(HttpListenerContext context, string key)
        {
            return context.Request.QueryString.Get(key);
        }

        public bool Contains(HttpListenerContext context, string key)
        {
            return context.Request.QueryString.AllKeys.Contains(key);
        }
    }
}