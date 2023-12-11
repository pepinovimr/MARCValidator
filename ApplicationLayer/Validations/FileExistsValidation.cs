using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    internal class FileExistsValidation : IUserInputValidation
    {
        public Result Validate(string input) =>
            File.Exists(input)
            ? Result.Success
            : new Result(ResultType.Error, ValidationErrorType.FileNotExist);
    }
}
