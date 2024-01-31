using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    /// <summary>
    /// Handles Validation of FileExistence
    /// </summary>
    internal class FileExistsValidation : IUserInputValidation
    {
        /// <summary>
        /// Validates whether file exists.
        /// </summary>
        /// <param name="input">Path to file</param>
        /// <returns><see cref="Result"/> with <see cref="ValidationType.FileNotExist"/> or <see cref="Result.Success"/></returns>
        public Result Validate(string input) =>
            File.Exists(input)
            ? Result.Success
            : new Result(Severity.Error, ValidationType.FileNotExist, SourceRecord: input);
    }
}
