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
            : new Result(ResultType.Error, ValidationErrorType.FileWrongFormat, string.Join(",",AllowedFileFormatMapping.Map.Values), extension);
    }
}
