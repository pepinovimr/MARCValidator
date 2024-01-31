namespace ComunicationDataLayer.Enums
{
    /// <summary>
    /// Used form mapping of <see cref="AllowedFileFormat"/> to <see cref="string"/>
    /// </summary>
    public static class AllowedFileFormatMapping
    {
        /// <summary>
        /// Map for mapping of <see cref="AllowedFileFormat"/> to <see cref="string"/>
        /// </summary>
        public readonly static Dictionary<AllowedFileFormat, string> Map = new()
        {
            { AllowedFileFormat.xml, ".xml" }
        };
    }
    /// <summary>
    /// List of supported formats of MARC record
    /// </summary>
    public enum AllowedFileFormat
    {
        xml = 0,
    }
}
