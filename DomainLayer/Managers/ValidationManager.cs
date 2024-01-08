using ComunicationDataLayer.POCOs;
using DomainLayer.FileStructureValidations;
namespace DomainLayer.Managers
{
    public class ValidationManager(string Path)
    {
        public Result Validate() =>
            FileStructureValidationFactory.CreateFileStructureValidation(Path).ValidateFileStructure();
    }
}
