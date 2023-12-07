namespace DomainLayer.FileStructureValidations
{
    internal class XmlStructureValidationFactory : FileStructureValidationFactory
    {
        public override IFileStructureValidation CreateFileStructureValidation(string path)
        {
            return new XMLFileStructureValidation();
        }
    }
}
