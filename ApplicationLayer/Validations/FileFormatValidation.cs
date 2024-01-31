using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;
namespace ApplicationLayer.Validations
{
    /// <summary>
    /// Handles Validation of file type
    /// </summary>
    internal class FileFormatValidation() : IUserInputValidation
    {
        /// <summary>
        /// Validates whether file has the correct file type
        /// </summary>
        /// <param name="input">Path to file</param>
        /// <returns><see cref="Result"/> with <see cref="ValidationType.FileWrongFormat"/> or <see cref="Result.Success"/></returns>
        public Result Validate(string input) => 
            new FileInfo(input).Extension is var extension && 
            AllowedFileFormatMapping.Map.ContainsValue(extension)
            ? Result.Success
            : new Result(Severity.Error, ValidationType.FileWrongFormat, DefaultOutput: new(Expected: string.Join(",",AllowedFileFormatMapping.Map.Values), Found: extension));
    }
}
