namespace Lubricentro25.Pages.DedicatedPages.BrandPages;

public partial class SingleBrandPage : ContentPage
{
	public SingleBrandPage(SingleBrandViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        Loaded += SingleBrandPage_Loaded;
	}

    private async void SingleBrandPage_Loaded(object? sender, EventArgs e)
    {
		if(BindingContext is SingleBrandViewModel vm) await vm.Refresh();
    }
}