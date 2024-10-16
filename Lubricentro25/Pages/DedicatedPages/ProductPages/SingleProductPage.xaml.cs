namespace Lubricentro25.Pages.DedicatedPages.ProductPages;

public partial class SingleProductPage : ContentPage
{
	public SingleProductPage(SingleProductViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        Loaded += SingleProductPage_Loaded;
	}

    private async void SingleProductPage_Loaded(object? sender, EventArgs e)
    {
		if(BindingContext is SingleProductViewModel vm)
		{
			await vm.Refresh();
		}
    }
}