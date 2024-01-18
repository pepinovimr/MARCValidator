using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.FileStructureValidations;
namespace DomainLayer.Managers
{
    public class ValidationManager(string Path)
    {
        public IEnumerable<Result> Validate() =>
            PerformStructureValidations() is var result &&
            result == Result.Success ? PerformDataValidations() : new List<Result> { result };

        private Result PerformStructureValidations() => 
            new FileStructureValidationFactory().CreateFileStructureValidation(Path).ValidateFileStructure();

        private IEnumerable<Result> PerformDataValidations() => 
            throw new NotImplementedException();
    }
}
