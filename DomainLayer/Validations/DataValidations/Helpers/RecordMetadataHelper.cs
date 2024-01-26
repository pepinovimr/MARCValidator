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

    }
}
