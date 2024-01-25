﻿using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Infrastrucure
{
    public interface IDataValidationBuilder
    {
        public IDataValidationBuilder ValidateObligation();
        public IDataValidationBuilder ValidatePattern();
        public IDataValidationBuilder ValidateConditions();
        public IDataValidationBuilder ValidateAlternatives();
        public IEnumerable<Result> GetResults();

        public string GetSourceField();

        public string? GetSourceFieldValue();
    }
}