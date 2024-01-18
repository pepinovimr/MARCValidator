using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal class DataValidationBuilderFactory(Record Record) : IDataValidationBuilderFactory
    {
        public IDataValidationBuilder CreateValidations(ValidationBase validation) =>
            validation.GetType() switch
            {
                Type t when t == typeof(LeaderValidation) => new LeaderValidationBuilder(Record, validation),
                Type t when t == typeof(ControlFieldValidation) => new ControlFieldValidationBuilder(Record),
                Type t when t == typeof(DataFieldValidation) => new DataFieldValidationBuilder(Record),
                Type t when t == typeof(SubFieldValidation) => new SubFieldValidationBuilder(Record),
                _ => throw new ArgumentException("Unexpected type of Validation")
            };
    }
}
