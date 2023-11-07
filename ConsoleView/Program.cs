using ApplicationLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLayer;

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
            IHost host = BuildApplication(builder);


            ConsoleView consoleView = host.Services.GetRequiredService<ConsoleView>();

            consoleView.StartApplication();

            Console.ReadKey();
        }

        private static IHost BuildApplication(HostApplicationBuilder builder)
        {
            //Add ViewModels
            builder.Services.AddTransient<ConsoleViewModel>();

            //Add Views
            builder.Services.AddTransient<ConsoleView>();

            return builder.Build();
        }
    }
}
