using ComunicationDataLayer.Enums;

namespace DataAccessLayer.MarcReading.RawReading
{
    /// <summary>
    /// Factory class for readers of MARC records in different formats
    /// </summary>
    public static class MarcReaderFactory
    {
        /// <summary>
        /// Cretes a IMarcReader class for specified file
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public static IMarcReader CreateMarcReader(string filePath) =>
        Path.GetExtension(filePath) switch
        {
            var extension when extension == AllowedFileFormatMapping.Map[AllowedFileFormat.xml] => new MarcXmlReader(filePath),
            _ => throw new NotSupportedException("This file extension is not supported")
        };
    }
}
