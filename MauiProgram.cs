using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WorldTime.ViewModels;

namespace WorldTime;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.UseMauiCommunityToolkit();

        builder.Services.AddSingleton<WorldTimePageViewModel>();
        builder.Services.AddSingleton<WorldTimePage>();

        builder.Services.AddTransient<SettingsPage>();

        //builder.Services.AddTransient<DetailsViewModel>();
        //builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<WatchFace>();

        return builder.Build();
    }
}
