namespace DataAccessLayer.MarcReading.RawReading
{
    internal static class MarcAttributesExtensions
    {
        public static string ToStringOrWhitespace(this int? value) =>
            value?.ToString() ?? " ";

        public static string ToStringWithLeadingZeroes(this int value) =>
            value.ToString("000");
    }
}
