using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Infrastrucure
{
    public interface IDataValidationBuilder
    {
        public IDataValidationBuilder ValidateObligation();
        public IDataValidationBuilder ValidatePattern();
        public IDataValidationBuilder ValidateConditions();
        public IDataValidationBuilder ValidateAlternatives();
        public List<Result>? GetResults();
        public string GetSourceFieldName();

        public string? GetSourceFieldValue();
        public IDataValidationBuilder SetValidationSource(ValidationSource validationSource);
    }
}