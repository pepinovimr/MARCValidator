using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharedView
{
    /// <summary>
    /// Handles operations needed for all Views
    /// </summary>
    public static class Startup
    {
        /// <summary>
        /// Configures services that are shared between all Views
        /// </summary>
        /// <returns></returns>
        public static HostApplicationBuilder ConfigureHost()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            


            return builder;
        }

    }
}