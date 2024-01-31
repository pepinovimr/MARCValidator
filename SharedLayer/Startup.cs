using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Resources;
using ApplicationLayer.Services;
using ApplicationLayer.Services.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Logging;
using ApplicationLayer.Mapping;
using NReco.Logging.File;
using DomainLayer.Validations.DataValidations.Infrastrucure;
using DomainLayer.Validations.DataValidations.ValidationControl;
using DataAccessLayer.Repositories;
using DomainLayer.Managers;
using DomainLayer.Validations.FileStructureValidations;

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

            builder.Services.AddScoped<IDataValidationBuilderFactory, DataValidationBuilderFactory>();
            builder.Services.AddScoped<IValidationRepository, ValidationRepository>();
            builder.Services.AddScoped<IMarcRepository, MarcRepository>();
            builder.Services.AddScoped<IValidationManager, ValidationManager>();
            builder.Services.AddScoped<IDataValidationDirector, DataValidationDirector>();
            builder.Services.AddScoped<IFileStructureValidationFactory, FileStructureValidationFactory>();

            return builder;
        }

    }
}