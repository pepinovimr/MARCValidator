using ComunicationDataLayer.Enums;

namespace DomainLayer.FileStructureValidations
{
    /// <summary>
    /// Factory for File structure validations
    /// </summary>
    public static class FileStructureValidationFactory
    {
        public static IFileStructureValidation CreateFileStructureValidation(string path) =>
            Path.GetExtension(path) switch
            {
                var extension when extension == AllowedFileFormatMapping.Map[AllowedFileFormat.xml] => new XmlFileStructureValidation(path),
                _ => throw new NotSupportedException("This file extension is not supported")
            };

    }
}
