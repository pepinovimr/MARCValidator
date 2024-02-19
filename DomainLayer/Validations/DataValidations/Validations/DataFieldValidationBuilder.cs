using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Interfaces;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Data validation builder for <see cref="DataFieldValidation"/>
    /// </summary>
    internal class DataFieldValidationBuilder : BaseDataValidationBuilder
    {
        private readonly DataFieldValidation _dataFieldValidation;
        private readonly List<IDataField> _fields;
        public DataFieldValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _dataFieldValidation = rules as DataFieldValidation ?? throw new NullReferenceException("Validation base cannot be null");

            _fields = Record.GetDataFields(_dataFieldValidation.DataField.Tag.ToString(), 
                                        _dataFieldValidation.DataField.Identificator1, 
                                        _dataFieldValidation.DataField.Identificator2);
        }
        public override string GetSourceFieldName() =>
            $"DataField Tag: {_dataFieldValidation.DataField.Tag} ind1: {_dataFieldValidation.DataField.Identificator1} ind2: {_dataFieldValidation.DataField.Identificator2}";

        public override IDataValidationBuilder ValidateObligation()
        {
            var result = ValidateByFieldObligationScope(_fields.FirstOrDefault());
            _dataFieldValidation.ValidationResults.Add(result);

            return this;
        }

        public override IDataValidationBuilder ValidatePattern() => this;

        public override IDataValidationBuilder ValidateMaxCount()
        {
            if (CountValidation(_fields.Count) is var result && result is not null)
                _dataFieldValidation.ValidationResults.Add(result);
            return this;
        }
    }
}