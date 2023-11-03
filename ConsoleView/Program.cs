using ApplicationLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedView;

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
            HostApplicationBuilder builder = Startup.ConfigureHost();
            BuildApplication(builder);

            ConsoleView consoleView = new ConsoleView(builder.Services.BuildServiceProvider().GetService<ConsoleViewModel>());

            Console.ReadKey();
        }

        private static void BuildApplication(HostApplicationBuilder builder)
        {
            builder.Services.AddTransient<ConsoleViewModel>();

            builder.Build();
        }
    }
}
