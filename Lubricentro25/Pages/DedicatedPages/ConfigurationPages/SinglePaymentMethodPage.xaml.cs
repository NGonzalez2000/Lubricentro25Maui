namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

public partial class SinglePaymentMethodPage : ContentPage
{
	public SinglePaymentMethodPage(SinglePaymentMethodViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}