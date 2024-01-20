﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Src.AuthController.Exceptions;
using Src.AuthController.Exceptions.Authorization;
using Src.AuthController.Exceptions.HttpRequests;
using UnityEngine;

namespace Src.AuthController.REST
{
    public class HttpClientRequestAdapter : IHttpClientRequestAdapter
    {
        private static HttpClient _httpClient;

        public HttpClientRequestAdapter()
        {
            _httpClient ??= new HttpClient();
        }

        public async void Request<T>(HttpMethod requestMethodType, string uri,
            IEnumerable<KeyValuePair<string, string>> requestParams, TaskCompletionSource<T> tcs)
        {
            var encodedParams = new FormUrlEncodedContent(requestParams);
            var request = new HttpRequestMessage(requestMethodType, uri);
            request.Content = encodedParams;

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                throw new HttpNetworkNotReachableException();
            }
            
            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(responseContent);
                tcs.SetResult(data);
            }
            else
            {
                Debug.Log(await response.Content.ReadAsStringAsync());
                throw new HttpClientRequestException(await response.Content.ReadAsStringAsync(), response.StatusCode);
            }
        }
    }
}