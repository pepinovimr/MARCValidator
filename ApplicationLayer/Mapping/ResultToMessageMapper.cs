using ApplicationLayer.Services.Interfaces;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using System.Text;

namespace ApplicationLayer.Mapping
{
    public static class ResultToMessageMapper
    {
        public static ILocalizationService LocalizationService { set; get; }
        public static Message ToMessage(this Result result) =>
            new (result.MapToText(), result.Type.MapToMessageType());

        public static Dictionary<Message, List<Message>> ToMessages(this List<Result> results) =>
            results
                .GroupBy(x => x.SourceRecord)
                .ToDictionary(
                    sourceRecord => new Message(sourceRecord.Key, MessageType.Header),
                    validations => validations.Select(x => x.ToMessage()).ToList()
                );

        private static MessageType MapToMessageType(this Severity resultType) =>
            resultType switch
            {
                Severity.Success => MessageType.Success,
                Severity.Info => MessageType.Info,
                Severity.Warning => MessageType.Warning,
                Severity.Error => MessageType.Error,
                _ => MessageType.Normal,
            };

        private static string MapToText(this Result result) {
            if (result.Type == Severity.Success)
                return LocalizationService["SuccessResult"];

            StringBuilder message = new();
            message.Append(LocalizationService[result.Type.ToString()]);
            message.Append(": ");

            if (result.Error != ValidationType.None)
                message.Append($"{LocalizationService[result.Error.ToString()]} | ");

            message.Append(result.DefaultOutput.GetOutput());
            message.Append(result.ConditionOutput.GetOutput(LocalizationService["Where"] + " ", ": "));
            message.Append(result.AlternativeOutput.GetOutput(LocalizationService["Alternative"] + " ", ": "));

            return message.ToString();
        }

        private static string GetOutput(this ValidationOutput? output, string? prefix = null, string? suffix = null)
        {
            string outputString = string.Empty;

            if (output is null)
                return outputString;

            if (!string.IsNullOrEmpty(prefix))
                outputString += prefix;

            if (!string.IsNullOrEmpty(output.SourceField))
                outputString += $"{LocalizationService["Source"]}: {output.SourceField} ";

            if (!string.IsNullOrEmpty(output.Expected))
                outputString += $"{LocalizationService["Expected"]}: {output.Expected} ";

            if (!string.IsNullOrEmpty(output.Found))
                outputString += $"{LocalizationService["Found"]}: {output.Found} ";

            if (!string.IsNullOrEmpty(suffix))
                outputString += suffix;

            if (output.Equals(prefix + suffix))
                return string.Empty;

            return outputString;
        }
    }
}
