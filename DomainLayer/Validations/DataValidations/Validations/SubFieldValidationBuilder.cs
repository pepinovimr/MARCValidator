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
        private readonly List<ISubfield> _fields;
        public SubFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _subFieldValidation = rules as SubFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _fields = _fields = Record.GetDataFields(_subFieldValidation.SubField.Parent.Tag.ToString(),
                                        _subFieldValidation.SubField.Parent.Identificator1,
                                        _subFieldValidation.SubField.Parent.Identificator2)?
                                        .Select(x => x.GetSubfield(_subFieldValidation.SubField.Code[0])).ToList() ?? [];
        }
        public override string GetSourceFieldName() =>
            $"SubField Code: {_subFieldValidation.SubField.Code} Parent: [DataField Tag: {_subFieldValidation.SubField.Parent.Tag} ind1: {_subFieldValidation.SubField.Parent.Identificator1} ind2: {_subFieldValidation.SubField.Parent.Identificator2}]";

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_fields.FirstOrDefault());
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
            foreach (var field in _fields)
            {
                _subFieldValidation.ValidationResults.Add(PatternValidation(_subFieldValidation, field?.Data));
            }
            return this;
        }
        public override IDataValidationBuilder ValidateMaxCount()
        {
            if (CountValidation(_fields.Count) is var result && result is not null)
                _subFieldValidation.ValidationResults.Add(result);
            return this;
        }
    }
}