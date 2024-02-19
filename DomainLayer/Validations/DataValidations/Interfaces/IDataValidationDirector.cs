using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Interfaces
{
    /// <summary>
    /// Interface for creating validations
    /// </summary>
    public interface IDataValidationDirector
    {
        /// <summary>
        /// Creates validation for every record on specified path
        /// </summary>
        public List<Result> ValidateRecords(string path);
    }
}