using ApplicationLayer;
using ComunicationDataLayer.POCOs;
using ConsoleViewLayer.IO;
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

        /// <summary>
        /// Initializes Injected ViewModel and other fields and subscribes to viewModel events
        /// </summary>
        public ConsoleView(ConsoleViewModel viewModel, ILogger<ConsoleView> logger) 
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
            ConsoleWriter.WriteToConsole(args.Message, args.ClearConsole, args.AddLineTerminator);
        }

        public void StartApplication(string[] args)
        {
            _viewModel.StartApplication();
            
            if(_viewModel.SetConfiguration(args))
                _viewModel.ValidateMARC();
        }
    }
}
