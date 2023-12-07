using ComunicationDataLayer.Enums;

namespace ComunicationDataLayer.POCOs
{
    public record Result(ResultType Type, ValidationErrorType Error, string Expected);
}
