namespace ApplicationLayer.Services.Interfaces
{
    /// <summary>
    /// Interface for handling localization services.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Gets or sets the localized value based on the provided key.
        /// </summary>
        /// <param name="name">The key to retrieve the localized value.</param>
        /// <returns>Localized value for key.</returns>
        public string this[string name] { get; }

        /// <summary>
        /// Sets the culture information for localization.
        /// </summary>
        /// <param name="culture">The culture to be set.</param>
        public void SetCultureInfo(string culture);
    }
}
