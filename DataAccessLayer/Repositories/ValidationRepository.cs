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
        public List<ValidationSet> GetValidations()
        {
            List<ValidationSet> validations = [];
            foreach (var file in GetFiles())
            {
                string jsonString = File.ReadAllText(Path.Combine(_propertiesPath, file));

                validations.Add(JsonConvert.DeserializeObject<ValidationSet>(jsonString, new JsonSerializerSettings
                    {
                        Converters = { new ValidationBaseJsonConverter(), new SubFieldConverter() }
                    }) ?? throw new JsonSerializationException("Deserialization returned null"));
            }

            return validations;
        }

        public List<string> GetFiles()
        {
            string jsonContent = File.ReadAllText(_fileListPath);
            JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDocument.RootElement;
            JsonElement filesArray = root.GetProperty("Files");
            List<string> files = [];

            foreach (JsonElement fileElement in filesArray.EnumerateArray())
            {
                files.Add(fileElement.GetProperty("File").GetString());
            }

            return files;
        }
    }
}
