using CommunityToolkit.Mvvm.Input;

namespace Lubricentro25.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool isSidePanelVisible = false;
    
    bool isLoaded = false;

    protected abstract Task LoadDataAsync();

    [RelayCommand]
    async Task Load()
    {
        if(!isLoaded)
        {
            await LoadDataAsync();
            isLoaded = true;
        }
    }

    [RelayCommand]
    void Close()
    {
        isLoaded = false;
    }

}
