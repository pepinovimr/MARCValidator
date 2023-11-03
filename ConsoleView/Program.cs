using System;
using ApplicationLayer;

namespace ConsoleView
{
    /// <summary>
    /// Main entro point to program using console
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Initializes MARC Validator console view
        /// </summary>
        private static void Main()
        {
            ConsoleViewModel consoleViewModel = new ConsoleViewModel();

            // Initialize the View from the View Layer, passing the ViewModel
            ConsoleView consoleView = new ConsoleView(consoleViewModel);

            // Perform logic or actions using the ViewModel
            consoleViewModel.PerformApplicationLogic();

            Console.ReadKey();
        }
    }
}
