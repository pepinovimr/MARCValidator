using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using System.Text.RegularExpressions;

namespace DomainLayer.Validations.DataValidations.Helpers
{
    internal static class PatternValidationHelper
    {
        public static readonly Dictionary<FieldObligationScope, Severity> ObligationToSeverityMap = new()
        {
            {FieldObligationScope.M, Severity.Error },
            {FieldObligationScope.MA, Severity.Error },
            {FieldObligationScope.R, Severity.Warning },
            {FieldObligationScope.RA, Severity.Warning },
            {FieldObligationScope.O, Severity.Info },
            {FieldObligationScope.FORBIDDEN, Severity.Error }
        };
        public static bool CanValidatePattern(List<Result> results, ValidationBase validation) =>
            results.LastOrDefault() is var lastResult
            && lastResult is not null
            && validation.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;
    }
}
