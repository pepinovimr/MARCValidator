using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories
{
    public interface IValidationRepository
    {
        public List<ValidationSet> GetValidations(AllowedDescriptionStandard descriptionStandard);
    }
}
