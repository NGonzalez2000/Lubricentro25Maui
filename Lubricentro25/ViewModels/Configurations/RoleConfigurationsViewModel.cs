using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api;
using Lubricentro25.Api.Interface;
using Lubricentro25.Pages.Configuration.Views;
using System;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations;

public partial class RoleConfigurationsViewModel(IRoleEndpoint rolesClient, RoleEditorViewModel roleEditorViewModel) : ObservableObject
{
    private readonly IRoleEndpoint _rolesClient = rolesClient;

    [ObservableProperty]
    ObservableCollection<Role> roles = [];
    List<Role> rolesList = [];

    [ObservableProperty]
    private bool isEnable = true;

    [ObservableProperty]
    RoleEditorViewModel roleEditorViewModel = roleEditorViewModel;

    [ObservableProperty]
    string searchText = string.Empty;

    [ObservableProperty]
    Role? selectedRole;



    [RelayCommand]
    private async Task LoadViewModel()
    {
        var apiResponse = await _rolesClient.GetAllRoles();
        if(!apiResponse.IsSuccessful)
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
        IsEnable = false;
        await RoleEditorViewModel.Load();
        var role = await RoleEditorViewModel.CreateRole();
        IsEnable = true;

        if (role is null) return;


        var response = await _rolesClient.CreateRole(role);

        if(!response.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
            return;
        }
        rolesList.Add(response.ResponseContent.First());
        Search("");

    }
    [RelayCommand]
    private async Task UpdateRole()
    {
        if(SelectedRole is null)
        {
            return;
        }
        if(SelectedRole.Name == "Sin Permisos" || SelectedRole.Name == "Master")
        {
            await Shell.Current.DisplayAlert("Error", "No se pueden editar/borrar estos Roles.", "Aceptar");
            return;
        }
        IsEnable = false;
        await RoleEditorViewModel.Load();
        var role = await RoleEditorViewModel.EditRole(SelectedRole);
        IsEnable = true;

        if (role is null) return;

        var response = await _rolesClient.UpdateRole(role);
        if (!response.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
            return;
        }

        int indx = rolesList.IndexOf(SelectedRole!);
        if (indx >= 0)
        {
            rolesList[indx] = new(response.ResponseContent.First());
            Search("");
        }
    }
    [RelayCommand]
    private async Task DeleteRole()
    {
        if (SelectedRole is null)
        {
            return;
        }
        if (SelectedRole.Name == "Sin Permisos" || SelectedRole.Name == "Master")
        {
            await Shell.Current.DisplayAlert("Error", "No se pueden editar/borrar estos Roles.", "Aceptar");
            return;
        }

        var response = await _rolesClient.DeleteRole(SelectedRole.Id);

        if (!response.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", response.ErrorMessage, "Aceptar");
            return;
        }

        rolesList.Remove(SelectedRole);
        Search("");
    }
    [RelayCommand]
    void Search(string name)
    {
        Roles = new(rolesList.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }
}
