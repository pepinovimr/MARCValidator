using ApplicationLayer.IO;
using ApplicationLayer.Mapping;
using ApplicationLayer.Services.Interfaces;
using ApplicationLayer.Validations;
using ComunicationDataLayer.Enums;
using ComunicationDataLayer.POCOs;
using DomainLayer.Managers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NDesk.Options;

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
        private string? _marcPath = null;
        private string? _outputFile = null;
        private bool _verbose = false;

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

        /// <summary>
        /// Sets up configuration from CMD parameters
        /// </summary>
        /// <param name="args"></param>
        public bool SetConfiguration(string[] args)
        {
            bool showHelp = false;

            var optionSet = new OptionSet
            {
                { "p|path=", _localizationService["MarcPath"], v => _marcPath = v },
                { "o|output=", _localizationService["OutputPath"], v => _outputFile = v },
                { "v|verbose", _localizationService["Verbosity"], v => _verbose = v != null },
                { "h|?|help",  v => { showHelp = v != null; } }
            };

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException e)
            {
                NotifyView(new MessageEventArgs(
                                    new Message(
                                       e.Message
                                        , MessageType.Error
                                        ), addLineTerminator: false)
                                        );
                return false;
            }

            if (showHelp)
            {
                var writer = new StringWriter();
                optionSet.WriteOptionDescriptions(writer);

                NotifyView(new MessageEventArgs(
                                    new Message(
                                       writer.ToString()
                                        , MessageType.Normal
                                        ), addLineTerminator: false)
                                        );
                return false;
            }

            if (_marcPath is null)
            {
                NotifyView(new MessageEventArgs(
                                    new Message(
                                       _localizationService["MarcPathRequired"]
                                        , MessageType.Error
                                        ), addLineTerminator: false)
                                        );
                return false;
            }

            return true;
        }

        /// <summary>
        /// Starts validation of MARC record
        /// </summary>
        public void ValidateMARC()
        {
            UserInputChainValidation inputValidations = new(new FileExistsValidation(), new FileFormatValidation());

            Result result = inputValidations.Validate(_marcPath);
            if(result.Type == Severity.Error)
            {
                Notify?.Invoke(this, new MessageEventArgs(result.ToMessage()));
                return;
            }

            var results = _validationManager.StartValidation(_marcPath).Distinct().ToList();

            OutputResults(results);
        }

        private void OutputResults(List<Result> results)
        {
            results.RemoveAll(x => x.Type == Severity.Success);
            if (!_verbose)
                results.RemoveAll(x => x.Type == Severity.Info);

            Dictionary<Message, List<Message>> messages = results.ToMessages();
            foreach (var res in messages)
            {
                Notify?.Invoke(this, new MessageEventArgs(new("______________________________", MessageType.Normal)));
                Notify?.Invoke(this, new MessageEventArgs(res.Key));
                foreach (var s in res.Value)
                {
                    Notify?.Invoke(this, new MessageEventArgs(s));
                }
                Notify?.Invoke(this, new MessageEventArgs(new Message("", MessageType.Normal)));
            }
        }

        /// <summary>
        /// Notifies ViewLayer
        /// </summary>
        public void NotifyView(MessageEventArgs messageEventArgs)
        {
            Notify?.Invoke(this, messageEventArgs);
        }
    }
}