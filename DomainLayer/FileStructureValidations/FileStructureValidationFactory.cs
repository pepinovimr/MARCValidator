namespace DomainLayer.FileStructureValidations
{
    /// <summary>
    /// Factory for File structure validations
    /// </summary>
    internal abstract class FileStructureValidationFactory
    {
        public abstract IFileStructureValidation CreateFileStructureValidation(string path);

    }
}
