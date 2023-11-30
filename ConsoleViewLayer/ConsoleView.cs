using ApplicationLayer;
using ComunicationDataLayer.POCOs;
using ConsoleViewLayer.IO.Interfaces;
using Microsoft.Extensions.Logging;

namespace ConsoleView
{
    /// <summary>
    /// View for MARC Validator console app
    /// </summary>
    public class ConsoleView
    {
        /// <summary>
        /// Injected ViewModel
        /// </summary>
        private readonly ConsoleViewModel _viewModel;

        /// <summary>
        /// Injected logger
        /// </summary>
        private readonly ILogger _logger;

        private readonly IConsoleWriter _consoleWriter;

        /// <summary>
        /// Initializes Injected ViewModel and other fields and subscribes to viewModel events
        /// </summary>
        public ConsoleView(ConsoleViewModel viewModel, IConsoleWriter consoleWrier, ILogger<ConsoleView> logger) 
        {
            _viewModel = viewModel;
            _logger = logger;
            _consoleWriter = consoleWrier;

            viewModel.Notify += ViewModel_Notify;
        }

        /// <summary>
        /// Handles notifiactions from ViewModel
        /// </summary>
        private void ViewModel_Notify(object sender, MessageEventArgs args)
        {
            _consoleWriter.WriteToConsole(args.Message);
        }

        public void StartApplication()
        {
            _viewModel.StartApplication();
        }
    }
}
