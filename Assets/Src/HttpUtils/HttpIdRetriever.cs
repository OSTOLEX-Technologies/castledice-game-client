using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Src.Auth.REST;

namespace Src.HttpUtils
{
    public class HttpIdRetriever
    {
        private readonly string _authServiceUrl;
        private readonly IHttpClientRequestAdapter _messageSender;

        public HttpIdRetriever(string authServiceUrl, IHttpClientRequestAdapter messageSender)
        {
            _authServiceUrl = authServiceUrl;
            _messageSender = messageSender;
        }

        public async Task<int> RetrievePlayerIdAsync(string playerToken)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, _authServiceUrl);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", playerToken);
            using var response = await _messageSender.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return GetIdFromJson(responseBody);
        }

        private static int GetIdFromJson(string json)
        {
            var data = JObject.Parse(json);
            if (data.TryGetValue("id", out var idToken))
            {
                return idToken.Value<int>();
            }

            throw new InvalidOperationException("Response body does not contain id field");
        }
    }
}