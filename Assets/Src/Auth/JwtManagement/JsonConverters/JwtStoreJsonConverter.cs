using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Src.Auth.JwtManagement.JsonConverters
{
    public class JwtStoreJsonConverter : JsonConverter<AbstractJwtStore>
    {
        public override void WriteJson(JsonWriter writer, AbstractJwtStore value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        [CanBeNull]
        public override AbstractJwtStore ReadJson(
            JsonReader reader, Type objectType, 
            AbstractJwtStore existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jsonObj = JToken.ReadFrom(reader);
            var type = jsonObj["Type"]!.ToObject<JwtStoreType>();
            return type switch
            {
                JwtStoreType.Standard => jsonObj.ToObject<JwtStore>(),
                JwtStoreType.Google => jsonObj.ToObject<GoogleJwtStore>(),
                _ => throw  new ArgumentException("JwtStoreType not found: " + type)
            };
        }
    }
}