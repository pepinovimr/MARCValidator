using ApplicationLayer;
using ComunicationDataLayer.POCOs;
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


        /// <summary>
        /// Initializes Injected ViewModel and other fields and subscribes to viewModel events
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
        private void ViewModel_Notify(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.Message.Text);
        }
    }
}
