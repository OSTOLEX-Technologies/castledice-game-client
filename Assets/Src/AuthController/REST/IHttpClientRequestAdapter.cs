using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Src.AuthController.REST
{
    public interface IHttpClientRequestAdapter
    {
        public void Request<T>(HttpMethod requestMethodType, string uri,
            Dictionary<string, string> requestParams, TaskCompletionSource<T> tcs);
    }
}