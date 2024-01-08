namespace DataAccessLayer.MarcReading
{
    internal static class MarcAttributesExtensions
    {
        public static string ToStringOrWhitespace(this int? value) =>
            value is null ? " " : value.ToString();

        public static string ToStringWithLeadingZeroes(this int value) =>
            value.ToString("000");
    }
}
