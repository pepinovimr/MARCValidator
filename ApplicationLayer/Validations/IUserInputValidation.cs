using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Validations
{
    internal interface IUserInputValidation
    {
        public Result Validate(string input);
    }
}
