using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Data validation builder for <see cref="SubFieldValidation"/>
    /// </summary>
    internal class SubFieldValidationBuilder : BaseDataValidationBuilder
    {
        private readonly SubFieldValidation _subFieldValidation;
        private readonly ISubfield? _field;
        public SubFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _subFieldValidation = rules as SubFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _field = _field = Record.GetDataField(_subFieldValidation.SubField.Parent.Tag.ToString(),
                                        _subFieldValidation.SubField.Parent.Identificator1?[0],
                                        _subFieldValidation.SubField.Parent.Identificator2?[0])?.GetSubfield(_subFieldValidation.SubField.Code[0]);
        }
        public override string GetSourceFieldName() =>
            $"SubField Code: {_subFieldValidation.SubField.Code} Parent: [DataField Tag: {_subFieldValidation.SubField.Parent.Tag} ind1: {_subFieldValidation.SubField.Parent.Identificator1} ind2: {_subFieldValidation.SubField.Parent.Identificator2}]";

        public override string? GetSourceFieldValue() =>
            _field?.Data;

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_field);
            _subFieldValidation.ValidationResults.Add(result with
            {
                DefaultOutput =
                    new(SourceField: GetSourceFieldName(), Expected: result.DefaultOutput?.Expected ?? "", Found: result.DefaultOutput?.Found ?? "")
            }
            );

            return this;
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            _subFieldValidation.ValidationResults.Add(PatternValidation(_subFieldValidation, _field?.Data));
            return this;
        }
    }
}