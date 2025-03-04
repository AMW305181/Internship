using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using GeneratorPostaciNext.Database;
using GeneratorPostaciNext.Windows;
using GeneratorPostaciNext.ViewModel;
using CommunityToolkit.Maui;

namespace GeneratorPostaciNext
{
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
                    fonts.AddFont("georgia.ttf", "Georgia");
                    fonts.AddFont("georgiab.ttf", "GeorgiaBold");
                    fonts.AddFont("georgiai.ttf", "GeorgiaItalic");
                    fonts.AddFont("georgiaz.ttf", "GeorgiaBoldItalic");

                });
            
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageVM>();

            builder.Services.AddTransient<CharacterScreen>();
            builder.Services.AddTransient<CharacterScreenVM>();

            builder.Services.AddTransient<GeneratorScreen>();
            builder.Services.AddTransient<GeneratorScreenVM>();

            builder.Services.AddTransient<OtherDBScreen>();
            builder.Services.AddTransient<OtherDBScreenVM>();

            builder.Services.AddTransient<RelationshipScreen>();
            builder.Services.AddTransient<RelationshipScreenVM>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
         
            return builder.Build();
        }
    }
}
