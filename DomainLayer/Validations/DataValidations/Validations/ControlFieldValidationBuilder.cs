using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Interfaces;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Data validation builder for <see cref="ControlFieldValidation"/>
    /// </summary>
    internal class ControlFieldValidationBuilder : BaseDataValidationBuilder
    {
        private readonly ControlFieldValidation _controlFieldValidation;
        private readonly List<IControlField?> _fields;

        public ControlFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _controlFieldValidation = rules as ControlFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _fields = Record.GetControlFields().Where(x => x.Tag.Equals(_controlFieldValidation.ControlField.Tag.ToString("000"))).ToList();
        }

        public override string GetSourceFieldName() =>
            "ControlField Tag: " + _controlFieldValidation.ControlField.Tag;

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_fields.FirstOrDefault());
            _controlFieldValidation.ValidationResults.Add(result);
            return this;
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            foreach ( var field in _fields )
            {
                _controlFieldValidation.ValidationResults.Add(PatternValidation(_controlFieldValidation, field?.Data));
            }
            return this;
        }

        public override IDataValidationBuilder ValidateMaxCount()
        {
            if(CountValidation(_fields.Count) is var result && result is not null)
                _controlFieldValidation.ValidationResults.Add(result);
            return this;
        }
    }
}