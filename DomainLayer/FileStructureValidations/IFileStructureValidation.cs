using ComunicationDataLayer.POCOs;

namespace DomainLayer.FileStructureValidations
{
    public interface IFileStructureValidation
    {
        public List<Result> ValidateFileStructure(string filePath);
    }
}
