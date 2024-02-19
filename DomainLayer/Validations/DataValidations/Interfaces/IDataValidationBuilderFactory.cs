using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Interfaces
{
    /// <summary>
    /// Interface for creating DataValidation classes
    /// </summary>
    public interface IDataValidationBuilderFactory
    {
        /// <summary>
        /// Creates ValidationBuilders base on field
        /// </summary>
        public IDataValidationBuilder CreateValidations(ValidationBase validation, Record record);
    }
}
