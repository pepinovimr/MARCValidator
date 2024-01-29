using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class ControlFieldValidationBuilder : BaseDataValidationBuilder
    {
        private ControlFieldValidation _controlFieldValidation;
        private IControlField? _field;

        public ControlFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _controlFieldValidation = rules as ControlFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _field = Record.GetControlFields().Where(x => x.Tag.Equals(_controlFieldValidation.ControlField.Tag.ToString("000"))).FirstOrDefault();
        }

        public override string GetSourceFieldName() =>
            "ControlField Tag: " + _controlFieldValidation.ControlField.Tag;

        public override string? GetSourceFieldValue() =>
            _field?.Data;

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_field);
            //if (ValidateByFieldObligationScope(_field) is var result && result != Result.Success)
                _controlFieldValidation.ValidationResults.Add(result with 
                    { DefaultOutput = 
                        new(SourceField: GetSourceFieldName(), Expected: result.DefaultOutput?.Expected ?? "", Found: result.DefaultOutput?.Found ?? "") 
                    }
                );

            return this;
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            //if (PatternValidation(_controlFieldValidation, _field?.Data) is var result && result != Result.Success)
                _controlFieldValidation.ValidationResults.Add(PatternValidation(_controlFieldValidation, _field?.Data));
            return this;
        }
    }
}