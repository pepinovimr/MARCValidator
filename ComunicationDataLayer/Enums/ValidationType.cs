namespace ComunicationDataLayer.Enums
{
    public enum ValidationType
    {
        None = 0,
        FileNotExist = 1,
        FileWrongFormat = 2,
        FileStructureError = 3,
        XsdValidationError = 4,
        DataPatternError = 5,
        ForbidenFieldExistsError = 6,
        ObligatedFieldNotExists = 7,
        FieldDoesNotMatchPattern = 8,
        ConditionForbidenFieldExistsError = 9,
        ConditionObligatedFieldNotExists = 10,
        ConditionFieldDoesNotMatchPattern = 11,
        ConditionDataPatternError = 12
    }
}
