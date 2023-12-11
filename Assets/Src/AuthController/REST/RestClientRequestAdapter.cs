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
            var requestHelper = new RequestHelper
            {
                Method = RestRequestMethodNames.GetRequestMethodName(requestMethodType),
                Uri = uri,
                Params = requestParams
            };
            string pars = "{";
            foreach (var p in requestParams.Keys)
            {
                pars += p + ": " + requestParams[p] + "\n";
            }
            pars += "}";
            
            Debug.Log("Exchanging...\n Helper content: " + requestHelper.Method + " " + requestHelper.Uri + " " + pars);

            RestClient.Request(requestHelper).Then(
                response =>
                {
                    Debug.Log("Received response!");
                    var data = JsonUtility.FromJson<T>(response.Text);
                    tcs.SetResult(data);
                }).Catch(Debug.Log);
        }
    }
}