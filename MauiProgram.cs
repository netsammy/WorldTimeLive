//using CommunityToolkit.Maui;
using Plugin.MauiMTAdmob;
using WorldTime.ViewModels;


namespace WorldTime;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
         builder
            .UseMauiApp<App>()
            .UseMauiMTAdmob()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        // builder.Logging.AddDebug();
#endif

        //builder.UseMauiCommunityToolkit();

        builder.Services.AddSingleton<WorldTimePageViewModel>();
        //on platform android load WorldTimePageAndroid and on windows load WorldTimePage
#if WINDOWS
        builder.Services.AddSingleton<WorldTimePage>();
#elif ANDROID
  builder.Services.AddSingleton<WorldTimePageAndroid>();
#endif

        builder.Services.AddTransient<SettingsPage>();

        //builder.Services.AddTransient<DetailsViewModel>();
        //builder.Services.AddTransient<DetailsPage>();

        builder.Services.AddTransient<WatchFace>();





        return builder.Build();
    }
}



