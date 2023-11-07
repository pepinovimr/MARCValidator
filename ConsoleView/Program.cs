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

            ConsoleView consoleView = new ConsoleView(host.Services.GetRequiredService<ConsoleViewModel>());

            Console.ReadKey();
        }

        private static IHost BuildApplication(HostApplicationBuilder builder)
        {
            builder.Services.AddTransient<ConsoleViewModel>();

            return builder.Build();
        }
    }
}
