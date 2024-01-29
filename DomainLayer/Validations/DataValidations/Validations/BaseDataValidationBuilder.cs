using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.ValidationControl;
using MARC4J.Net.MARC;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations.Validations
{
    internal abstract class BaseDataValidationBuilder(Record marcRecord, ValidationBase rules) : IDataValidationBuilder
    {
        protected ValidationSource ValidationSource { get; set; } = ValidationSource.Normal;
        protected Record Record { get; } = marcRecord;
        private ValidationBase _validationBase { get; } = rules;
        //protected List<Result> Results = [];
        //protected List<Result> AlternativeResults = [];
        //protected List<Result> ConditionResults = [];

        public virtual IDataValidationBuilder ValidateConditions()
        {
            if (_validationBase.Conditions is null) return this;

            DataValidationBuilderFactory factory = new();

            List<Result> conditionResults = new ChainValidation(factory).PerformChainValidation(Record, _validationBase.Conditions);

            _validationBase.ValidationResults.AddRange(conditionResults.Where(x => x != Result.Success).Select(MapConditionResult));

            //foreach (var rule in Rules.Conditions)
            //{
            //    List<Result> results = ValidateRule(rule, factory, ValidationSource.Condition);

            //    ConditionResults.AddRange(results.Where(x => x != Result.Success).Select(MapConditionResult));
            //}

            return this;
        }

        public virtual IDataValidationBuilder ValidateAlternatives()
        {
            if (_validationBase.Alternatives is null) return this;

            //Predicate<Result> matchAlternative = x => 
            //    x.DefaultOutput.SourceField.Equals(GetSourceFieldName()) &&
            //    x.DefaultOutput.Expected.Equals(Rules.Pattern) &&
            //    x.DefaultOutput.Found.Equals(GetSourceFieldValue());

            //if (Results.FindLast(matchAlternative) is null)
            //    return this;

            DataValidationBuilderFactory factory = new();

            List<Result> alternativeResults = new ChainValidation(factory).PerformChainValidation(Record, _validationBase.Alternatives);

            //foreach (var rule in Rules.Alternatives)
            //{
            //    List<Result> results = ValidateRule(rule, factory, ValidationSource.Alternative);

            //    if (results.Count == 0)
            //    {
            //        //Results.RemoveAll(matchAlternative);
            //        alternativeResults.Add(Result.Success);
            //        return this;
            //    }
            //    else
            //        alternativeResults.AddRange(results.Select(MapAlternativeResult));
            //}




            if (alternativeResults.Any(x => x.Type == Severity.Success || x == Result.Success))
                _validationBase.ValidationResults = [Result.Success];
            else
                _validationBase.ValidationResults.AddRange(alternativeResults.Select(MapAlternativeResult));                

            //Results.AddRange(alternativeResults.Select(x => x != Result.Success ? MapAlternativeResult(x) : x));
            //STIL RETURNS THINGS IT SHOULD !!! 072!
            return this;
        }

        //private List<Result> ValidateRule(ValidationBase rule, DataValidationBuilderFactory factory, ValidationSource validationSource)
        //{
        //    IDataValidationBuilder builder = factory.CreateValidations(rule, Record);
        //    builder
        //        .SetValidationSource(validationSource)
        //        .ValidateObligation()
        //        .ValidatePattern()
        //        .ValidateConditions()
        //        .ValidateAlternatives();

        //    return builder.GetResults() ?? [Result.Success];
        //}

        protected Result ValidateByFieldObligationScope(object? field) =>
            _validationBase.Obligation == FieldObligationScope.FORBIDDEN
                ? field is not null 
                    ? new Result(PatternValidationHelper.ObligationToSeverityMap[_validationBase.Obligation], ValidationType.ForbidenFieldExistsError) 
                    : Result.Success
                : field is null 
                    ? new Result(PatternValidationHelper.ObligationToSeverityMap[_validationBase.Obligation], ValidationType.ObligatedFieldNotExists) 
                    : Result.Success;

        private Result MapConditionResult(Result resultToMap) =>
            resultToMap with
            {
                ConditionOutput = new(SourceField: resultToMap.DefaultOutput?.SourceField ?? "",
                                    Expected: resultToMap.DefaultOutput?.Expected ?? "",
                                    Found: resultToMap.DefaultOutput?.Found ?? ""),
                DefaultOutput = new(SourceField: GetSourceFieldName(), 
                                    Expected:  _validationBase.PatternErrorMessage ?? _validationBase.Pattern ?? "", 
                                    Found: GetSourceFieldValue() ?? "")
            };

        private Result MapAlternativeResult(Result resultToMap) =>
            resultToMap with
            {
                AlternativeOutput = new(SourceField: resultToMap.DefaultOutput?.SourceField ?? "",
                                    Expected: resultToMap.DefaultOutput?.Expected ?? "",
                                    Found: resultToMap.DefaultOutput?.Found ?? ""),
                DefaultOutput = new(SourceField: GetSourceFieldName(),
                                    Expected: _validationBase.PatternErrorMessage ?? _validationBase.Pattern ?? "",
                                    Found: GetSourceFieldValue() ?? "")
            };


        /// <summary>
        /// Ensures that pattern can be validated without null references
        /// </summary>
        protected bool CanValidatePattern() =>
            _validationBase.ValidationResults.LastOrDefault() is var lastResult
            && lastResult is not null
            && _validationBase.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;

        protected Result PatternValidation(ValidationBase validation, string? value)
        {
            if (!PatternValidationHelper.CanValidatePattern(_validationBase.ValidationResults, validation))
                return Result.Success;

            if (!Regex.IsMatch(value, validation.Pattern))
                _validationBase.ValidationResults.Add(new Result(PatternValidationHelper.ObligationToSeverityMap[validation.Obligation],
                    ValidationType.FieldDoesNotMatchPattern, 
                        DefaultOutput: new( Expected: validation.PatternErrorMessage ?? validation.Pattern, Found: value, SourceField: GetSourceFieldName())));

            return Result.Success;
        }

        public IDataValidationBuilder SetValidationSource(ValidationSource validationSource) 
        {
            ValidationSource = validationSource;
            return this;
        }

        public List<Result>? GetResults() 
        {
            //if(AlternativeResults.Any(x => x == Result.Success) || AlternativeResults.Any(x => x.Type == Severity.Success))
            //    return null;

            //return _validationBase.ValidationResults.Count != 0 || _validationBase.ValidationResults.All(x => x == Result.Success || x.Type == Severity.Success) 
            //    ? _validationBase.ValidationResults.Select(result => result with { SourceRecord = Record.GetName() }).ToList() 
            //    : [Result.Success];
            return _validationBase.ValidationResults;
        }
        public string GetRecordName() =>
            Record.GetDataFields().FirstOrDefault(x => x.Tag.Equals("015"))?.GetSubfield('a').Data ?? Record.GetControlNumber();

        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public abstract string GetSourceFieldName();

        public abstract string? GetSourceFieldValue();
    }
}
