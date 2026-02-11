using System;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using HCSMinecraftLauncher.ViewModels;
using HCSMinecraftLauncher.Views;

namespace HCSMinecraftLauncher
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }

    class Program
    {
        // macOS特殊初始化
        [DllImport("/System/Library/Frameworks/AppKit.framework/AppKit")]
        private static extern void NSApplicationLoad();

        public static void Main(string[] args)
        {
            // macOS激活应用
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                try { NSApplicationLoad(); } catch { }
            }
            
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }
}