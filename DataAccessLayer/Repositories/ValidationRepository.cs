using ComunicationDataLayer.POCOs;
using DataAccessLayer.Repositories.JsonConverters;
using Newtonsoft.Json;
using System.Text.Json;

namespace DataAccessLayer.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private string _propertiesPath = Path.Combine(".", "Properties");
        private readonly string _fileListPath;
        public ValidationRepository()
        {
            _fileListPath = Path.Combine(_propertiesPath, "ValidationFiles.json");
        }
        public IEnumerable<ValidationSet> GetValidations()
        {
            foreach (var file in GetFiles())
            {
                string jsonString = File.ReadAllText(Path.Combine(_propertiesPath, file));

                yield return JsonConvert.DeserializeObject<ValidationSet>(jsonString, new JsonSerializerSettings
                    {
                        Converters = { new ValidationBaseJsonConverter(), new SubFieldConverter() }
                    }) ?? throw new JsonSerializationException("Deserialization returned null");
            }
        }

        public IEnumerable<string> GetFiles()
        {

            string jsonContent = File.ReadAllText(_fileListPath);

            JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);

            JsonElement root = jsonDocument.RootElement;

            JsonElement filesArray = root.GetProperty("Files");

            foreach (JsonElement fileElement in filesArray.EnumerateArray())
            {
                yield return fileElement.GetProperty("File").GetString();
            }
        }
    }
}
