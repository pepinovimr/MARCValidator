using ApplicationLayer.IO;
using ApplicationLayer.Mapping;
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
    public class ConsoleViewModel(ILogger<ConsoleViewModel> logger, ILocalizationService localizationService)
    {
        private readonly ILocalizationService _localizationService = localizationService;
        private readonly ILogger<ConsoleViewModel> _logger = logger;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<MessageEventArgs> Notify;

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

        public void ValidateMARC()
        {

            NotifyView(new MessageEventArgs(
                                    new Message(
                                        _localizationService["InputFilePath"]
                                        , MessageType.Normal
                                        ), addLineTerminator: false)
                                        );

            string path = ConsoleReader.ReadFromConsole();

            UserInputChainValidation inputValidations = new(new FileExistsValidation(), new PathExtensionValidation(".xml"));

            Result result = inputValidations.Validate(path);
            if(result.Type == ResultType.Error)
                Notify?.Invoke(this, new MessageEventArgs(result.ToMessage()));
            else
                Notify?.Invoke(this, new MessageEventArgs(Result.Success.ToMessage()));

            //Continue
        }

        public void NotifyView(MessageEventArgs messageEventArgs)
        {
            Notify?.Invoke(this, messageEventArgs);
        }
    }
}