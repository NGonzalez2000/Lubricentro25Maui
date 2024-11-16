using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.ViewModels.Customers;

namespace Lubricentro25.Pages.Customers;

public partial class CustomersPage : BasePage
{
	public CustomersPage(CustomersViewModel vm) : base(vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    
}