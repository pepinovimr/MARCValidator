using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer;

namespace ConsoleView
{
    /// <summary>
    /// View for MARC Validator console app
    /// </summary>
    internal class ConsoleView
    {
        private readonly ConsoleViewModel _viewModel;
        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleView(ConsoleViewModel viewModel) 
        {
            _viewModel = viewModel;

            viewModel.Notify += ViewModel_Notify;

            viewModel.PerformApplicationLogic();
        }

        /// <summary>
        /// Handles notifiactions from ViewModel
        /// </summary>
        private void ViewModel_Notify(object sender, string message)
        {
            Console.WriteLine("Received notification: " + message);
        }
    }
}
