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
                Severity.Info => "Info: " + LocalizationService[result.Error.ToString()] + " " + result.DefaultOutput?.SourceField + " Expected: " + result.DefaultOutput?.Expected + " Found: " + result.DefaultOutput?.Found +" SourceRecord: "+ result.SourceRecord + " ConditionField: " + result.ConditionOutput?.SourceField + " ConditionFound: " + result.ConditionOutput?.Found + " ConditionExpected: " + result.ConditionOutput?.Expected + " AlternativeField: " + result.AlternativeOutput?.SourceField + " AlternativeFound: " + result.AlternativeOutput?.Found + " AlternativeExpected: " + result.AlternativeOutput?.Expected,
                Severity.Warning => "Warning: " + LocalizationService[result.Error.ToString()] + " " + result.DefaultOutput?.SourceField + " Expected: " + result.DefaultOutput?.Expected + " Found: " + result.DefaultOutput?.Found + " SourceRecord: " + result.SourceRecord + " ConditionField: " + result.ConditionOutput?.SourceField + " ConditionFound: " + result.ConditionOutput?.Found + " ConditionExpected: " + result.ConditionOutput?.Expected + " AlternativeField: " + result.AlternativeOutput?.SourceField + " AlternativeFound: " + result.AlternativeOutput?.Found + " AlternativeExpected: " + result.AlternativeOutput?.Expected,
                Severity.Error => "Error: " + LocalizationService[result.Error.ToString()] + " " + result.DefaultOutput?.SourceField + " Expected: " + result.DefaultOutput?.Expected + " Found: " + result.DefaultOutput?.Found + " SourceRecord: " + result.SourceRecord + " ConditionField: " + result.ConditionOutput?.SourceField + " ConditionFound: " + result.ConditionOutput?.Found + " ConditionExpected: " + result.ConditionOutput?.Expected + " AlternativeField: " + result.AlternativeOutput?.SourceField + " AlternativeFound: " + result.AlternativeOutput?.Found + " AlternativeExpected: " + result.AlternativeOutput?.Expected,
                _ => throw new NotImplementedException(),
            };
    }
}
