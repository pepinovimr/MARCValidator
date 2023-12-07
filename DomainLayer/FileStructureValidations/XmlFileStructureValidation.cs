using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;

namespace DomainLayer.FileStructureValidations
{
    internal class XMLFileStructureValidation : IFileStructureValidation
    {
        public List<Result> ValidateFileStructure(string filePath)
        {
            if (IsXmlFile(filePath))
            {
                return [new Result(ResultType.Error, ValidationErrorType.FileWrongFormat, "xml")];
            }



            return [new Result(ResultType.Success, ValidationErrorType.None, string.Empty)];
        }

        public bool IsXmlFile(string filePath) => Path.GetExtension(filePath).Equals("xml");

        public bool IsValidByXsd(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
