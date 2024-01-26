using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Infrastrucure
{
    internal interface IDataValidation
    {
        public List<Result> ValidateData();
    }
}
