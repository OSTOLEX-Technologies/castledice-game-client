using System.Collections.Generic;
using System.Threading.Tasks;

namespace Src.AuthController.REST
{
    public interface IHttpClientRequestAdapter
    {
        public void Request<T>(RestRequestMethodType requestMethodType, string uri,
            Dictionary<string, string> requestParams, TaskCompletionSource<T> tcs);
    }
}