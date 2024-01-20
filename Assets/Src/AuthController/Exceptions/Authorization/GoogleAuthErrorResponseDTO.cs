using System;
using System.Text.Json.Serialization;

namespace Src.AuthController.Exceptions.Authorization
{
    [Serializable]
    public class GoogleAuthErrorResponseDto
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("error_description")]
        public string Description { get; set; }
    }
}