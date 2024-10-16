using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Helpers;
using Lubricentro25.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Login;

public partial class LoginViewModel(ILubricentroApiClient clientApi, IPopUpService popUpService, IBranchEndpoint branchRepository, IConfiguration config) : ObservableObject
{

    [ObservableProperty]
    List<string> emails = [];
    [ObservableProperty]
    private string _userName = string.Empty;
    [ObservableProperty]
    private string _password = string.Empty;
    [ObservableProperty]
    private string _error = string.Empty;
    [ObservableProperty]
    ObservableCollection<Branch> branches = [];

    [ObservableProperty]
    Branch? selectedBranch;


    [RelayCommand]
    public async Task Login()
    {
        if(SelectedBranch is null)
        {
            await popUpService.ShowErrorMessage("DEBE SELECCIONAR UNA SUCURSAL");
            return;
        }
        var loginResponse = await clientApi.Login(new(UserName, Password, SelectedBranch.Id));

        if (!loginResponse.IsSuccessful)
        {
            Error = loginResponse.ErrorMessage;
            return;
        }

        await Task.Delay(250);

        try
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            string customFolderPath = Path.Combine(appDataPath, "Lubricentro25/Resources/Data");

            string filePath = customFolderPath + "/appsettings.json";

            string jsonContent = File.ReadAllText(filePath);

            JObject jsonObject = JObject.Parse(jsonContent);

            jsonObject[LoginConfigurationHelper.SectionName]![nameof(LoginConfigurationHelper.LastUsedEmail)] = UserName;
            jsonObject[LoginConfigurationHelper.SectionName]![nameof(LoginConfigurationHelper.SelectedBranch)] = SelectedBranch.Id;

            File.WriteAllText(filePath, jsonObject.ToString());
        }
        catch
        {
        }
        await Shell.Current.GoToAsync("//main");
    }

    [RelayCommand]
    public async Task PasswordRecovery()
    {
        string? user = await Shell.Current.DisplayPromptAsync("Recuperar Contraseña", "Ingrese su usuario", "ACEPTAR", "CANCELAR", initialValue: UserName);

        if (user == null) return;

        if(SelectedBranch is null)
        {
            await Shell.Current.DisplayAlert("", "Debe seleccionar una sucursal", "OK");
            return;
        }
        var response = await clientApi.PasswordRecovery(new(SelectedBranch.Id, user));
        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
        }

        await popUpService.ShowMessage("Su nueva contraseña debe llegar al mail de usuario en cualquier momento.");
    }

    internal async Task LoadBranches()
    {
        var loginSettings = new LoginConfigurationHelper();
        config.Bind(LoginConfigurationHelper.SectionName, loginSettings);

        UserName = loginSettings.LastUsedEmail;

        var response = await branchRepository.GetBranchesAsync();

        if (!response.IsSuccessful)
        {
            Error = response.ErrorMessage;
        }
        Branches = new(response.ResponseContent);

        if (Branches.Any())
        {
            SelectedBranch = Branches.SingleOrDefault(b => b.Id == loginSettings.SelectedBranch, Branches[0]);
        }
    }
}
