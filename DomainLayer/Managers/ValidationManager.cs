using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Interfaces;
using DomainLayer.Validations.FileStructureValidations;

namespace DomainLayer.Managers
{
    /// <summary>
    /// Manages sequencing of validations in <see cref="DomainLayer"/>
    /// </summary>
    /// <param name="dataValidationDirector"></param>
    public class ValidationManager(IDataValidationDirector dataValidationDirector, IFileStructureValidationFactory fileStructureValidationFactory) : IValidationManager
    {
        private readonly IDataValidationDirector _dataValidationDirector = dataValidationDirector;
        private readonly IFileStructureValidationFactory _fileStructureValidationFactory = fileStructureValidationFactory;

        /// <summary>
        /// Starts validation in sequence
        /// </summary>
        /// <returns>List of validation Results</returns>
        public List<Result> StartValidation(string path) =>
            PerformStructureValidations(path) is var result &&
            result.DefaultIfEmpty(null) is not null ? PerformDataValidations(path) : result;

        private List<Result> PerformStructureValidations(string path) =>
            _fileStructureValidationFactory.CreateFileStructureValidation(path).ValidateFileStructure();

        private List<Result> PerformDataValidations(string path) =>
            _dataValidationDirector.ValidateRecords(path);
    }
}
