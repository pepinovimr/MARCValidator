using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Validations;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Infrastrucure
{
    public interface IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation, Record record);
    }
}
