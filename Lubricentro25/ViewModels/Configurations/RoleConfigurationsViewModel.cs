using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.ViewModels.Configurations;

public partial class RoleConfigurationsViewModel(IRoleEndpoint rolesClient) : ObservableObject
{
    private readonly IRoleEndpoint _rolesClient = rolesClient;
    [ObservableProperty]
    private List<Role> roles = [];
    List<Role> rolesList = [];
    [ObservableProperty]
    private bool isEditing = false;

    [RelayCommand]
    private async Task LoadViewModel()
    {
        var apiResponse = await _rolesClient.GetAllRoles();
        if(!apiResponse.IsSuccess)
        {
            await Shell.Current.DisplayAlert("Error", apiResponse.ErrorMessage, "Ok");
            return;
        }

        rolesList = new(apiResponse.ResponseContent);
        Roles = new(rolesList);
    }

    [RelayCommand]
    private async Task CreateRole()
    {
        await Task.CompletedTask;
    }
    [RelayCommand]
    private async Task UpdateRole()
    {
        await Task.CompletedTask;
    }
    [RelayCommand]
    private async Task DeleteRole()
    {
        await Task.CompletedTask;
    }
    [RelayCommand]
    void Search(string name)
    {
        Roles = new(rolesList.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }
}
