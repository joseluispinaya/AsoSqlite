using AsoSqlite.Mobile.Repositories;
using AsoSqlite.Mobile.ViewModels;
using AsoSqlite.Mobile.Views;
using Microsoft.Extensions.Logging;

namespace AsoSqlite.Mobile
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


            builder.Services.AddSingleton<IRepository, Repository>();

            builder.Services.AddTransient<InicioView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<LoadingView>();
            builder.Services.AddTransient<AsociacionesView>();
            builder.Services.AddTransient<FlyoutHeaderControl>();


            //View Models
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoadingViewModel>();
            builder.Services.AddTransient<FlyoutHeaderControlModel>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
