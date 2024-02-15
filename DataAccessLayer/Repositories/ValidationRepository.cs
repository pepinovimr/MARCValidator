using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DataAccessLayer.Repositories.JsonConverters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace DataAccessLayer.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private string _propertiesPath = Path.Combine(".", "Properties");
        private readonly string _fileListPath;
        private readonly ILogger _logger;
        public ValidationRepository(ILogger<ValidationRepository> logger)
        {
            _fileListPath = Path.Combine(_propertiesPath, "ValidationFiles.json");
            _logger = logger;
        }
        public List<ValidationSet> GetValidations(AllowedDescriptionStandard descriptionStandard)
        {
            List<ValidationSet> validations = [];
            foreach (var files in GetFilePath(descriptionStandard))
            {
                string jsonString = File.ReadAllText(files);

                validations.Add(JsonConvert.DeserializeObject<ValidationSet>(jsonString, new JsonSerializerSettings
                {
                    Converters = { new ValidationBaseJsonConverter(), }
                }) ?? throw new JsonSerializationException("Deserialization returned null"));
            }
            return validations;
        }

        private List<string> GetFilePath(AllowedDescriptionStandard descriptionStandard)
        {
            string jsonContent = File.ReadAllText(_fileListPath);
            JsonDocument jsonDocument = JsonDocument.Parse(jsonContent);
            JsonElement root = jsonDocument.RootElement;
            JsonElement filesArray = root.GetProperty("Files");
            List<string> filePaths = [];
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Tohle smazat!");
            Console.ForegroundColor = ConsoleColor.White;
            descriptionStandard = AllowedDescriptionStandard.rda;
            foreach (JsonElement fileElement in filesArray.EnumerateArray())
            {
                var path = fileElement.GetProperty("File").GetString();
                if (path is null)
                    _logger.LogError($"Validation file has no value");

                if (!path.ToLower().StartsWith(descriptionStandard.ToString().ToLower()))
                    continue;

                if (Path.Combine(_propertiesPath, path) is var fullPath && File.Exists(fullPath))
                    filePaths.Add(fullPath);
                else
                    _logger.LogError($"Validation file {path} does not exist");
            }

            return filePaths;
        }
    }
}
