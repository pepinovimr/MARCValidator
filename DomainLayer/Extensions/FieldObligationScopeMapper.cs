using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace DomainLayer.Extensions
{
    internal static class FieldObligationScopeMapper
    {
        public static Severity ToResultType(this FieldObligationScope fieldObligationScope) =>
            fieldObligationScope switch
            {
                FieldObligationScope.M or FieldObligationScope.MA => Severity.Error,
                FieldObligationScope.R or FieldObligationScope.RA => Severity.Warning,
                FieldObligationScope.O => Severity.Info,
                _ => throw new ArgumentException($"Trying to map unsupported FieldObligationScope: {fieldObligationScope}")
            };
    }
}
