using Lubricentro25.ViewModels.Configurations.Migrations;
using System.Reflection;

namespace Lubricentro25.Pages.Configuration.Migrations;

public partial class ClientMigrationPage : ContentPage
{
	public ClientMigrationPage(ClientMigrationViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    
}