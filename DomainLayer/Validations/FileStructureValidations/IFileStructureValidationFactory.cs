namespace DomainLayer.Validations.FileStructureValidations
{
    /// <summary>
    /// Interface for making factories for <see cref="IFileStructureValidation"/>
    /// </summary>
    internal interface IFileStructureValidationFactory
    {
        /// <summary>
        /// Creates instances of <see cref="IFileStructureValidation"/> based on file type
        /// </summary>
        public IFileStructureValidation CreateFileStructureValidation(string path);
    }
}
