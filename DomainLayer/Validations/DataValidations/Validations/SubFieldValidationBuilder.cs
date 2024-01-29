using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class SubFieldValidationBuilder : BaseDataValidationBuilder
    {
        private readonly SubFieldValidation _subFieldValidation;
        private readonly ISubfield? _field;
        public SubFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _subFieldValidation = rules as SubFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _field = Record.GetDataFields().Where(x =>
                x.Tag.Equals(_subFieldValidation.SubField.Parrent.Tag.ToString("000"))
                && (x.Indicator1.Equals(_subFieldValidation.SubField.Parrent.Identificator1?[0] ?? ' ') || x.Indicator1.Equals(_subFieldValidation.SubField.Parrent.Identificator1?[0] ?? '#'))
                && (x.Indicator2.Equals(_subFieldValidation.SubField.Parrent.Identificator2?[0] ?? ' ') || x.Indicator2.Equals(_subFieldValidation.SubField.Parrent.Identificator2?[0] ?? '#')))
                .FirstOrDefault()?.GetSubfield(_subFieldValidation.SubField.Code[0]);
        }
        public override string GetSourceFieldName() =>
            $"SubField Code: {_subFieldValidation.SubField.Code} Parrent: [DataField Tag: {_subFieldValidation.SubField.Parrent.Tag} ind1: {_subFieldValidation.SubField.Parrent.Identificator1} ind2: {_subFieldValidation.SubField.Parrent.Identificator2}]";

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