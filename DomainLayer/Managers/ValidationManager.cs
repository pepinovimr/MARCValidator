using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.ValidationControl;
using DomainLayer.Validations.FileStructureValidations;

namespace DomainLayer.Managers
{
    public class ValidationManager(IDataValidationDirector dataValidationDirector) : IValidationManager
    {
        private readonly IDataValidationDirector _dataValidationDirector = dataValidationDirector;

        public List<Result> Validate(string path) =>
            PerformStructureValidations(path) is var result &&
            result.DefaultIfEmpty(null) is not null ? PerformDataValidations(path) : result;

        private List<Result> PerformStructureValidations(string path) =>
            new FileStructureValidationFactory().CreateFileStructureValidation(path).ValidateFileStructure();

        private List<Result> PerformDataValidations(string path) =>
            _dataValidationDirector.ValidateRecords(path);
    }
}
