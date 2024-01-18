using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal class SubFieldValidationBuilder(Record Record) : IDataValidationBuilder
    {

        public IDataValidationBuilder ValidateConditions()
        {
            throw new NotImplementedException();
        }

        public IDataValidationBuilder ValidateObligation()
        {
            throw new NotImplementedException();
        }

        public IDataValidationBuilder ValidatePattern()
        {
            throw new NotImplementedException();
        }
    }
}