using ComunicationDataLayer.POCOs;
using DomainLayer.Validations.DataValidations.Infrastrucure;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    internal class ValidationBuilderDirector
    {
        private IDataValidationBuilder _dataValidationBuilder;

        public IDataValidationBuilder DataValidationBuilder
        {
            set { _dataValidationBuilder = value; }
        }

        public List<Result> BuildValidation()
        {
            throw new NotImplementedException();
        }
    }
}
