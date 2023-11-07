using ApplicationLayer.Services.Interfaces;
using System.Globalization;
using System.Resources;

namespace ApplicationLayer.Services
{
    /// <summary>
    /// An implementation of the <see cref="ILocalizationService"/> interface.
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        /// <summary>
        /// Gets localized value through <see cref="GetLocalizedValue(string)"/>
        /// </summary>
        /// <param name="name">The key to retrieve the localized value. </param>
        /// <returns>Value by key from file depending on <see cref="CultureInfo.CurrentCulture"/> or just key if value is not found.</returns>
        public string this[string name] => GetLocalizedValue(name);

        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// Constructor for the <see cref="LocalizationService"/> class.
        /// </summary>
        /// <param name="resourceManager">The resource manager providing access to localized resources</param>
        public LocalizationService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Gets localized value using <see cref="_resourceManager"/>.
        /// </summary>
        /// <param name="key">The key to retrieve the localized value.</param>
        /// <returns>Value by key from file depending on <see cref="CultureInfo.CurrentCulture"/> or just key if value is not found.</returns>
        private string GetLocalizedValue(string key)
        {
            string? localizedString = _resourceManager.GetString(key);
            return localizedString != null ? localizedString : key;
        }

        /// <summary>
        /// Sets Culture Info to <see cref="CultureInfo.CurrentCulture"/> and <see cref="CultureInfo.CurrentUICulture"/>.
        /// </summary>
        /// <param name="culture">Culture to be set.</param>
        public void SetCultureInfo(string culture)
        {
            CultureInfo.CurrentCulture =
                CultureInfo.CurrentUICulture =
                            new CultureInfo(culture);
        }
    }
}
