﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Src.Auth.Exceptions.HttpRequests;

namespace Src.Auth.REST
{
    public class HttpClientRequestAdapter : IHttpClientRequestAdapter
    {
        private static HttpClient _httpClient;

        public HttpClientRequestAdapter()
        {
            _httpClient ??= new HttpClient();
        }

        public async Task<T> Request<T>(HttpMethod requestMethodType, string uri,
            IEnumerable<KeyValuePair<string, string>> requestParams)
        {
            var encodedParams = new FormUrlEncodedContent(requestParams);
            var request = new HttpRequestMessage(requestMethodType, uri);
            request.Content = encodedParams;

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(responseContent);
                return data;
            }

            throw new HttpClientRequestException(await response.Content.ReadAsStringAsync(), response.StatusCode);
        }
    }
}