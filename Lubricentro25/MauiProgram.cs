using Lubricentro25.Api;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Lubricentro25.ViewModels;
using Lubricentro25.Pages;
using Lubricentro25.Models.Helpers;
using Microsoft.Extensions.Configuration;
using Lubricentro25.Services;

namespace Lubricentro25
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "GoogleFont");
            });

   
            JsonConfigReader jsonConfigReader = new();
            using Stream stream = jsonConfigReader.ReadJsonFile("appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(config);

            SetUpPreferences(config);

            //Api
            builder.Services.AddLubricentroApi();
            //ViewModels
            builder.Services.AddViewModels();
            //Pages
            builder.Services.AddPages();

            builder.Services.AddServices();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }

        private static void SetUpPreferences(IConfiguration config)
        {
            var apiAddressSetting = new AddressConfigurationHelper();
            config.Bind(AddressConfigurationHelper.SectionName, apiAddressSetting);
            Preferences.Set("ApiAddress", apiAddressSetting!.ApiAddress);
        }

    }

}