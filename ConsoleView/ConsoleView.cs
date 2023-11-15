using ApplicationLayer;
using ApplicationLayer.Models;
using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsoleView
{
    /// <summary>
    /// View for MARC Validator console app
    /// </summary>
    internal class ConsoleView
    {
        /// <summary>
        /// Injected ViewModel
        /// </summary>
        private readonly ConsoleViewModel _viewModel;

        /// <summary>
        /// Injected logger
        /// </summary>
        private readonly ILogger _logger;

        ILocalizationService _localizationService;

        /// <summary>
        /// Initializes Injected ViewModel and other fields and subscribes to viewModel events
        /// </summary>
        public ConsoleView(ConsoleViewModel viewModel, ILogger<ConsoleViewModel> logger, ILocalizationService localizationService) 
        {
            _viewModel = viewModel;
            _logger = logger;
            _localizationService = localizationService;

            viewModel.Notify += ViewModel_Notify;
        }

        /// <summary>
        /// Handles notifiactions from ViewModel
        /// </summary>
        private void ViewModel_Notify(object sender, (string, MessageType)message)
        {
            Console.WriteLine(_localizationService[message.Item1]);
        }
    }
}
