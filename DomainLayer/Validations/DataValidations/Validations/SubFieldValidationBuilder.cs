using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class SubFieldValidationBuilder : BaseDataValidationBuilder
    {
        private SubFieldValidation _subFieldValidation;
        private ISubfield? _field;
        public SubFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _subFieldValidation = rules as SubFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _field = Record.GetDataFields().Where(x =>
                x.Tag.Equals(_subFieldValidation.SubField.Parrent.Tag.ToString("000"))
                && (x.Indicator1.Equals(_subFieldValidation.SubField.Parrent.Identificator1 ?? " ") || x.Indicator1.Equals(_subFieldValidation.SubField.Parrent.Identificator1 ?? "#"))
                && (x.Indicator2.Equals(_subFieldValidation.SubField.Parrent.Identificator2 ?? " ") || x.Indicator2.Equals(_subFieldValidation.SubField.Parrent.Identificator2 ?? "#")))
                .FirstOrDefault()?.GetSubfield(_subFieldValidation.SubField.Code[0]);
        }
        public override string GetSourceField() =>
            $"SubField Code: {_field?.Code} [DataField Tag: {_subFieldValidation.SubField.Parrent.Tag} ind1: {_subFieldValidation.SubField.Parrent.Identificator1} ind2: {_subFieldValidation.SubField.Parrent.Identificator2}]";

        public override string? GetSourceFieldValue() =>
            _field?.Data;

        public override IDataValidationBuilder ValidateObligation()
        {
            if (ValidateByFieldObligationScope(_field) is var result && result != Result.Success)
                Results.Add(result with { SourceField = GetSourceField() });

            return this;
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            if (PatternValidation(_subFieldValidation, _field?.Data) is var result && result != Result.Success)
                Results.Add(result);
            return this;
        }
    }
}