using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations
{
    public interface IDataValidationBuilder
    {
        public IDataValidationBuilder ValidateObligation();
        public IDataValidationBuilder ValidatePattern();
        public IDataValidationBuilder ValidateConditions();
        public IEnumerable<Result> GetResults();
    }
}