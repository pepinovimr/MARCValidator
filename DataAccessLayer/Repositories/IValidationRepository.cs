using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories
{
    public interface IValidationRepository
    {
        public IEnumerable<ValidationSet> GetValidations();
    }
}
