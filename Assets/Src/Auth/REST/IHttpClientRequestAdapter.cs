﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Src.Auth.REST
{
    public interface IHttpClientRequestAdapter
    {
        public Task<T> Request<T>(HttpMethod requestMethodType, string uri,
            IEnumerable<KeyValuePair<string, string>> requestParams);
    }
}