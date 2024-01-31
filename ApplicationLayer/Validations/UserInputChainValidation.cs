using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    /// <summary>
    /// Used to validate a sequence of <see cref="IUserInputValidation"/>
    /// </summary>
    /// <param name="Validations"></param>
    internal class UserInputChainValidation(params IUserInputValidation[] Validations) : IUserInputValidation
    {
        /// <summary>
        /// Validates a sequence of <see cref="IUserInputValidation"/>
        /// </summary>
        /// <returns><see cref="Result.Success"/> or a <see cref="Result"/> of first failed <see cref="IUserInputValidation"/></returns>
        public Result Validate(string input) => 
            Validations.Select(validation => validation.Validate(input))
            .FirstOrDefault(result => result.Type == Severity.Error)
            ?? Result.Success;
    }
}
