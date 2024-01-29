using ComunicationDataLayer.Enums;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Helpers
{
    internal static class RecordMetadataHelper
    {
        public static string GetName(this Record record) =>
            record.GetDataFields().FirstOrDefault(x => x.Tag.Equals("015"))?.GetSubfield('a').Data ?? record.GetControlNumber();

        public static AllowedDescriptionStandard GetDescriptionStandard(this Record record) =>
            (record.GetDataFields()?.FirstOrDefault(x => x.Tag.Equals("040"))?.GetSubfield('e').Data.Equals("rda") ?? false)
            ? AllowedDescriptionStandard.rda
            : record.Leader.Marshal()[18].Equals('a')
                ? AllowedDescriptionStandard.aacr2
                : AllowedDescriptionStandard.unidentified;

        public static IDataField? GetDataField(this Record record, string tag, char? ind1, char? ind2) =>
            record.GetDataFields().Where(x =>
                x.Tag.Equals(tag.PadLeft(3, '0'))
                    && x.Indicator1.AreIndicatorsEqual(ind1)
                    && x.Indicator2.AreIndicatorsEqual(ind2))
                .FirstOrDefault();

        private static bool AreIndicatorsEqual(this char sourceIndicator, char? comparisonIndicator) =>
            sourceIndicator.Equals(comparisonIndicator ?? ' ') 
            || sourceIndicator.Equals(comparisonIndicator ?? '#');


    }
}
