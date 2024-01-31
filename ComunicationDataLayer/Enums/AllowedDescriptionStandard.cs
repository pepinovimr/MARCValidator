namespace ComunicationDataLayer.Enums
{
    /// <summary>
    /// List a allowed Description standards
    /// </summary>
    public enum AllowedDescriptionStandard
    {
        /// <summary>
        /// Should be used when none of other description standards are detected
        /// </summary>
        unidentified = 0,
        /// <summary>
        /// Should be used when Leader[18] == 'a'
        /// </summary>
        aacr2 = 1,
        /// <summary>
        /// Should be used when 040$e == rda
        /// </summary>
        rda = 2
    }
}
