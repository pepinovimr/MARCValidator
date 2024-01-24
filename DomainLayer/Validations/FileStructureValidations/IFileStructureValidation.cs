using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.FileStructureValidations
{
    public interface IFileStructureValidation
    {
        public List<Result> ValidateFileStructure();
    }
}
