using System.Runtime.InteropServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Core;
#endif

namespace Lubricentro25;

public partial class App : Microsoft.Maui.Controls.Application
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
    protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState? activationState)
    {
        Microsoft.Maui.Controls.Window window = base.CreateWindow(activationState);
        // Manipulate Window object

#if WINDOWS
        
        
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
    
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            ShowWindow(windowHandle, 3);
        });
        window.Activated += (s, e) =>
        {
            if (window.Handler.PlatformView is Microsoft.UI.Xaml.Window element)
            {
                if (element.Content is FrameworkElement frameworkElement)
                {
                    frameworkElement.KeyDown += (s, e) =>
                    {
                        var ctrl = Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down);
                        var alt = Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Menu).HasFlag(CoreVirtualKeyStates.Down);
                        var isN = e.Key == VirtualKey.N;

                        if (ctrl && alt && isN)
                        {
                            bool state = Preferences.Get("AfipState", true);
                            state = !state;
                            Shell.Current.Title = state ? "Lubricentro 25" : "Lubricentro";
                            Preferences.Set("AfipState", state);
                        }
                    };
                }
            }
        };
        
#endif

        return window;
    }
}
