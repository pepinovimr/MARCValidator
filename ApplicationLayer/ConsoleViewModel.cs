using ApplicationLayer.Models;
using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace ApplicationLayer
{
    /// <summary>
    /// Handles application logic for ConsoleView
    /// </summary>
    public class ConsoleViewModel
    {

        private ILogger<ConsoleViewModel> _logger;

        /// <summary>
        /// Handles notifications for views.
        /// Should be the only interaction with Views
        /// </summary>
        public event EventHandler<(string, MessageType)> Notify;

        /// <summary>
        /// Constructor for <see cref="ConsoleViewModel"/>
        /// </summary>
        public ConsoleViewModel( ILogger<ConsoleViewModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Starts application
        /// </summary>
        public void StartApplication()
        {
            _logger.Log(LogLevel.Information, "Application Started");
            Notify?.Invoke(this, ("ApplicationName", MessageType.Header));
        }
    }
}