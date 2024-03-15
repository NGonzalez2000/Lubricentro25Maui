using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Models.Messages;
using Lubricentro25.Pages.Configuration;

namespace Lubricentro25;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddConfigurationPagesMessage>(this, AddEmployeeConfigurationPages);
        
    }
    private void AddEmployeeConfigurationPages(object recipient, AddConfigurationPagesMessage message)
    {
        if (message.Value)
        {
            var shellContent = new ShellContent()
            {
                ContentTemplate = new DataTemplate(typeof(EmployeeConfigurationPage)),
                Title = "Empleados",
                Route = nameof(EmployeeConfigurationPage)
            };
            ConfigurationTab.Items.Add(shellContent);

            shellContent = new ShellContent()
            {
                ContentTemplate = new DataTemplate(typeof(RoleConfigurationPage)),
                Title = "Roles",
                Route = nameof(RoleConfigurationPage)
            };
            ConfigurationTab.Items.Add(shellContent);
        }
        


        WeakReferenceMessenger.Default.Unregister<AddConfigurationPagesMessage>(this);

    }
}
