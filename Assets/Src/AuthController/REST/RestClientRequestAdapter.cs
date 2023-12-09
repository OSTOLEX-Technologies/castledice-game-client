using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto26;
using UnityEngine;

namespace Src.AuthController.REST
{
    public class RestClientRequestAdapter : IRestClientRequestAdapter
    {
        public void Request<T>(RestRequestMethodType requestMethodType, string uri,
            Dictionary<string, string> requestParams, TaskCompletionSource<T> tcs)
        {
            RestClient.Request(new RequestHelper
            {
                Method = RestRequestMethodNames.GetRequestMethodName(requestMethodType),
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