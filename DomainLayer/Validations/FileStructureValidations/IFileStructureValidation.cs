using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.FileStructureValidations
{
    /// <summary>
    /// Interface for validating file structures
    /// </summary>
    public interface IFileStructureValidation
    {
        /// <summary>
        /// Validates structure of a file
        /// </summary>
        public List<Result> ValidateFileStructure();
    }
}
