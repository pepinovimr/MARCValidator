using ComunicationDataLayer.POCOs;
using DataAccessLayer.Repositories;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.FileStructureValidations;
using MARC4J.Net.MARC;

namespace DomainLayer.Managers
{
    public class ValidationManager : IValidationManager
    {
        private readonly IDataValidationBuilderFactory _dataValidationBuilderFactory;
        private readonly IValidationRepository _validationRepository;
        private readonly IMarcRepository _marcRepository;
        public ValidationManager(IDataValidationBuilderFactory dataValidationBuilderFactory, IValidationRepository validationRepository, IMarcRepository marcRepository)
        {
            _dataValidationBuilderFactory = dataValidationBuilderFactory;
            _validationRepository = validationRepository;
            _marcRepository = marcRepository;
        }
        public IEnumerable<Result> Validate(string path) =>
            PerformStructureValidations(path) is var result &&
            result.DefaultIfEmpty(null) is not null ? PerformDataValidations(path) : result;

        private List<Result> PerformStructureValidations(string path) =>
            new FileStructureValidationFactory().CreateFileStructureValidation(path).ValidateFileStructure();

        private IEnumerable<Result> PerformDataValidations(string path) =>
            ValidateRecords(_marcRepository.GetRecords(path), _validationRepository.GetValidations());

        private List<Result> ValidateRecords(IEnumerable<Record> marcRecords, IEnumerable<ValidationSet> rules)
        {
            List<Result> results = [];
            foreach (Record record in marcRecords)
            {
                foreach (ValidationSet rule in rules)
                {
                    results.AddRange(ValidateRecord(record, rule));
                }
            }

            return results;
        }
        private List<Result> ValidateRecord(Record marcRecord, ValidationSet rule)
        {
            List<Result> results = [];
            foreach (ValidationBase validation in rule.ValidationList)
            {
                IDataValidationBuilder builder = _dataValidationBuilderFactory.CreateValidations(validation, marcRecord)
                                                                              .ValidateObligation()
                                                                              .ValidatePattern()
                                                                              .ValidateConditions()
                                                                              .ValidateObligation();
                results.AddRange(builder.GetResults());
            }

            return results;
        }
    }
}
