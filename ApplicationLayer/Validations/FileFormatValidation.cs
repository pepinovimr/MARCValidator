using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;
namespace ApplicationLayer.Validations
{
    internal class FileFormatValidation() : IUserInputValidation
    {
        public Result Validate(string input) => 
            new FileInfo(input).Extension is var extension && 
            AllowedFileFormatMapping.Map.ContainsValue(extension)
            ? Result.Success
            : new Result(Severity.Error, ValidationType.FileWrongFormat, Expected: string.Join(",",AllowedFileFormatMapping.Map.Values), Found: extension);
    }
}
