using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories.JsonConverters
{
    /// <summary>
    /// Custom converter for serialization of classes inheriting <see cref="ValidationBase"/>
    /// </summary>
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
                SubFieldValidation subField = jsonObject.ToObject<SubFieldValidation>(serializer);

                if (jsonObject["Parrent"] != null)
                {
                    DataField parrentDataField = jsonObject["Parrent"]?.ToObject<DataField>(serializer);
                    subField.SubField.Parrent = parrentDataField;
                }

                return subField;
            }

            throw new JsonSerializationException("Unknown type of ValidationBase");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
