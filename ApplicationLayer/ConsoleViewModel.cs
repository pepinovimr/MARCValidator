using ApplicationLayer.Services.Interfaces;
using Microsoft.Extensions.Logging;

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
        public event EventHandler<string> Notify;

        /// <summary>
        /// Constructor for <see cref="ConsoleViewModel"/>
        /// </summary>
        public ConsoleViewModel( ILogger<ConsoleViewModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Temporary
        /// </summary>
        public void PerformApplicationLogic()
        {
            Notify?.Invoke(this, "ApplicationName");
        }
    }
}