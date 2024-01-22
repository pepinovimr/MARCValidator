using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal abstract class DataValidationBuilder(Record Record, ValidationBase Rules) : IDataValidationBuilder
    {
        protected List<Result> Results = new List<Result>();
        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();

        public IDataValidationBuilder ValidateConditions()
        {
            if (Rules.Conditions is null)
                return this;

            DataValidationBuilderFactory factory = new(Record);

            foreach (var rule in Rules.Conditions)
            {
                IDataValidationBuilder builder = factory.CreateValidations(rule);
                builder.ValidateObligation()
                       .ValidatePattern()
                       .ValidateConditions();

                Results.AddRange(builder.GetResults());
            }

            return this;
        }

        public IEnumerable<Result> GetResults() => Results;
    }
}
