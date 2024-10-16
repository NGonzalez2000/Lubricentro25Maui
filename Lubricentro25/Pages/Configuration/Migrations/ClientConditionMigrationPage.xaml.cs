using Lubricentro25.ViewModels.Configurations.Migrations;

namespace Lubricentro25.Pages.Configuration.Migrations;

public partial class ClientConditionMigrationPage : ContentPage
{
	public ClientConditionMigrationPage(ClientConditionMigrationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}