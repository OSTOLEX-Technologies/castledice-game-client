using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto26;
using Src.AuthController.Exceptions;
using UnityEngine;

namespace Src.AuthController.REST
{
    public static class RestClientRequestAdapter
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
        
        public static void Request<T>(RestRequestMethodType requestMethodType, string uri, Dictionary<string, string> requestParams, TaskCompletionSource<T> tcs)
        {
            RestClient.Request(new RequestHelper
            {
                Method = GetRequestMethodName(requestMethodType),
                Uri = uri,
                Params = requestParams

            }).Then(
                response =>
                {
                    var data = JsonUtility.FromJson<T>(response.Text);
                    tcs.SetResult(data);
                }).Catch(Debug.Log);
        }
    }
}