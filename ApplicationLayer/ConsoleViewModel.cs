using ApplicationLayer.Services.Interfaces;

namespace ApplicationLayer
{
    public class ConsoleViewModel
    {
        private readonly ILocalizationService _localization;

        /// <summary>
        /// Handles ViewNotification for 
        /// </summary>
        public event EventHandler<string> Notify;

        public ConsoleViewModel(ILocalizationService localizationService)
        {
            _localization = localizationService;
        }

        /// <summary>
        /// Temporary
        /// </summary>
        public void PerformApplicationLogic()
        {
            Notify?.Invoke(this, _localization["ApplicationName"]);
            _localization.SetCultureInfo("en");
            Notify?.Invoke(this, _localization["ApplicationName"]);
        }
    }
}