using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;

namespace Lubricentro25.ViewModels.Login; 

public partial class LoginViewModel(ILubricentroApiClient clientApi) : ObservableObject
{
    private readonly ILubricentroApiClient _client = clientApi;
    [ObservableProperty]
    private string _userName = string.Empty;
    [ObservableProperty]
    private string _password = string.Empty;
    [ObservableProperty]
    private string _error = string.Empty;

    [RelayCommand]
    public async Task Login()
    {
        var loginResponse = await _client.Login(new(UserName,Password));

        if (!loginResponse.Success)
        {
            Error = loginResponse.Error;
            return;
        }

        await Shell.Current.GoToAsync("//main");

    }
    [RelayCommand]
    public static async Task PasswordRecovery()
    {
        await Shell.Current.DisplayAlert("Nueva Contraseña", "123456", "xd");
    }
}
