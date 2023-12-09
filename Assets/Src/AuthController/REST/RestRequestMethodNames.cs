using Src.AuthController.Exceptions;

namespace Src.AuthController.REST
{
    public static class RestRequestMethodNames
    {
        public static string GetRequestMethodName(RestRequestMethodType type)
        {
            switch (type)
            {
                case RestRequestMethodType.Post: return "POST";
                case RestRequestMethodType.Get: return "GET";
                case RestRequestMethodType.Delete: return "DELETE";
                default: throw new RestRequestMethodNameNotFoundException(type);
            }
        }
    }
}