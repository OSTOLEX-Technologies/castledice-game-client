using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Src.Auth.Exceptions.HttpRequests;
using UnityEngine;

namespace Src.Auth.REST
{
    public class HttpClientRequestAdapter : IHttpClientRequestAdapter
    {
        private static HttpClient _httpClient;

        public HttpClientRequestAdapter()
        {
            _httpClient ??= new HttpClient();
        }

        public async Task<T> Request<T>(
            HttpMethod requestMethodType, 
            string uri,
            IEnumerable<KeyValuePair<string, string>> requestParams)
        {
            var parametrizedUri = FormatUriWithParams(uri, requestParams);
            
            var request = new HttpRequestMessage(requestMethodType, parametrizedUri);

            return await SendMessage<T>(request);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await SendAsync(request);
        }

        public async Task<T> Request<T>(
            HttpMethod requestMethodType, 
            string uri,
            IEnumerable<KeyValuePair<string, string>> requestParams,
            IEnumerable<KeyValuePair<string, string>> requestBodyContent)
        {
            var parametrizedUri = FormatUriWithParams(uri, requestParams);
            
            var request = new HttpRequestMessage(requestMethodType, parametrizedUri);
            
            var encodedContent = new FormUrlEncodedContent(requestBodyContent);
            request.Content = encodedContent;

            return await SendMessage<T>(request);
        }

        private async Task<T> SendMessage<T>(HttpRequestMessage request)
        {
            Debug.LogWarning("Sending to: " + request.RequestUri);
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpClientRequestException(await response.Content.ReadAsStringAsync(), response.StatusCode);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.LogWarning("Response: " + responseContent);
            var data = JsonConvert.DeserializeObject<T>(responseContent);
            return data;
        }
        

        private string FormatUriWithParams(
            string rawUri, 
            IEnumerable<KeyValuePair<string, string>> uriParams)
        {
            var uriWithParams = $"{rawUri}?";
            var paramsList = uriParams.ToList();
            for (var i = 0; i < paramsList.Count(); i++)
            {
                uriWithParams += $"{paramsList[i].Key}={paramsList[i].Value}";
                if (i < paramsList.Count - 1)
                {
                    uriWithParams += "&";
                }
            }

            return uriWithParams;
        }
    }
}