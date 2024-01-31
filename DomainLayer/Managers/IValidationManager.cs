using ComunicationDataLayer.POCOs;

namespace DomainLayer.Managers
{
    /// <summary>
    /// Interface for validation management
    /// </summary>
    public interface IValidationManager
    {
        public List<Result> StartValidation(string path);
    }
}
