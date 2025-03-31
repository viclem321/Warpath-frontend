using Warpath_frontend.Services;
using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.Views.PlayerPage;
using Warpath_frontend.Views.VillagePage;
using Warpath_frontend.Views.LoadingPage;
using Warpath_frontend.Views.LoginPage;
using Warpath_frontend.Views.RegisterPage;
using Warpath_frontend.Views.MapPage;
using CommunityToolkit.Maui;

namespace Warpath_frontend;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .ConfigureMauiHandlers(handlers =>
            {
                // Ajout des handlers si nécessaire
            });
        // ajout du AppState (données partagés entre toutes les pages) sous forme de service
        builder.Services.AddSingleton<AppStateService>();
        // ajout des services
        builder.Services.AddSingleton<UserApiService>();
        builder.Services.AddSingleton<PlayerApiService>();
        builder.Services.AddSingleton<VillageApiService>();
        builder.Services.AddSingleton<MapApiService>();
        builder.Services.AddSingleton<SignalRService>();

        // ajout des viewModels
        builder.Services.AddTransient<PlayerViewModel>();
        builder.Services.AddTransient<VillageViewModel>();
        builder.Services.AddTransient<MapViewModel>();

        // ajout des pages
        builder.Services.AddTransient<LoadingPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<PlayerPage>();
        builder.Services.AddTransient<VillagePage>();
        builder.Services.AddTransient<MapPage>();




#if DEBUG
        // builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
