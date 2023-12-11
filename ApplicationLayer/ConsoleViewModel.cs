using ApplicationLayer.Services.Interfaces;
using ApplicationLayer.Validations;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using Microsoft.Extensions.Logging;

namespace ApplicationLayer
{
    /// <summary>
    /// Handles application logic for ConsoleView
    /// </summary>
    public class ConsoleViewModel
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILogger<ConsoleViewModel> _logger;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<MessageEventArgs?> Notify;

        /// <summary>
        /// Constructor for <see cref="ConsoleViewModel"/>
        /// </summary>
        public ConsoleViewModel(ILogger<ConsoleViewModel> logger, ILocalizationService localizationService)
        {
            _logger = logger;
            _localizationService = localizationService;
        }

        /// <summary>
        /// Starts application
        /// </summary>
        public void StartApplication()
        {
            _logger.Log(LogLevel.Information, "Application Started");
            Notify?.Invoke(this, new MessageEventArgs(
                                    new Message(
                                        _localizationService["ApplicationName"]
                                        , MessageType.Normal
                                        )));
        }

        public void ValidateMARC(string path)
        {
            UserInputChainValidation inputValidations = new(new FileExistsValidation(), new PathExtensionValidation(".xml"));

            Result result = inputValidations.Validate(path);
            if(result.Type == ResultType.Error) { }
                Notify?.Invoke(this, new MessageEventArgs(new Message("Chyba, nutno pak namapovat", MessageType.Error)));

            //Continue
        }
    }
}