using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.Pages.Configuration.Migrations;
using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Configuration;

public partial class MigrationPage : ContentPage
{
	public MigrationPage(MigrationsConfigurationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Loaded), Command = vm.LoadCommand });

        Routing.RegisterRoute("MigrationPage/ClientCondition", typeof(ClientConditionMigrationPage));
        Routing.RegisterRoute("MigrationPage/Client", typeof(ClientMigrationPage));

    }
}