using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Infrastrucure
{
    internal interface IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation);
    }
}
