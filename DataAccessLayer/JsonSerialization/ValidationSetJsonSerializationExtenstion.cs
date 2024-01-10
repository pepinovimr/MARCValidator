using DataAccessLayer.MarcValidationStructure;
using Newtonsoft.Json;

namespace DataAccessLayer.JsonSerialization
{
    internal static class ValidationSetJsonSerializationExtenstion
    {
        public static string ToJson(this ValidationSet objectToSerialize) =>
            JsonConvert.SerializeObject(objectToSerialize, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        public static ValidationSet ToValidationSet(this string jsonString) =>
            JsonConvert.DeserializeObject<ValidationSet>(jsonString, new JsonSerializerSettings
            {
                Converters = { new ValidationBaseJsonConverter() }
            }) ?? throw new JsonSerializationException("Deserialization returned null");
    }
}
