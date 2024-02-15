using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Helpers
{
    /// <summary>
    /// Helper class for Pattern Validation
    /// </summary>
    internal static class PatternValidationHelper
    {
        /// <summary>
        /// Map for converting from <see cref="FieldObligationScope"/> to <see cref="Severity"/>
        /// </summary>
        public static readonly Dictionary<FieldObligationScope, Severity> ObligationToSeverityMap = new()
        {
            {FieldObligationScope.M, Severity.Error },
            {FieldObligationScope.MA, Severity.Info },
            {FieldObligationScope.R, Severity.Warning },
            {FieldObligationScope.RA, Severity.Info },
            {FieldObligationScope.O, Severity.Info },
            {FieldObligationScope.FORBIDDEN, Severity.Error }
        };

        /// <summary>
        /// Determines whether Field can be validated by pattern validation without causing unexpected problems
        /// </summary>
        /// <param name="results"></param>
        /// <param name="validation"></param>
        /// <returns></returns>
        public static bool CanValidatePattern(List<Result> results, ValidationBase validation) =>
            results.LastOrDefault() is var lastResult
            && lastResult is not null
            && validation.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;
    }
}
