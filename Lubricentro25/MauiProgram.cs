using Lubricentro25.Api;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Lubricentro25.Api.Endpoints;
using Lubricentro25.ViewModels;
using Lubricentro25.Pages;

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
            builder.Services.AddLubricentroApi(x => x.ApiBaseAddress = "https://www.lubricentroapi.com/");
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
    }
}