using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Interfaces;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Data validation builder for <see cref="LeaderValidation"/>
    /// </summary>
    internal class LeaderValidationBuilder : BaseDataValidationBuilder
    {
        private readonly LeaderValidation _leaderFieldValidation;
        private readonly string _leaderValue;

        public LeaderValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _leaderFieldValidation = rules as LeaderValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _leaderValue = marcRecord.Leader.Marshal();
        }


        public override string GetSourceFieldName() =>
            "Leader";

        public override IDataValidationBuilder ValidateObligation() => this;

        public override IDataValidationBuilder ValidatePattern()
        {
            _leaderFieldValidation.ValidationResults.Add(PatternValidation(_leaderFieldValidation, _leaderValue));
            return this;
        }

        public override IDataValidationBuilder ValidateMaxCount() => this;
    }
}