using DataAccessLayer.MarcValidationStructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DataAccessLayer.JsonSerialization
{
    public class ValidationBaseJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ValidationBase);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            if (jsonObject["Leader"] != null)
            {
                return jsonObject.ToObject<LeaderValidation>(serializer);
            }
            else if (jsonObject["ControlField"] != null)
            {
                return jsonObject.ToObject<ControlFieldValidation>(serializer);
            }
            else if (jsonObject["DataField"] != null)
            {
                return jsonObject.ToObject<DataFieldValidation>(serializer);
            }
            else if (jsonObject["SubField"] != null)
            {
                return jsonObject.ToObject<SubFieldValidation>(serializer);
            }

            throw new JsonSerializationException("Unknown type of ValidationBase");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
