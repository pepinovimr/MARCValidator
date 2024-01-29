using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    public interface IDataValidationDirector
    {
        public List<Result> ValidateRecords(string path);
    }
}