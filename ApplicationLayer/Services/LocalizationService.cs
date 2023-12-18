using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Resources;

namespace ApplicationLayer.Services
{
    public class LocalizationService(ResourceManager resourceManager, ILogger<LocalizationService> logger) : ILocalizationService
    {
        /// <summary>
        /// Gets localized value through <see cref="GetLocalizedValue(string)"/>
        /// </summary>
        /// <param name="name">The key to retrieve the localized value. </param>
        /// <returns>Value by key from file depending on <see cref="CultureInfo.CurrentCulture"/> or just key if value is not found.</returns>
        public string this[string name] => GetLocalizedValue(name);

        private readonly ResourceManager _resourceManager = resourceManager;

        private readonly ILogger _logger = logger;

        /// <summary>
        /// Gets localized value using <see cref="_resourceManager"/>.
        /// </summary>
        /// <param name="key">The key to retrieve the localized value.</param>
        /// <returns>Value by key from file depending on <see cref="CultureInfo.CurrentCulture"/> or just key if value is not found.</returns>
        private string GetLocalizedValue(string key)
        {
            string? localizedString = null;
            try
            {
                localizedString = _resourceManager.GetString(key);
            }
            catch (MissingManifestResourceException ex)
            {
                _logger.LogWarning(ex.Message, ex.StackTrace);
            }
            return localizedString ?? key;
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
