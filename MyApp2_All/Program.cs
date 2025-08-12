// using Avalonia;
// using System;

// namespace MyApp2;

// class Program
// {
//     // Initialization code. Don't use any Avalonia, third-party APIs or any
//     // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
//     // yet and stuff might break.
//     [STAThread]
//     public static void Main(string[] args) => BuildAvaloniaApp()
//         .StartWithClassicDesktopLifetime(args);

//     // Avalonia configuration, don't remove; also used by visual designer.
//     public static AppBuilder BuildAvaloniaApp()
//         => AppBuilder.Configure<App>()
//             .UsePlatformDetect()
//             .WithInterFont()
//             .LogToTrace();
// }
using Avalonia;
using System;
using System.Runtime.InteropServices;

namespace MyApp2;

class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [STAThread]
    public static void Main(string[] args)
    {
        AllocConsole(); // 🟢 This opens a console window

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
