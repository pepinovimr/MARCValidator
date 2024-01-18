using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations
{
    internal interface IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation);
    }
}
