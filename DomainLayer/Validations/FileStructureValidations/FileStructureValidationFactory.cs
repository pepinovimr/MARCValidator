using ComunicationDataLayer.Enums;

namespace DomainLayer.Validations.FileStructureValidations
{
    /// <summary>
    /// Factory for File structure validations
    /// </summary>
    public class FileStructureValidationFactory : IFileStructureValidationFactory
    {
        public IFileStructureValidation CreateFileStructureValidation(string path) =>
            Path.GetExtension(path) switch
            {
                var extension when extension == AllowedFileFormatMapping.Map[AllowedFileFormat.xml] => new XmlFileStructureValidation(path),
                _ => throw new NotSupportedException("This file extension is not supported")
            };

    }
}
