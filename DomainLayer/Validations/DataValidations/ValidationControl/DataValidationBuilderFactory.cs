using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.Validations;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    internal class DataValidationBuilderFactory(Record Record) : IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation) =>
            validation.GetType() switch
            {
                Type t when t == typeof(LeaderValidation) => new LeaderValidationBuilder(Record, validation),
                Type t when t == typeof(ControlFieldValidation) => new ControlFieldValidationBuilder(Record, validation),
                Type t when t == typeof(DataFieldValidation) => new DataFieldValidationBuilder(Record, validation),
                Type t when t == typeof(SubFieldValidation) => new SubFieldValidationBuilder(Record, validation),
                _ => throw new ArgumentException("Unexpected type of Validation")
            };
    }
}
