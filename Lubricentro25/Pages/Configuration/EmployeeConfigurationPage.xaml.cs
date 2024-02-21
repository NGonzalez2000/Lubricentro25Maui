using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Configuration;

public partial class EmployeeConfigurationPage : ContentPage
{
	public EmployeeConfigurationPage()
	{
		InitializeComponent();
		BindingContext = new EmployeeConfigurationViewModel();
	}
}