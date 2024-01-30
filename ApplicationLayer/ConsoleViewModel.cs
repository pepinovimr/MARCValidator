using ApplicationLayer.IO;
using ApplicationLayer.Mapping;
using ApplicationLayer.Services.Interfaces;
using ApplicationLayer.Validations;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Managers;
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
        private readonly IValidationManager _validationManager;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<MessageEventArgs> Notify;

        public ConsoleViewModel(ILogger<ConsoleViewModel> logger, ILocalizationService localizationService, IValidationManager validationManager)
        {
            _localizationService = localizationService;
            _logger = logger;
            _validationManager = validationManager;
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
        ///this should be refactored
        public void ValidateMARC()
        {

            NotifyView(new MessageEventArgs(
                                    new Message(
                                        _localizationService["InputFilePath"]
                                        , MessageType.Normal
                                        ), addLineTerminator: false)
                                        );

            string path = ConsoleReader.ReadFromConsole();

            UserInputChainValidation inputValidations = new(new FileExistsValidation(), new FileFormatValidation());

            Result result = inputValidations.Validate(path);
            if(result.Type == Severity.Error)
            {
                Notify?.Invoke(this, new MessageEventArgs(result.ToMessage()));
                return;
            }
            var results = _validationManager.Validate(path).Distinct().ToList();
            var rs = results.ToMessages();
            foreach (var res in rs)
            {
                Notify?.Invoke(this, new MessageEventArgs(res.Key));
                foreach(var s in res.Value)
                {
                    Notify?.Invoke(this, new MessageEventArgs(s));
                }
                Notify?.Invoke(this, new MessageEventArgs(new Message("", MessageType.Normal)));
            }
            //result = v.Validate();

            //List<Record> m = v.GetMarc().ToList();

            //if (result.Type == Severity.Error)
            //    Notify?.Invoke(this, new MessageEventArgs(result.ToMessage()));
            //else
            //    Notify?.Invoke(this, new MessageEventArgs(Result.Success.ToMessage()));
        }

        public void NotifyView(MessageEventArgs messageEventArgs)
        {
            Notify?.Invoke(this, messageEventArgs);
        }
    }
}