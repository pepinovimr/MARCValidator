using ComunicationDataLayer.POCOs;
using ComunicationDataLayer.Enums;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DomainLayer.Validations.FileStructureValidations
{
    internal class XmlFileStructureValidation : IFileStructureValidation
    {
        private readonly string _xmlSchemaFile = Path.Combine(".", "Properties", "MarcXmlSchema.xsd");
        private readonly XmlSchemaSet _xmlSchemaSet = new();
        private XDocument _xDocument;
        private readonly string _filePath;
        public XmlFileStructureValidation(string filePath)
        {
            _xmlSchemaSet.Add("http://www.loc.gov/MARC21/slim", _xmlSchemaFile);
            _filePath = filePath;
        }

        public List<Result> ValidateFileStructure() =>
            ValidateStructure() is var result & result == Result.Success ? ValidateByXsd() : [result];

        private List<Result> ValidateByXsd()
        {
            List<Result> results = [];
            _xDocument.Validate(_xmlSchemaSet, (o, e) =>
            {
                results.Add(new Result(e.Severity == XmlSeverityType.Warning ? Severity.Warning : Severity.Error,
                                    ComunicationDataLayer.Enums.ValidationType.XsdValidationError, DefaultOutput: new(Found: e.Message)));
            });
            return results;
        }

        //Not ideal, but we know that the file exists and we know that it is .xml,
        //so failure when loading means there is something wrong with Mandatory xml elements (root element, etc...)
        private Result ValidateStructure()
        {
            try
            {
                _xDocument = XDocument.Load(_filePath);
            }
            catch (XmlException e)
            {
                return new Result(Severity.Error, ComunicationDataLayer.Enums.ValidationType.FileStructureError, SourceRecord: _filePath, DefaultOutput: new(Found: e.Message));
            }
            return Result.Success;
        }
    }
}
