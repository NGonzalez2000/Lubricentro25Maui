using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Helpers.Interface;
using Lubricentro25.Models.Messages;

namespace Lubricentro25.ViewModels.Login; 

public partial class LoginViewModel(ILubricentroApiClient clientApi, IAuthenticationEndpoint authenticationEndpoint, IChatConnectionHelper chatConnectionHelper) : ObservableObject
{
    private readonly ILubricentroApiClient _client = clientApi;
    private readonly IAuthenticationEndpoint _authenticationEndpoint = authenticationEndpoint;
    private readonly IChatConnectionHelper _chatConnectionHelper = chatConnectionHelper;
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

        var response = await _authenticationEndpoint.PolicyValidation("ChatPolicy");
        if (!response.IsSuccess)
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
        }
        if (response.ResponseContent.First().IsAllowed)
        {
            var auth = _client.GetAuthentication();
            if (auth != null)
            {
                await _chatConnectionHelper.Connect(auth.Token);
                _chatConnectionHelper.SetUserId(auth.Id);
            }
        }

        

    }
    [RelayCommand]
    public static async Task PasswordRecovery()
    {
        await Shell.Current.DisplayAlert("Nueva Contraseña", "123456", "xd");
    }
}
