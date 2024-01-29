using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DataAccessLayer.Repositories;
using DomainLayer.Validations.DataValidations.Helpers;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations.ValidationControl
{
    public class DataValidationDirector(IDataValidationBuilderFactory dataValidationBuilderFactory, IValidationRepository validationRepository, IMarcRepository marcRepository) : IDataValidationDirector
    {
        private readonly IDataValidationBuilderFactory _dataValidationBuilderFactory = dataValidationBuilderFactory;
        private readonly IValidationRepository _validationRepository = validationRepository;
        private readonly IMarcRepository _marcRepository = marcRepository;

        public List<Result> ValidateRecords(string path)
        {
            List<Result> results = [];
            foreach (Record record in _marcRepository.GetRecords(path))
            {
                List<Result> setResults = [];
                foreach (ValidationSet rule in _validationRepository.GetValidations(record.GetDescriptionStandard()))
                {
                    setResults.AddRange(CreateValidation(record, rule));
                }
                if (setResults.All(x => x == Result.Success))
                    results.Add(Result.Success with { SourceRecord = record.GetName()});
                else
                    results.AddRange(setResults.Where(x => x!= Result.Success));
            }

            return results;
        }
        private List<Result> CreateValidation(Record marcRecord, ValidationSet rule)
        {
            List<Result> results = [];
            foreach (ValidationBase validation in rule.ValidationList)
            {
                IDataValidationBuilder builder = _dataValidationBuilderFactory.CreateValidations(validation, marcRecord)
                                                                              .ValidateObligation()
                                                                              .ValidatePattern()
                                                                              .ValidateConditions()
                                                                              .ValidateAlternatives();
                results.AddRange(builder?.GetResults() ?? [Result.Success]);
            }

            return results.Select(x => x with { SourceRecord = marcRecord.GetName()}).ToList();
        }

    }
}
