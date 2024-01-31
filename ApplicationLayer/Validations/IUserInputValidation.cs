using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    /// <summary>
    /// Interface for validation of User input
    /// </summary>
    internal interface IUserInputValidation
    {
        /// <summary>
        /// Validation method 
        /// </summary>
        /// <param name="input">User input, ussualy a path to file</param>
        /// <returns>Either a <see cref="Result.Success"/> or a <see cref="Result"/> with correct <see cref="ComunicationDataLayer.Enums.ValidationType"/></returns>
        public Result Validate(string input);
    }
}
