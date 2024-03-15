using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Messages;

namespace Lubricentro25.ViewModels.Login; 

public partial class LoginViewModel(ILubricentroApiClient clientApi, IAuthenticationEndpoint authenticationEndpoint) : ObservableObject
{
    private readonly ILubricentroApiClient _client = clientApi;
    private readonly IAuthenticationEndpoint _authenticationEndpoint = authenticationEndpoint;
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

        if (!loginResponse.IsSuccess)
        {
            Error = loginResponse.ErrorMessage;
            return;
        }

        var authRepsone = await _authenticationEndpoint.PolicyValidation("EmployeeModificationsPolicy");
        if(!authRepsone.IsSuccess)
        {
            Error = authRepsone.ErrorMessage;
            return;
        }

        WeakReferenceMessenger.Default.Send(new AddConfigurationPagesMessage(authRepsone.ResponseContent.FirstOrDefault(defaultValue: new() { IsAllowed = false }).IsAllowed));

        await Shell.Current.GoToAsync("//main");

    }
    [RelayCommand]
    public static async Task PasswordRecovery()
    {
        await Shell.Current.DisplayAlert("Nueva Contraseña", "123456", "xd");
    }
}
