﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Resources;
using ApplicationLayer.Services;
using ApplicationLayer.Services.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Mapping;

namespace SharedLayer
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

            //Setup Localization
            CultureInfo.CurrentCulture = 
                CultureInfo.CurrentUICulture = 
                    CultureInfo.DefaultThreadCurrentCulture = 
                        CultureInfo.DefaultThreadCurrentUICulture = 
                            new CultureInfo("cs-CZ");
            builder.Services.AddScoped<ResourceManager>(provider => new ResourceManager("SharedLayer.Properties.ConsoleLocalization", typeof(Startup).Assembly));
            builder.Services.AddScoped<ILocalizationService, LocalizationService>();

            ResultToMessageMapper.LocalizationService 
                = builder.Services.BuildServiceProvider().GetService<ILocalizationService>() 
                ?? throw new Exception("Localization service not found");
            
            //Setup Logging
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();    //Removes all logging outputs
                loggingBuilder.AddFile("app.log");
            });


            return builder;
        }

    }
}