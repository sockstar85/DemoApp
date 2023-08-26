using System.Reflection;
using System.Resources;
using CommunityToolkit.Maui;
using CoreM.Startup;
using DemoApp.Pages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Visuals.Startup;

namespace DemoApp;

/// <summary>
///     The entry point for the Maui Application.
/// </summary>
public static class MauiProgram
{
    #region Methods

    /// <summary>
    ///     Creates the maui application.
    /// </summary>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .VisualsInit()
            .RegisterViews()
            .RegisterViewModels()
            .RegisterRequiredTypes()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return BuildApp(builder);
    }

    /// <summary>
    ///     Builds the application using <see cref="CoreAppBuilder" />.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui app builder.</param>
    private static MauiApp BuildApp(MauiAppBuilder mauiAppBuilder)
    {
#if DEBUG
        mauiAppBuilder.Logging.AddDebug();
        const bool debugEnabled = true;
#else
        const bool debugEnabled = false;
#endif

        /*Using Core builder initializes any dependencies
         *that require services to be finalized prior to
         *to initialization.
         */

        return CoreAppBuilder.Build(
            mauiAppBuilder,
            GetConfigJson(),
            debugEnabled);
    }

    /// <summary>
    ///     Gets the configuration json.
    /// </summary>
    private static JObject GetConfigJson()
    {
        const string configFileLocation = $"{nameof(DemoApp)}.{nameof(Config)}.appSettings.json";

        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(configFileLocation)
                     ?? throw new MissingManifestResourceException($"Unable to locate {configFileLocation}");

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();

        return JObject.Parse(json);
    }

    /// <summary>
    ///     Registers the required types for interface resolution.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui application builder.</param>
    private static MauiAppBuilder RegisterRequiredTypes(this MauiAppBuilder mauiAppBuilder)
    {
        //Register any required types

        return mauiAppBuilder;
    }

    /// <summary>
    ///     Registers view models.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui application builder.</param>
    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        //Register any view models
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<NextPageViewModel>();

        return mauiAppBuilder;
    }

    /// <summary>
    ///     Registers views.
    /// </summary>
    /// <param name="mauiAppBuilder">The maui application builder.</param>
    private static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        //Register any views
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddTransient<NextPage>();

        return mauiAppBuilder;
    }

    #endregion
}