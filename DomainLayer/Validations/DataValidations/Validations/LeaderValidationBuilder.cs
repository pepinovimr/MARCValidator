using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal class LeaderValidationBuilder : BaseDataValidationBuilder
    {
        private LeaderValidation _leaderFieldValidation;
        private string _leaderValue;

        public LeaderValidationBuilder(Record marcRecord, ValidationBase rules) : base(marcRecord, rules)
        {
            _leaderFieldValidation = rules as LeaderValidation ?? throw new NullReferenceException("Validation base cannot be null");
            _leaderValue = marcRecord.Leader.Marshal();
        }


        public override string GetSourceFieldName() =>
            "Leader";

        public override string? GetSourceFieldValue() =>
            _leaderValue;

        public override IDataValidationBuilder ValidateObligation() => this;

        public override IDataValidationBuilder ValidatePattern()
        {
            if (PatternValidation(_leaderFieldValidation, _leaderValue) is var result && result != Result.Success)
                Results.Add(result);
            return this;
        }
    }
}