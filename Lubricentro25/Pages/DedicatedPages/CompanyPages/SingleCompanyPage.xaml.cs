namespace Lubricentro25.Pages.DedicatedPages.CompanyPages;

public partial class SingleCompanyPage : ContentPage
{
	public SingleCompanyPage(SingleCompanyViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        Appearing += SingledCompanyPage_Appearing;
	}

    private async void SingledCompanyPage_Appearing(object? sender, EventArgs e)
    {
        if(BindingContext is SingleCompanyViewModel vm)
        {
            await vm.LoadDataAsync();
        }
    }
}