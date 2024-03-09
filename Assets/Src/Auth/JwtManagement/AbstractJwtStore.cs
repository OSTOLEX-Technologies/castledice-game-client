using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Src.Auth.JwtManagement
{
    public abstract class AbstractJwtStore
    {
        [JsonProperty]
        [JsonPropertyName("type")]
        protected JwtStoreType Type;
    }
}