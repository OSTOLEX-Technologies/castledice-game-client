using System.Net;

namespace Src.Auth.REST.PortListener.ListenerContextInterpretation
{
    public interface IHttpListenerContextInterpreter
    {
        public string Get(HttpListenerContext context, string key);
        
        public bool Contains(HttpListenerContext context, string key);
    }
}