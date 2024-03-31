using Lubricentro25.Api;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.ViewModels;
using Lubricentro25.Pages;
using System.Reflection;
using Lubricentro25.Models.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Maui.Devices.Sensors;
using System.Text;

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
            });

            string filePath = Path.Combine(AppContext.BaseDirectory, "Resources/Data/appsettings.json");
            string jsonContent = File.ReadAllText(filePath);

            using Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));


            var config = new ConfigurationBuilder()
                .AddJsonStream(stream).Build();

            builder.Configuration.AddConfiguration(config);

            SetUpPreferences(config);

            //Api
            builder.Services.AddLubricentroApi();
            builder.Services.AddEndpoints();
            //ViewModels
            builder.Services.AddViewModels();
            //Pages
            builder.Services.AddPages();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
        private static void SetUpPreferences(IConfiguration config)
        {
            var settings = new AddressConfigurationHelper();
            config.Bind(AddressConfigurationHelper.SectionName, settings);
            Preferences.Set("ApiAddress", settings!.ApiAddress);
        }

    }

}