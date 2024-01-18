using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations
{
    internal interface IDataValidation
    {
        public IEnumerable<Result> ValidateData();
    }
}
