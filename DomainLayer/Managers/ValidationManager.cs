using ComunicationDataLayer.POCOs;
using DataAccessLayer.MarcReading.Serialization;
using DomainLayer.Validations.FileStructureValidations;
using MARC4J.Net.MARC;
namespace DomainLayer.Managers
{
    public class ValidationManager(string Path)
    {
        public Result Validate() =>
            new FileStructureValidationFactory().CreateFileStructureValidation(Path).ValidateFileStructure();

        public IEnumerable<Record> GetMarc() =>
            new MarcSerializer().Serialize(Path);

    }
}
