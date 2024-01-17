namespace DomainLayer.Validations.FileStructureValidations
{
    internal interface IFileStructureValidationFactory
    {
        public IFileStructureValidation CreateFileStructureValidation(string path);
    }
}
