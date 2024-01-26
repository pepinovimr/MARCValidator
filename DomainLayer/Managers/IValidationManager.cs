using ComunicationDataLayer.POCOs;

namespace DomainLayer.Managers
{
    public interface IValidationManager
    {
        public List<Result> Validate(string path);
    }
}
