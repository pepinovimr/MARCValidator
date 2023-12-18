using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;
namespace ApplicationLayer.Validations
{
    internal class PathExtensionValidation(string ExpectedFileExtension) : IUserInputValidation
    {
        public Result Validate(string input) => 
            new FileInfo(input).Extension is var extension && 
            extension.Equals(NormalizeFileExtension(ExpectedFileExtension)) 
            ? Result.Success
            : new Result(ResultType.Error, ValidationErrorType.FileWrongFormat, ExpectedFileExtension, extension);

        private string NormalizeFileExtension(string fileExtension) => 
            fileExtension[0].Equals('.') 
            ? fileExtension 
            : fileExtension.Insert(0, "."); 
    }
}
