using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations
{
    internal class ControlFieldValidationBuilder : DataValidationBuilder
    {
        private Record _record;
        private ControlFieldValidation _controlFieldValidation;
        private IControlField? _field;

        public ControlFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _record = marcRecord;
            _controlFieldValidation = rules as ControlFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _field = marcRecord.GetControlFields().Where(x => x.Tag.Equals(_controlFieldValidation.ControlField.Tag)).First();
        }

        public override IDataValidationBuilder ValidateObligation()
        {
            if (ValidateByFieldObligationScope(_field) is var result && result != Result.Success)
                Results.Add(result with { Source = "ControlField " + _controlFieldValidation.ControlField.Tag });

            return this;
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            if (!CanValidatePattern())
                return this;

            if (!Regex.IsMatch(_field.Data, _controlFieldValidation.Pattern))
                Results.Add(new Result(ObligationToSeverityMap[_controlFieldValidation.Obligation],
                    ValidationType.FieldDoesNotMatchPattern, _controlFieldValidation.Pattern, _field.Data, "ControlField"));

            return this;
        }
    }
}