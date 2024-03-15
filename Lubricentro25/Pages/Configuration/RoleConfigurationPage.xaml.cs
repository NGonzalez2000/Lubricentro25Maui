using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Configuration;

public partial class RoleConfigurationPage : ContentPage
{
	public RoleConfigurationPage(RoleConfigurationsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Loaded), Command = vm.LoadViewModelCommand });
	}
}