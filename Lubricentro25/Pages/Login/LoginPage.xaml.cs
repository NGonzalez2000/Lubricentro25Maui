using CommunityToolkit.Maui.Views;
using Lubricentro25.Models.Helpers;
using Lubricentro25.Pages.Login.PopUps;
using Lubricentro25.ViewModels.Login;
using Microsoft.Extensions.Configuration;

namespace Lubricentro25.Pages.Login;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        Loaded += LoginPage_Loaded;
    }

    private async void LoginPage_Loaded(object? sender, EventArgs e)
    {
        if (BindingContext is LoginViewModel vm)
        {
            await vm.LoadBranches();
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		this.ShowPopupAsync(new ConfigurationPopUp());
    }
}