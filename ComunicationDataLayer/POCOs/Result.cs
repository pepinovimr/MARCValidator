using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    /// <summary>
    /// Used for comunication between <see cref="DomainLayer"/> and <see cref="ApplicaitonLayer"/>
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Error"></param>
    /// <param name="SourceRecord"></param>
    /// <param name="DefaultOutput"></param>
    /// <param name="ConditionOutput"></param>
    /// <param name="AlternativeOutput"></param>
    public record Result(Severity Type, ValidationType Error, string SourceRecord = "",
        ValidationOutput? DefaultOutput = null,
        ValidationOutput? ConditionOutput = null,
        ValidationOutput? AlternativeOutput = null)
    {
        public static Result Success { get; } = new(Severity.Success, ValidationType.None);
    }

    public record ValidationOutput(string SourceField = "", string Expected = "", string Found = "")
    {
        public static ValidationOutput NoOutput { get; } = new("", "", "");
    }
}
