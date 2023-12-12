using System.Net.Http;
using Src.AuthController.Exceptions;

namespace Src.AuthController.REST
{
    public static class RestRequestMethodNames
    {
        public static HttpMethod GetRequestMethodName(RestRequestMethodType type)
        {
            switch (type)
            {
                case RestRequestMethodType.Post: return HttpMethod.Post;
                case RestRequestMethodType.Get: return HttpMethod.Get;
                case RestRequestMethodType.Delete: return HttpMethod.Delete;
                default: throw new RestRequestMethodNameNotFoundException(type);
            }
        }
    }
}