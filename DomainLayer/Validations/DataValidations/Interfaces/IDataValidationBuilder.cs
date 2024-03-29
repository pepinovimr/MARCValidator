﻿using ComunicationDataLayer.POCOs;

namespace DomainLayer.Validations.DataValidations.Interfaces
{
    /// <summary>
    /// Interface for building a validation <see cref="MARC4J.Net.MARC.Record"/>
    /// </summary>
    public interface IDataValidationBuilder
    {
        /// <summary>
        /// Validates with <see cref="ValidationBase.Obligation"/>
        /// </summary>
        public IDataValidationBuilder ValidateObligation();
        /// <summary>
        /// Validates with <see cref="ValidationBase.Pattern"/>
        /// </summary>
        /// <returns></returns>
        public IDataValidationBuilder ValidatePattern();
        /// <summary>
        /// Validates whether number of fields does not exceed max allowed coent for them
        /// </summary>
        public IDataValidationBuilder ValidateMaxCount();
        /// <summary>
        /// Validates all <see cref="ValidationBase.Conditions"/>
        /// <para>Should only be used when parent <see cref="ValidationBase"/> was succesful</para>
        /// </summary>
        public IDataValidationBuilder ValidateConditions();
        /// <summary>
        /// Validates all <see cref="ValidationBase.Alternatives"/>
        /// <para>Should only be used when parent <see cref="ValidationBase"/> failed</para>
        /// </summary>
        public IDataValidationBuilder ValidateAlternatives();
        /// <summary>
        /// Gets <see cref="ValidationBase.ValidationResults"/>
        /// </summary>
        public List<Result>? GetResults();
        /// <summary>
        /// Gets string representation of field name
        /// </summary>
        public string GetSourceFieldName();
    }
}