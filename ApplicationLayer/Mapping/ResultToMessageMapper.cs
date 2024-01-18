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
        

        private static MessageType MapToMessageType(this Severity resultType) =>
            resultType switch
            {
                Severity.Success => MessageType.Success,
                Severity.Info => MessageType.Info,
                Severity.Warning => MessageType.Warning,
                Severity.Error => MessageType.Error,
                _ => MessageType.Normal,
            };

        private static string MapToText(this Result result) =>
            result.Type switch
            {
                Severity.Success => LocalizationService["SuccessResult"],
                Severity.Info => "Info: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                Severity.Warning => "Warning: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                Severity.Error => "Error: " + LocalizationService[result.Error.ToString()] + " " + result.Source + " Expected: " + result.Expected + " Found: " + result.Found,
                _ => throw new NotImplementedException(),
            };
    }
}
