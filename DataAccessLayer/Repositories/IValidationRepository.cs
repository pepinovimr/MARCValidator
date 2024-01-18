﻿using ComunicationDataLayer.POCOs;

namespace DataAccessLayer.Repositories
{
    public interface IValidationRepository
    {
        public ICollection<ValidationSet> GetValidations();
    }
}
