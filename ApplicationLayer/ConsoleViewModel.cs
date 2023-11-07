using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer
{
    /// <summary>
    /// Handles application logic for ConsoleView
    /// </summary>
    public class ConsoleViewModel
    {
        private readonly ILocalizationService _localization;

        private ILogger<ConsoleViewModel> _logger;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<string> Notify;

        /// <summary>
        /// Constructor for <see cref="ConsoleViewModel"/>
        /// </summary>
        public ConsoleViewModel(ILocalizationService localizationService, ILogger<ConsoleViewModel> logger)
        {
            _localization = localizationService;
            _logger = logger;
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