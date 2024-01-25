using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.Validations;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    public class DataValidationBuilderFactory() : IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation, Record record) =>
            validation.GetType() switch
            {
                Type t when t == typeof(LeaderValidation) => new LeaderValidationBuilder(record, validation),
                Type t when t == typeof(ControlFieldValidation) => new ControlFieldValidationBuilder(record, validation),
                Type t when t == typeof(DataFieldValidation) => new DataFieldValidationBuilder(record, validation),
                Type t when t == typeof(SubFieldValidation) => new SubFieldValidationBuilder(record, validation),
                _ => throw new ArgumentException("Unexpected type of Validation")
            };
    }
}
