using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories
{
    public interface IValidationRepository
    {
        public List<ValidationSet> GetValidations();
    }
}
