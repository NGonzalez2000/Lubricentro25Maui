using Lubricentro25.Pages.Configuration;

namespace Lubricentro25;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        var shellContent = new ShellContent()
        {
            ContentTemplate = new DataTemplate(typeof(EmployeeConfigurationPage)),
            Title = "Empleados",
            Route = nameof(EmployeeConfigurationPage)
        };
        ConfigurationTab.Items.Add(shellContent);

    }

}
