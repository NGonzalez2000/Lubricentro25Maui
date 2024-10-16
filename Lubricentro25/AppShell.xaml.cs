using CommunityToolkit.Mvvm.Messaging;
using Lubricentro25.Models.Messages;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.DedicatedPages.BrandPages;
using Lubricentro25.Pages.DedicatedPages.ClientPages;
using Lubricentro25.Pages.DedicatedPages.CompanyPages;
using Lubricentro25.Pages.DedicatedPages.ProductPages;
using Lubricentro25.Pages.DedicatedPages.ProviderPages;

namespace Lubricentro25;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddConfigurationPagesMessage>(this, AddEmployeeConfigurationPages);

        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(SingleClientPage), typeof(SingleClientPage));
        Routing.RegisterRoute(nameof(SingleCompanyPage), typeof(SingleCompanyPage));
        Routing.RegisterRoute(nameof(SingleProviderPage), typeof(SingleProviderPage));
        Routing.RegisterRoute(nameof(SingleBrandPage), typeof(SingleBrandPage));
        Routing.RegisterRoute(nameof(SingleProductPage), typeof(SingleProductPage));
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
