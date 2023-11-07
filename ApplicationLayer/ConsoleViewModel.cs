using ApplicationLayer.Services.Interfaces;

namespace ApplicationLayer
{
    /// <summary>
    /// Handles application logic for ConsoleView
    /// </summary>
    public class ConsoleViewModel
    {
        private readonly ILocalizationService _localization;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<string> Notify;

        /// <summary>
        /// Constructor for <see cref="ConsoleViewModel"/>
        /// </summary>
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