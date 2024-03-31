using CommunityToolkit.Maui.Views;
using Lubricentro25.Pages.Login.PopUps;
using Lubricentro25.ViewModels.Login;

namespace Lubricentro25.Pages.Login;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("//main");
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		this.ShowPopupAsync(new ConfigurationPopUp());
    }
}