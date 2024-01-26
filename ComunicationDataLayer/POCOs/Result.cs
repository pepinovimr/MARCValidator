using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
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
