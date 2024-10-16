using CommunityToolkit.Maui.Behaviors;
using Lubricentro25.ViewModels.Customers;

namespace Lubricentro25.Pages.Customers;

public partial class TaxConditionPage : ContentPage
{
	public TaxConditionPage(TaxConditionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        Behaviors.Add(new EventToCommandBehavior() { EventName = nameof(Loaded), Command = vm.LoadCommand });
    }
}