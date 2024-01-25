using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class ControlFieldValidationBuilder : BaseDataValidationBuilder
    {
        private ControlFieldValidation _controlFieldValidation;
        private IControlField? _field;
        private ControlFieldValidation _rules;

        public ControlFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _controlFieldValidation = rules as ControlFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _field = Record.GetControlFields().Where(x => x.Tag.Equals(_controlFieldValidation.ControlField.Tag.ToString("000"))).FirstOrDefault();
            _rules = rules as ControlFieldValidation;
        }

        public override string GetSourceField() =>
            "ControlField Tag: " + _rules.ControlField.Tag;

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
            if (PatternValidation(_controlFieldValidation, _field?.Data) is var result && result != Result.Success)
                Results.Add(result);
            return this;
        }
    }
}