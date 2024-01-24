using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.FileStructureValidations;
namespace DomainLayer.Managers
{
    public class ValidationManager(string Path)
    {
        public IEnumerable<Result> Validate() =>
            PerformStructureValidations() is var result &&
            result.DefaultIfEmpty(null) is not null ? PerformDataValidations() : result ;

        private List<Result> PerformStructureValidations() => 
            new FileStructureValidationFactory().CreateFileStructureValidation(Path).ValidateFileStructure();

        private IEnumerable<Result> PerformDataValidations() => 
            throw new NotImplementedException();
    }
}
