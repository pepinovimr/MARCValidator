using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using MARC4J.Net.MARC;

namespace DomainLayer.Validations.DataValidations
{
    internal abstract class DataValidationBuilder(Record MarcRecord, ValidationBase Rules) : IDataValidationBuilder
    {
        protected List<Result> Results = [];

        protected Dictionary<Type, string> ValidationBaseToStringMap = new()
        {
            { typeof(LeaderValidation), "Leader"},
            { typeof(ControlFieldValidation), "ControlField"},
            { typeof(DataFieldValidation), "DataField"},
            { typeof(SubFieldValidation), "SubField"},
        };

        protected Dictionary<FieldObligationScope, Severity> ObligationToSeverityMap = new()
        {
            {FieldObligationScope.M, Severity.Error },
            {FieldObligationScope.MA, Severity.Error },
            {FieldObligationScope.R, Severity.Warning },
            {FieldObligationScope.RA, Severity.Warning },
            {FieldObligationScope.O, Severity.Info },
            {FieldObligationScope.FORBIDDEN, Severity.Error }
        };

        public virtual IDataValidationBuilder ValidateConditions()
        {
            if (Rules.Conditions is null)
                return this;

            DataValidationBuilderFactory factory = new(MarcRecord);

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

        protected Result ValidateByFieldObligationScope(IVariableField? field) =>
            Rules.Obligation == FieldObligationScope.FORBIDDEN
                ? ValidateForbidden(field)
                : ValidateObligated(field);

        private Result ValidateForbidden(IVariableField? field) =>
            field is not null 
            ? new Result(ObligationToSeverityMap[Rules.Obligation], ValidationType.ForbidenFieldExistsError)
            : Result.Success;

        private Result ValidateObligated(IVariableField? field) => 
            field is null 
            ? new Result(ObligationToSeverityMap[Rules.Obligation], ValidationType.ObligatedFieldNotExists)
            : Result.Success;

        /// <summary>
        /// Ensures that pattern can be validated without null references
        /// </summary>
        protected bool CanValidatePattern() =>
            Results.Last() is var lastResult 
            && lastResult is not null 
            && Rules.Pattern is not null
            && lastResult.Error != ValidationType.ForbidenFieldExistsError 
            && lastResult.Error != ValidationType.ObligatedFieldNotExists;
        public IEnumerable<Result> GetResults() => Results;

        public abstract IDataValidationBuilder ValidateObligation();

        public abstract IDataValidationBuilder ValidatePattern();
    }
}
