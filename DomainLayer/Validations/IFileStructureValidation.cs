using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations
{
    //Make factory for this -> More possible MARC filetypes -> each has to have a different structure validations
    //Now YAGNI
    public interface IFileStructureValidation
    {
        public List<Result> ValidateFileStructure(string FilePath);
    }
}
