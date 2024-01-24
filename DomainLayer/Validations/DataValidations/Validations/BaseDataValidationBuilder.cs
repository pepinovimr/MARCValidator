using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.ValidationControl;
using MARC4J.Net.MARC;
using System.Data;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal abstract class BaseDataValidationBuilder(Record marcRecord, ValidationBase rules) : IDataValidationBuilder
    {
        protected Record Record { get; } = marcRecord;
        private ValidationBase Rules { get; } = rules;
        protected List<Result> Results = [];

        protected Dictionary<FieldObligationScope, Severity> ObligationToSeverityMap = new()
        {
            {FieldObligationScope.M, Severity.Error },
            {FieldObligationScope.MA, Severity.Error },
            {FieldObligationScope.R, Severity.Warning },
            {FieldObligationScope.RA, Severity.Warning },
            {FieldObligationScope.O, Severity.Info },
            {FieldObligationScope.FORBIDDEN, Severity.Error }
        };

        public virtual IDataValidationBuilder ValidateConditions()
        {
            if (Rules.Conditions is null) return this;

            DataValidationBuilderFactory factory = new(Record);

            foreach (var rule in Rules.Conditions)
            {
                IEnumerable<Result> results = ValidateRule(rule, factory);

                Results.AddRange(results.Select(MapConditionResult));
            }

            return this;
        }

        public virtual IDataValidationBuilder ValidateAlternatives()
        {
            if (Rules.Alternatives is null) return this;

            Predicate<Result> matchAlternative = x =>
                x.SourceField.Equals(GetSourceField()) &&
                x.Expected.Equals(Rules.Pattern) &&
                x.Found.Equals(GetSourceFieldValue());

            if (Results.FindLast(matchAlternative) is null)
                return this;

            DataValidationBuilderFactory factory = new(Record);

            List<Result> alternativeResults = [];

            foreach (var rule in Rules.Alternatives)
            {
                IEnumerable<Result> results = ValidateRule(rule, factory);

                if (!results.Any())
                {
                    Results.RemoveAll(matchAlternative);
                    return this;
                }
                else
                    alternativeResults.AddRange(results.Select(MapConditionResult));
            }
            Results.AddRange(alternativeResults.Select(MapAlternativeResult));

            return this;
        }

        private IEnumerable<Result> ValidateRule(ValidationBase rule, DataValidationBuilderFactory factory)
        {
            IDataValidationBuilder builder = factory.CreateValidations(rule);
            builder
                .ValidateObligation()
                .ValidatePattern()
                .ValidateConditions()
                .ValidateAlternatives();

            return builder.GetResults();
        }

        protected Result ValidateByFieldObligationScope(IVariableField? field) =>
            Rules.Obligation == FieldObligationScope.FORBIDDEN
                ? ValidateForbidden(field)
                : ValidateObligated(field);

        protected Result ValidateByFieldObligationScope(ISubfield? field) =>
            Rules.Obligation == FieldObligationScope.FORBIDDEN
                ? ValidateForbidden(field)
                : ValidateObligated(field);

        private Result ValidateForbidden(object? field) =>
            field is not null
            ? new Result(ObligationToSeverityMap[Rules.Obligation], ValidationType.ForbidenFieldExistsError)
            : Result.Success;

        private Result ValidateObligated(object? field) =>
            field is null
            ? new Result(ObligationToSeverityMap[Rules.Obligation], ValidationType.ObligatedFieldNotExists)
            : Result.Success;

        private Result MapConditionResult(Result resultToMap) =>
            resultToMap with
            {
                ConditionSourceField = resultToMap.SourceField,
                SourceField = GetSourceField(),
                ConditionExpected = resultToMap.Expected,
                Expected = Rules.Pattern ?? "",
                ConditionFound = resultToMap.Found,
                Found = GetSourceFieldValue() ?? ""
            };

        private Result MapAlternativeResult(Result resultToMap) =>
            resultToMap with
            {
                AlternativeSourceField = resultToMap.SourceField,
                SourceField = GetSourceField(),
                AlternativeExpected = resultToMap.Expected,
                Expected = Rules.Pattern ?? "",
                AlternativeFound = resultToMap.Found,
                Found = GetSourceFieldValue() ?? ""
            };


        /// <summary>
        /// Ensures that pattern can be validated without null references
        /// </summary>
        protected bool CanValidatePattern() =>
            Results.Last() is var lastResult
            && lastResult is not null
            && Rules.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;

        protected Result PatternValidation(ValidationBase validation, string? value)
        {
            if (!CanValidatePattern())
                return Result.Success;

            if (!Regex.IsMatch(value, validation.Pattern))
                Results.Add(new Result(ObligationToSeverityMap[validation.Obligation],
                    ValidationType.FieldDoesNotMatchPattern, Expected: validation.Pattern, Found: value, SourceField: GetSourceField()));

            return Result.Success;
        }

        public IEnumerable<Result> GetResults() =>
            Results.Select(result => result with { SourceRecord = GetRecordName() });

        public string GetRecordName() =>
            Record.GetDataFields().First(x => x.Tag.Equals("015")).GetSubfield('a').Data ?? Record.GetControlNumber();

        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public abstract string GetSourceField();

        public abstract string? GetSourceFieldValue();
    }
}
