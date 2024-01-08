using ApplicationLayer.Services.Interfaces;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace ApplicationLayer.Mapping
{
    public static class ResultToMessageMapper
    {
        public static ILocalizationService LocalizationService { set; get; }
        public static Message ToMessage(this Result result) =>
            new (result.MapToText(), result.Type.MapToMessageType());
        

        private static MessageType MapToMessageType(this ResultType resultType) =>
            resultType switch
            {
                ResultType.Success => MessageType.Success,
                ResultType.Info => MessageType.Info,
                ResultType.Warning => MessageType.Warning,
                ResultType.Error => MessageType.Error,
                _ => MessageType.Normal,
            };

        private static string MapToText(this Result result) =>
            result.Type switch
            {
                ResultType.Success => LocalizationService["SuccessResult"],
                ResultType.Info => "Info: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                ResultType.Warning => "Warning: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                ResultType.Error => "Error: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                _ => throw new NotImplementedException(),
            };
    }
}
