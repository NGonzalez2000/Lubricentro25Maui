using Lubricentro25.Api;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.Login;
using Lubricentro25.ViewModels.Login;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Lubricentro25.Api.Endpoints;

namespace Lubricentro25
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            //Api
            builder.Services.AddLubricentroApi(x => x.ApiBaseAddress = "https://localhost:7279/");
            builder.Services.AddEndpoints();
            //ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            //Pages
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddConfigurationPages();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}