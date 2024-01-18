using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal class ControlFieldValidationBuilder : IDataValidationBuilder
    {
        public ControlFieldValidationBuilder(Record Record)
        {
        }

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