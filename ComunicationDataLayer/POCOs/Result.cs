using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    public record Result(Severity Type, ValidationType Error, string SourceRecord = "",
        string Expected = "", string Found = "", string SourceField = "",
        string ConditionSourceField = "", string ConditionExpected = "", string ConditionFound = "",
        string AlternativeSourceField = "", string AlternativeExpected = "", string AlternativeFound = "")
    {
        public static Result Success { get; } = new(Severity.Success, ValidationType.None);
    }
}
