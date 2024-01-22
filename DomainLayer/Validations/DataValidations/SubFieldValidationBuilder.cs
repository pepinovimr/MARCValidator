using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal class SubFieldValidationBuilder(Record Record, ValidationBase Rules) : DataValidationBuilder(Record, Rules)
    {
        public override IDataValidationBuilder ValidateObligation()
        {
            throw new NotImplementedException();
        }

        public override IDataValidationBuilder ValidatePattern()
        {
            throw new NotImplementedException();
        }
    }
}