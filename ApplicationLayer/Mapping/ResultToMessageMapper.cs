using ApplicationLayer.Services.Interfaces;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using System.Text;

namespace ApplicationLayer.Mapping
{
    /// <summary>
    /// Handles conversion of <see cref="Result"/> to <see cref="Message"/>
    /// </summary>
    public static class ResultToMessageMapper
    {
        public static ILocalizationService LocalizationService { set; get; }

        /// <summary>
        /// Converts a single <see cref="Result"/> to <see cref="Message"/>
        /// </summary>
        public static Message ToMessage(this Result result) =>
            new (result.MapToText(), result.Type.MapToMessageType());

        /// <summary>
        /// Converts a <see cref="IEnumerable{Result}"/> to <see cref="List{Message}"/>
        /// </summary>
        public static Dictionary<Message, List<Message>> ToMessages(this IEnumerable<Result> results) =>
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

            message.Append(result.DefaultOutput.GetStringValue());
            message.Append(result.ConditionOutput.GetStringValue(LocalizationService["Where"] + " ", ": "));
            message.Append(result.AlternativeOutput.GetStringValue(LocalizationService["Alternative"] + " ", ": "));

            return message.ToString();
        }

        private static string GetStringValue(this ValidationOutput? output, string? prefix = null, string? suffix = null)
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
