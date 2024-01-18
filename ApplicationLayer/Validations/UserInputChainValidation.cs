using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    internal class UserInputChainValidation(params IUserInputValidation[] Validations) : IUserInputValidation
    {
        public Result Validate(string input) => 
            Validations.Select(validation => validation.Validate(input))
            .FirstOrDefault(result => result.Type == Severity.Error)
            ?? Result.Success;
    }
}
