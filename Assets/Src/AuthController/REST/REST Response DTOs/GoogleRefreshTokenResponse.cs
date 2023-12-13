﻿using System;
using Newtonsoft.Json;

namespace Src.AuthController.REST.REST_Response_DTOs
{
    [Serializable]
    public class GoogleRefreshTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public float ExpiresInSeconds { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; private set; }

        [JsonProperty("token_type")]
        public string TokenType { get; private set; }
    }
}