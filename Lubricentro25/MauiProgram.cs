using Lubricentro25.Api;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.Configuration.Views;
using Lubricentro25.Pages.Login;
using Lubricentro25.ViewModels.Configurations;
using Lubricentro25.ViewModels.Login;
using Microsoft.Extensions.Logging;

namespace Lubricentro25
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            //Api
            builder.Services.AddLubricentroApi(x => x.ApiBaseAddress = "https://localhost:7279/");

            //ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<EmployeeConfigurationViewModel>();
            builder.Services.AddSingleton<EmployeeEditorViewModel>();

            //Pages
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<EmployeeConfigurationPage>();
            builder.Services.AddSingleton<EmployeeEditorView>();
            

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
