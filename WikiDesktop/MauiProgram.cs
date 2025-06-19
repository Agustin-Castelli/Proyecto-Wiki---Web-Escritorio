using Microsoft.Extensions.Logging;
using WikiDesktop.Services;
using WikiShared.Interfaces;

namespace WikiDesktop;

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
            });

        // Blazor WebView para MAUI
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        // HttpClient apuntando a tu API
        builder.Services.AddScoped(sp =>
            new HttpClient { BaseAddress = new Uri("https://localhost:7240/") }); // Asegurate que sea el puerto de tu API

        // Servicios compartidos
        builder.Services.AddScoped<IArticleService, ArticleService>();
        builder.Services.AddScoped<IKeywordService, KeywordService>();

        return builder.Build();
    }
}