using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Extensions;
using MARC4J.Net.MARC;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations
{
    internal class LeaderValidationBuilder(Record Record, ValidationBase Rules) : DataValidationBuilder(Record, Rules)
    {
        private string _leaderValue = Record.Leader.Marshal();

        public override IDataValidationBuilder ValidateObligation() => this;

        public override IDataValidationBuilder ValidatePattern()
        {
            if (Rules.Pattern is not null && !Regex.IsMatch(_leaderValue, Rules.Pattern))
            {
                Results.Add(new Result(Rules.Obligation.ToResultType(), ValidationType.DataPatternError, Rules.Pattern, _leaderValue, "Leader"));
            }

            return this;
        }
    }
}