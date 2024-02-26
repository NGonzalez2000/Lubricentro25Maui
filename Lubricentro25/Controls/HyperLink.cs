using CommunityToolkit.Mvvm.Input;

namespace Lubricentro25.Controls;

class HyperLink : Label
{
    public static readonly BindableProperty CommandProperty =
       BindableProperty.Create(nameof(Command), typeof(IAsyncRelayCommand), typeof(HyperLink), null);

    public IAsyncRelayCommand Command
    {
        get { return (IAsyncRelayCommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    public HyperLink()
    {
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            // Launcher.OpenAsync is provided by Essentials.
            Command = new Command(async () => await Command.ExecuteAsync(null))
        }) ;
    }
}
