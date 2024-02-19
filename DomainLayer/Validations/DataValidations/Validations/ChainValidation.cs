using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Interfaces;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.Validations
{
    /// <summary>
    /// Class for validating multiple <see cref="ValidationBase"/> in a sequence
    /// </summary>
    /// <param name="factory"></param>
    internal class ChainValidation(IDataValidationBuilderFactory factory)
    {
        private readonly IDataValidationBuilderFactory _factory = factory;

        public List<Result> PerformChainValidation(Record record, List<ValidationBase> validations)
        {
            List<Result> results = [];

            foreach (ValidationBase validation in validations) 
            {
                IDataValidationBuilder builder = _factory.CreateValidations(validation, record)
                                                            .ValidateObligation()
                                                            .ValidatePattern()
                                                            .ValidateMaxCount()
                                                            .ValidateConditions()
                                                            .ValidateAlternatives();

                results = builder.GetResults() ?? results;
            }

            return results;
        }
    }
}
