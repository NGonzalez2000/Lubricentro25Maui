using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Billing;

public partial class BillingBudgetsPage : ContentPage
{
	public BillingBudgetsPage(RoleConfigurationsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		Loaded += async (s, e) => { await vm.LoadViewModelCommand.ExecuteAsync(null); }; 
		
	}
}