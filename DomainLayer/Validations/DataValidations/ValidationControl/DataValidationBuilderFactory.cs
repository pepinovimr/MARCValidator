using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Interfaces;
using DomainLayer.Validations.DataValidations.Validations;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    /// <summary>
    /// Manages creating instances of classes inheriting <see cref="BaseDataValidationBuilder"/>
    /// </summary>
    public class DataValidationBuilderFactory() : IDataValidationBuilderFactory
    {
        /// <summary>
        /// Creates class inheriting <see cref="BaseDataValidationBuilder"/> based on type of <see cref="ValidationBase"/>
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
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
