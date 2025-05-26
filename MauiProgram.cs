using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

using Mopups.Hosting;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

namespace AlcalaTFG
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
                }).UseMauiCommunityToolkit().UseFFImageLoading().ConfigureMopups();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        window.ExtendsContentIntoTitleBar = false;
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.SetBorderAndTitleBar(false, false);
                            p.IsResizable = false;
                        }

                        const int width = 1600;
                        const int height = 900;

                        winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                    });
                });
            });
#endif

            return builder.Build();
        }
    }
}
