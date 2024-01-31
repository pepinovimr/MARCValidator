using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Data validation builder for <see cref="DataFieldValidation"/>
    /// </summary>
    internal class DataFieldValidationBuilder : BaseDataValidationBuilder
    {
        private readonly DataFieldValidation _dataFieldValidation;
        private readonly IDataField? _field;
        public DataFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _dataFieldValidation = rules as DataFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _field = Record.GetDataField(_dataFieldValidation.DataField.Tag.ToString(), 
                                        _dataFieldValidation.DataField.Identificator1?[0], 
                                        _dataFieldValidation.DataField.Identificator2?[0]);
        }
        public override string GetSourceFieldName() =>
            $"DataField Tag: {_dataFieldValidation.DataField.Tag} ind1: {_dataFieldValidation.DataField.Identificator1} ind2: {_dataFieldValidation.DataField.Identificator2}";

        public override string? GetSourceFieldValue() => null;

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_field);
            _dataFieldValidation.ValidationResults.Add(result with
            {
                DefaultOutput =
                    new(SourceField: GetSourceFieldName(), Expected: result.DefaultOutput?.Expected ?? "", Found: result.DefaultOutput?.Found ?? "")
            }
            );

            return this;
        }

        public override IDataValidationBuilder ValidatePattern() => this;
    }
}