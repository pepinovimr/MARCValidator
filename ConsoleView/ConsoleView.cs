using ApplicationLayer;
using Microsoft.Extensions.Logging;

namespace ConsoleView
{
    /// <summary>
    /// View for MARC Validator console app
    /// </summary>
    internal class ConsoleView
    {
        private readonly ConsoleViewModel _viewModel;

        private readonly ILogger _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleView(ConsoleViewModel viewModel, ILogger<ConsoleViewModel> logger) 
        {
            _viewModel = viewModel;
            _logger = logger;

            viewModel.Notify += ViewModel_Notify;
        }

        /// <summary>
        /// Handles notifiactions from ViewModel
        /// </summary>
        private void ViewModel_Notify(object sender, string message)
        {
            Console.WriteLine(message);
        }

        public void StartApplication()
        {
            _logger.Log(LogLevel.Information, "Application Started");
            _viewModel.PerformApplicationLogic();
        }
    }
}
