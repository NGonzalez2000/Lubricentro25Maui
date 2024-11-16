using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Models.Messages;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.DedicatedPages.BrandPages;
using Lubricentro25.Pages.DedicatedPages.ClientPages;
using Lubricentro25.Pages.DedicatedPages.CompanyPages;
using Lubricentro25.Pages.DedicatedPages.ConfigurationPages;
using Lubricentro25.Pages.DedicatedPages.ProductPages;
using Lubricentro25.Pages.DedicatedPages.ProviderPages;
using System.Reflection;

namespace Lubricentro25;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddConfigurationPagesMessage>(this, AddEmployeeConfigurationPages);

        RegisterRoutes();
        // Hook into the window's key event
    }
    
    private void RegisterRoutes()
    {
        var assembly = Assembly.GetAssembly(typeof(SingleClientPage));
        var singlePagesType = assembly!.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Lubricentro25.Pages.DedicatedPages") && !t.IsAbstract && t.IsClass && t.Name.EndsWith("Page"));

        // Register each page dynamically
        foreach (var singlePageType in singlePagesType)
        {
            Routing.RegisterRoute(singlePageType.Name, singlePageType);
        }
    }

    private void AddEmployeeConfigurationPages(object recipient, AddConfigurationPagesMessage message)
    {
        if (message.Value.IsAllowed && message.Value.Policy == "EmployeeModificationsPolicy")
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
            return;
        }

        if(message.Value.IsAllowed && message.Value.Policy == "MigrationPolicy")
        {
            var shellContent = new ShellContent()
            {
                ContentTemplate = new DataTemplate(typeof(MigrationPage)),
                Title = "Migraciones",
                Route = nameof(MigrationPage)
            };
            ConfigurationTab.Items.Add(shellContent);
            return;
        }
        if (message.Value.IsAllowed && message.Value.Policy == "CompanyPolicy")
        {
            var shellContent = new ShellContent()
            {
                ContentTemplate = new DataTemplate(typeof(CompaniesPage)),
                Title = "Compañías",
                Route = nameof(CompaniesPage)
            };
            ConfigurationTab.Items.Add(shellContent);
        }

        if (!message.Value.IsAllowed && message.Value.Policy == "ChatPolicy")
        {
            ChatTab.Items.Remove(ChatTab.Items.First());
        }
        //WeakReferenceMessenger.Default.Unregister<AddConfigurationPagesMessage>(this);

    }
}
