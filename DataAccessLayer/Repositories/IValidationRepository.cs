using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Interface for getting <see cref="ValidationSet"/> for specified description standard
    /// </summary>
    public interface IValidationRepository
    {
        /// <summary>
        /// Gets validation by specified <see cref="AllowedDescriptionStandard"/>
        /// </summary>
        public List<ValidationSet> GetValidations(AllowedDescriptionStandard descriptionStandard);
    }
}
