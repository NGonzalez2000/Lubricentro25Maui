namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

public partial class SingleBillTypePage : ContentPage
{
	public SingleBillTypePage(SingleBillTypeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}