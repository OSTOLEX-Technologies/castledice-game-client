using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Src.AuthController.REST
{
    public class RestClientRequestAdapter : IRestClientRequestAdapter
    {
        private static HttpClient _httpClient;

        public RestClientRequestAdapter()
        {
            _httpClient ??= new HttpClient();
        }
        
        public async void Request<T>(RestRequestMethodType requestMethodType, string uri,
            Dictionary<string, string> requestParams, TaskCompletionSource<T> tcs)
        {
            var encodedParams = new FormUrlEncodedContent(requestParams);
            var request = new HttpRequestMessage(RestRequestMethodNames.GetRequestMethodName(requestMethodType), uri);
            request.Content = encodedParams;

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK) {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(responseContent);
                tcs.SetResult(data);
            }
        }
    }
}