namespace Lubricentro25.Pages.DedicatedPages.ConfigurationPages;

public partial class SingleVatTypePage : ContentPage
{
	public SingleVatTypePage(SingleVatTypeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}