using System.Resources;
using CommunityToolkit.Maui;
using CoreM.Startup;
using DemoApp.Localization;
using DemoApp.Pages;
using Microsoft.Extensions.Logging;
using Visuals.Startup;

namespace DemoApp;

public static class MauiProgram
{
    #region Methods

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

       RegisterRequiredTypes(
            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .VisualsInit()
                .RegisterViews()
                .RegisterViewModels()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }));

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var stringResources = new List<ResourceManager>
        {
            AppResource.ResourceManager,
            ErrorResource.ResourceManager
        };

        var app = CoreAppBuilder.Build(builder, stringResources);

        return app;
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