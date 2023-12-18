using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    public record Result(ResultType Type, ValidationErrorType Error, string Expected = "", string Found = "", string Source = "")
    {
        public static Result Success { get; } = new(ResultType.Success, ValidationErrorType.None);
    }
}
