using ComunicationDataLayer.Enums;

namespace DataAccessLayer.MarcReading.RawReading
{
    public static class MarcReaderFactory
    {
        public static IMarcReader CreateMarcReader(string filePath) =>
        Path.GetExtension(filePath) switch
        {
            var extension when extension == AllowedFileFormatMapping.Map[AllowedFileFormat.xml] => new MarcXmlReader(filePath),
            _ => throw new NotSupportedException("This file extension is not supported")
        };
    }
}
