using System.Runtime.InteropServices;

namespace Lubricentro25;

public partial class App : Application
{
#if WINDOWS
[DllImport("user32.dll")]
public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
#endif
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

        // Manipulate Window object

#if WINDOWS
Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
{
    var nativeWindow = handler.PlatformView;
    nativeWindow.Activate();
    IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
    ShowWindow(windowHandle, 3);
});
#endif

        return window;
    }
}
