using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    public record Result(Severity Type, ValidationType Error, string Expected = "", string Found = "", string Source = "")
    {
        public static Result Success { get; } = new(Severity.Success, ValidationType.None);
    }
}
