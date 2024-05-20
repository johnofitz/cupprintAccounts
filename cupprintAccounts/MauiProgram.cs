using Microsoft.Extensions.Logging;
using Mopups.Hosting;

namespace cupprintAccounts
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
                })
                .ConfigureMopups();
                

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<HomePageView>();
            builder.Services.AddTransient<PopUpView>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddTransient<PopUpViewModel>();
            builder.Services.AddTransient<InvoicePageView>();
            builder.Services.AddTransient<InvoiceViewModel>();


            return builder.Build();
        }
    }
}
