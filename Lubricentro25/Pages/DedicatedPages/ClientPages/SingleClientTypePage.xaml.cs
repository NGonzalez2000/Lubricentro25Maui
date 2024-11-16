namespace Lubricentro25.Pages.DedicatedPages.ClientPages;

public partial class SingleClientTypePage : ContentPage
{
	public SingleClientTypePage(SingleClientTypeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}