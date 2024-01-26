using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.ValidationControl;
using MARC4J.Net.MARC;
using System.Data;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal abstract class BaseDataValidationBuilder(Record marcRecord, ValidationBase rules) : IDataValidationBuilder
    {
        protected ValidationSource ValidationSource { get; set; } = ValidationSource.Normal;
        protected Record Record { get; } = marcRecord;
        private ValidationBase Rules { get; } = rules;
        protected List<Result> Results = [];

        public virtual IDataValidationBuilder ValidateConditions()
        {
            if (Rules.Conditions is null) return this;

            DataValidationBuilderFactory factory = new();

            foreach (var rule in Rules.Conditions)
            {
                List<Result> results = ValidateRule(rule, factory, ValidationSource.Condition);

                Results.AddRange(results.Select(MapConditionResult));
            }

            return this;
        }

        public virtual IDataValidationBuilder ValidateAlternatives()
        {
            if (Rules.Alternatives is null) return this;

            Predicate<Result> matchAlternative = x => 
                x.DefaultOutput.SourceField.Equals(GetSourceFieldName()) &&
                x.DefaultOutput.Expected.Equals(Rules.Pattern) &&
                x.DefaultOutput.Found.Equals(GetSourceFieldValue());

            if (Results.FindLast(matchAlternative) is null)
                return this;

            DataValidationBuilderFactory factory = new();

            List<Result> alternativeResults = [];

            foreach (var rule in Rules.Alternatives)
            {
                List<Result> results = ValidateRule(rule, factory, ValidationSource.Alternative);

                if (!results.Any())
                {
                    Results.RemoveAll(matchAlternative);
                    return this;
                }
                else
                    alternativeResults.AddRange(results.Select(MapAlternativeResult));
            }
            Results.AddRange(alternativeResults.Select(MapAlternativeResult));

            return this;
        }

        private List<Result> ValidateRule(ValidationBase rule, DataValidationBuilderFactory factory, ValidationSource validationSource)
        {
            IDataValidationBuilder builder = factory.CreateValidations(rule, Record);
            builder
                .SetValidationSource(validationSource)
                .ValidateObligation()
                .ValidatePattern()
                .ValidateConditions()
                .ValidateAlternatives();

            return builder.GetResults();
        }

        protected Result ValidateByFieldObligationScope(object? field) =>
            Rules.Obligation == FieldObligationScope.FORBIDDEN
                ? field is not null 
                    ? new Result(PatternValidationHelper.ObligationToSeverityMap[Rules.Obligation], ValidationType.ForbidenFieldExistsError) 
                    : Result.Success
                : field is null 
                    ? new Result(PatternValidationHelper.ObligationToSeverityMap[Rules.Obligation], ValidationType.ObligatedFieldNotExists) 
                    : Result.Success;

        private Result MapConditionResult(Result resultToMap) =>
            resultToMap with
            {
                ConditionOutput = resultToMap.DefaultOutput,
                DefaultOutput = new(SourceField: GetSourceFieldName(), 
                                    Expected:  Rules.PatternErrorMessage ?? Rules.Pattern ?? "", 
                                    Found: GetSourceFieldValue() ?? "")
            };

        private Result MapAlternativeResult(Result resultToMap) =>
            resultToMap with
            {
                AlternativeOutput = resultToMap.DefaultOutput,
                DefaultOutput = new(SourceField: GetSourceFieldName(),
                                    Expected: Rules.PatternErrorMessage ?? Rules.Pattern ?? "",
                                    Found: GetSourceFieldValue() ?? "")
            };


        /// <summary>
        /// Ensures that pattern can be validated without null references
        /// </summary>
        protected bool CanValidatePattern() =>
            Results.LastOrDefault() is var lastResult
            && lastResult is not null
            && Rules.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;

        protected Result PatternValidation(ValidationBase validation, string? value)
        {
            if (!PatternValidationHelper.CanValidatePattern(Results, validation))
                return Result.Success;

            if (!Regex.IsMatch(value, validation.Pattern))
                Results.Add(new Result(PatternValidationHelper.ObligationToSeverityMap[validation.Obligation],
                    ValidationType.FieldDoesNotMatchPattern, 
                        DefaultOutput: new( Expected: validation.PatternErrorMessage ?? validation.Pattern, Found: value, SourceField: GetSourceFieldName())));

            return Result.Success;
        }

        public IDataValidationBuilder SetValidationSource(ValidationSource validationSource) 
        {
            ValidationSource = validationSource;
            return this;
        }

        public List<Result> GetResults() =>
            Results.Select(result => result with { SourceRecord = Record.GetName() }).ToList();

        public string GetRecordName() =>
            Record.GetDataFields().FirstOrDefault(x => x.Tag.Equals("015"))?.GetSubfield('a').Data ?? Record.GetControlNumber();

        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public abstract string GetSourceFieldName();

        public abstract string? GetSourceFieldValue();
    }
}
