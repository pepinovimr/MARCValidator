using ComunicationDataLayer.Enums;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Helpers
{
    /// <summary>
    /// Helper class for getting data out of <see cref="Record"/>
    /// </summary>
    internal static class RecordMetadataHelper
    {
        /// <summary>
        /// Gets name of <see cref="Record"/>
        /// </summary>
        /// <returns>Name of record from 015$a or Control number $001</returns>
        public static string GetName(this Record record) =>
            record.GetDataFields().FirstOrDefault(x => x.Tag.Equals("015"))?.GetSubfield('a').Data ?? record.GetControlNumber();

        /// <summary>
        /// Gets description standard from <see cref="Record"/>
        /// </summary>
        /// <returns><see cref="AllowedDescriptionStandard"/> by 040$e or Leader[18]</returns>
        public static AllowedDescriptionStandard GetDescriptionStandard(this Record record) =>
            (record.GetDataFields()?.FirstOrDefault(x => x.Tag.Equals("040"))?.GetSubfield('e')?.Data?.Equals("rda") ?? false)
            ? AllowedDescriptionStandard.rda
            : record.Leader.Marshal()[18].Equals('a')
                ? AllowedDescriptionStandard.aacr2
                : AllowedDescriptionStandard.unidentified;

        /// <summary>
        /// Gets <see cref="IDataField"/> from <see cref="Record"/> by tag and optionally indicators
        /// </summary>
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
