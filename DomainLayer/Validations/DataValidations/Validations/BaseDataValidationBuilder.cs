﻿using ComunicationDataLayer.Enums;
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

        public virtual IDataValidationBuilder ValidateConditions()
        {
            if (_validationBase.Conditions is null) return this;

            DataValidationBuilderFactory factory = new();

            List<Result> conditionResults = new ChainValidation(factory).PerformChainValidation(Record, _validationBase.Conditions);

            _validationBase.ValidationResults.AddRange(conditionResults.Where(x => x != Result.Success).Select(MapConditionResult));

            return this;
        }

        public virtual IDataValidationBuilder ValidateAlternatives()
        {
            if (_validationBase.Alternatives is null) return this;

            DataValidationBuilderFactory factory = new();

            List<Result> alternativeResults = new ChainValidation(factory).PerformChainValidation(Record, _validationBase.Alternatives);

            if (alternativeResults.Any(x => x.Type == Severity.Success || x == Result.Success))
                _validationBase.ValidationResults = [Result.Success];
            else
                _validationBase.ValidationResults.AddRange(alternativeResults.Select(MapAlternativeResult));
            return this;
        }

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
            return _validationBase.ValidationResults;
        }

        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public abstract string GetSourceFieldName();

        public abstract string? GetSourceFieldValue();
    }
}
