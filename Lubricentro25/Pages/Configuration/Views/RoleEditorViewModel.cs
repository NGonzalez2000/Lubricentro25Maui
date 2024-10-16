using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services;
using System.Collections.Generic;

namespace Lubricentro25.Pages.Configuration.Views;

public partial class RoleEditorViewModel : ObservableObject
{
    [ObservableProperty]
    Role role = new();
    [ObservableProperty]
    bool isEnable;
    TaskCompletionSource<Role?>? taskCompletionSource;

    [ObservableProperty]
    List<PolicyModel> policies = [];

    private List<Policy> _realPolicies = [];

    private readonly IRoleEndpoint _roleEndpoint;
    public RoleEditorViewModel(IRoleEndpoint roleEndpoint)
    {
        _roleEndpoint = roleEndpoint;
        policies.Add(new("Chat",false));
        policies.Add(new("Modificacion de Empleados",false));
    }

    public async Task Load()
    {
        var response = await _roleEndpoint.GetAllPolicies();
        if(!response.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", "No se pudieron cargar las politicas", "Aceptar");
            Policies = [];
            return;
        }

        _realPolicies = new(response.ResponseContent);
    }

    [RelayCommand]
    void Accept()
    {
        PolicyDictionary dictionary = new();
        foreach(var policy in Policies)
        {
            if (policy.IsSelected)
            {
                Role.Policies.Add(_realPolicies.First(p => p.Name == dictionary.TranslateToEnglish(policy.Name)));
            }
        }
        taskCompletionSource?.SetResult(Role);
        foreach (var policy in Policies)
            policy.IsSelected = false;
        Role = new();
        IsEnable = false;
    }

    [RelayCommand]
    void Cancel()
    {
        Role = new();
        foreach (var policy in Policies)
            policy.IsSelected = false;
        taskCompletionSource?.SetResult(null);
        IsEnable = false;
    }

    public async Task<Role?> CreateRole()
    {
        IsEnable = true;
        Role = new();
        taskCompletionSource = new();
        return await taskCompletionSource.Task;
    }

    public async Task<Role?> EditRole(Role role)
    {
        IsEnable = true;
        Role = new()
        {
            Id = role.Id,
            Name = role.Name
        };

        PolicyDictionary dictionary = new();

        foreach(var policy in Policies)
        {
            policy.IsSelected = role.Policies.Any(p => p.Name == dictionary.TranslateToEnglish(policy.Name)); 
        }

        taskCompletionSource = new();
        return await taskCompletionSource.Task;
    }
}

public partial class PolicyModel(string _name, bool _isSelected) : ObservableObject
{
    [ObservableProperty]
    string name = _name;
    [ObservableProperty]
    bool isSelected = _isSelected;
}

