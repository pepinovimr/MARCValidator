namespace ComunicationDataLayer.Enums
{
    /// <summary>
    /// List of possible Validations
    /// </summary>
    public enum ValidationType
    {
        /// <summary>
        /// To be used when validation was a succes
        /// </summary>
        None = 0,
        FileNotExist = 1,
        FileWrongFormat = 2,
        FileStructureError = 3,
        XsdValidationError = 4,
        DataPatternError = 5,
        ForbidenFieldExistsError = 6,
        ObligatedFieldNotExists = 7,
        FieldDoesNotMatchPattern = 8
    }
}
