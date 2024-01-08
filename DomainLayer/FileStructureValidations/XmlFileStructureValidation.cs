using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DomainLayer.FileStructureValidations
{
    internal class XmlFileStructureValidation : IFileStructureValidation
    {
        private readonly string XML_SCHEMA_File = @".\Properties\MarcXmlSchema.xsd";
        private readonly XmlSchemaSet _xmlSchemaSet = new();
        private XDocument _xDocument;
        private readonly string _filePath;
        public XmlFileStructureValidation(string filePath)
        {
            _xmlSchemaSet.Add("http://www.loc.gov/MARC21/slim", XML_SCHEMA_File);
            _filePath = filePath;
        }

        public Result ValidateFileStructure() =>
            ValidateStructure() is var result & result == Result.Success ? ValidateByXsd() : result;

        private Result ValidateByXsd()
        {
            Result result;
            _xDocument.Validate(_xmlSchemaSet, (o, e) =>
            {
                result = new Result(e.Severity == XmlSeverityType.Warning ? ResultType.Warning : ResultType.Error, 
                                    ValidationErrorType.XsdValidationError, e.Message);
            });
            result = Result.Success;
            return result;
        }

        //Not ideal, but we know that the file exists and we know that it is .xml,
        //so failure when loading means there is something wrong with Mandatory xml elements (root element, etc...)
        private Result ValidateStructure()
        {
            try
            {
                _xDocument = XDocument.Load(_filePath);
            }catch (XmlException e){
                return new Result(ResultType.Error, ValidationErrorType.FileStructureError, e.Message);
            }
            return Result.Success;
        }
    }
}
