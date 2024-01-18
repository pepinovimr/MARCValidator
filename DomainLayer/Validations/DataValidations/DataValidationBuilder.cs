using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal abstract class DataValidationBuilder(Record Record, ValidationBase Rules) : IDataValidationBuilder
    {
        protected ICollection<Result> Results = new List<Result>();
        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public IDataValidationBuilder ValidateConditions()
        {
            if (Rules.Conditions is null)
                return this;

            foreach (var rule in Rules.Conditions)
            {
                new DataValidationBuilderFactory(Record)
                    .CreateValidations(rule)
                    .ValidateObligation()
                    .ValidatePattern()
                    .ValidateConditions();
            }

            return this;
        }
    }
}
