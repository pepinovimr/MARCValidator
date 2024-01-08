namespace ComunicationDataLayer.Enums
{
    public static class AllowedFileFormatMapping
    {
        public readonly static Dictionary<AllowedFileFormat, string> Map = new()
        {
            { AllowedFileFormat.xml, ".xml" }
        };
    }
    public enum AllowedFileFormat
    {
        xml = 0,
    }
}
