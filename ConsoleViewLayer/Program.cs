using ApplicationLayer;
using ConsoleViewLayer.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedLayer;

namespace ConsoleView
{
    /// <summary>
    /// Main entry point to program using console
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

        /// <summary>
        /// Dependency injection for classes only used in ConsoleApp
        /// </summary>
        /// <param name="builder">Builder from <see cref="Startup.ConfigureHost()"/> method.</param>
        /// <returns>Builded Host to be used in <see cref="Program.Main()"/></returns>
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
