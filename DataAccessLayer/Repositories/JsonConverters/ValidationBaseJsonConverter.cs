using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories.JsonConverters
{
    public class ValidationBaseJsonConverter : JsonConverter
    {
        //public override ValidationSet ReadJson(JsonReader reader, Type objectType, ValidationSet existingValue, bool hasExistingValue, JsonSerializer serializer)
        //{
        //    if (reader.TokenType == JsonToken.Null)
        //        return null;

        //    var jsonObject = JObject.Load(reader);

        //    var validationSet = new ValidationSet();
        //    validationSet.Name = jsonObject["Name"].Value<string>();

        //    var validationListArray = jsonObject["ValidationList"].Value<JArray>();
        //    validationSet.ValidationList = new List<ValidationBase>();

        //    foreach (var item in validationListArray)
        //    {
        //        var validationBase = CreateValidationBase(item, serializer);
        //        validationSet.ValidationList.Add(validationBase);
        //    }

        //    return validationSet;
        //}

        //private ValidationBase CreateValidationBase(JToken token, JsonSerializer serializer)
        //{
        //    var typeName = token["Type"].Value<string>();

        //    switch (typeName)
        //    {
        //        case "LeaderValidation":
        //            return token.ToObject<LeaderValidation>(serializer);

        //        case "ControlFieldValidation":
        //            return token.ToObject<ControlFieldValidation>(serializer);

        //        case "DataFieldValidation":
        //            return token.ToObject<DataFieldValidation>(serializer);

        //        case "SubFieldValidation":
        //            return token.ToObject<SubFieldValidation>(serializer);

        //        // Add other cases for different validation types

        //        default:
        //            throw new NotSupportedException($"Unknown validation type: {typeName}");
        //    }
        //}

        //public override void WriteJson(JsonWriter writer, ValidationSet value, JsonSerializer serializer)
        //{
        //    throw new NotImplementedException();
        //}
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
