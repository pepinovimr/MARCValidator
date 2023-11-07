using ApplicationLayer.Services.Interfaces;
using System.Globalization;
using System.Resources;

namespace ApplicationLayer.Services
{
    public class LocalizationService : ILocalizationService
    {
        public string this[string name] => GetLocalizedValue(name);

        private readonly ResourceManager _resourceManager;

        public LocalizationService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        private string GetLocalizedValue(string key)
        {
            string? localizedString = _resourceManager.GetString(key);
            return localizedString != null ? localizedString : key;
        }

        public void SetCultureInfo(string culture)
        {
            CultureInfo.CurrentCulture =
                CultureInfo.CurrentUICulture =
                    CultureInfo.DefaultThreadCurrentCulture =
                        CultureInfo.DefaultThreadCurrentUICulture =
                            new CultureInfo(culture);
        }
    }
}
