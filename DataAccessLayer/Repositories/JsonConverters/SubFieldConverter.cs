using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories.JsonConverters
{
    public class SubFieldConverter : JsonConverter<SubField>
    {
        public override SubField ReadJson(JsonReader reader, Type objectType, SubField existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            SubField subField = new()
            {
                Code = jsonObject["Code"]?.ToObject<string>(serializer)
            };

            if (jsonObject["Parrent"] != null)
            {
                subField.Parrent = jsonObject["Parrent"].ToObject<DataField>(serializer);
            }

            return subField;
        }

        public override void WriteJson(JsonWriter writer, SubField value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
