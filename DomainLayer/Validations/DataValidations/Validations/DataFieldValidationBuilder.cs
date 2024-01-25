using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class DataFieldValidationBuilder : BaseDataValidationBuilder
    {
        private DataFieldValidation _dataFieldValidation;
        private IDataField? _field;
        public DataFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _dataFieldValidation = rules as DataFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _field = Record.GetDataFields().Where(x =>
                x.Tag.Equals(_dataFieldValidation.DataField.Tag.ToString("000"))
                && (x.Indicator1.Equals(_dataFieldValidation.DataField.Identificator1 ?? " ") || x.Indicator1.Equals(_dataFieldValidation.DataField.Identificator1 ?? "#"))
                && (x.Indicator2.Equals(_dataFieldValidation.DataField.Identificator2 ?? " ") || x.Indicator2.Equals(_dataFieldValidation.DataField.Identificator2 ?? "#")))
                .FirstOrDefault();
        }
        public override string GetSourceField() =>
            $"DataField Tag: {_field?.Tag} ind1: {_field?.Indicator1} ind2: {_field?.Indicator2}";

        public override string? GetSourceFieldValue() => null;

        public override IDataValidationBuilder ValidateObligation()
        {
            if (ValidateByFieldObligationScope(_field) is var result && result != Result.Success)
                Results.Add(result with { SourceField = GetSourceField() });

            return this;
        }

        public override IDataValidationBuilder ValidatePattern() => this;
    }
}