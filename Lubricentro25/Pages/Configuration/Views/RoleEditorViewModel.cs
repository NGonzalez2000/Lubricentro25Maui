namespace Lubricentro25.Pages.Configuration.Views;

public partial class RoleEditorViewModel : ObservableObject
{
    
    [ObservableProperty]
    private List<PolicyPicker> policies = [];
    public RoleEditorViewModel()
    {
        policies.Add(new("Chat"));
        policies.Add(new("Edicion de Empleados"));
    }
}

public partial class PolicyPicker(string name) : ObservableObject
{
    [ObservableProperty]
    private string name = name;
    [ObservableProperty]
    private bool isSelected;
}
