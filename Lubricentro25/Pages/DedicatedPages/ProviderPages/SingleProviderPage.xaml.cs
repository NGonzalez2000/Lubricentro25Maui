namespace Lubricentro25.Pages.DedicatedPages.ProviderPages;

public partial class SingleProviderPage : ContentPage
{
	public SingleProviderPage(SingleProviderViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        Loaded += SingleProviderPage_Loaded;
    }
    private async void SingleProviderPage_Loaded(object? sender, EventArgs e)
    {
        if (BindingContext is SingleProviderViewModel viewModel)
        {
            await viewModel.Refresh();
        }
    }
}