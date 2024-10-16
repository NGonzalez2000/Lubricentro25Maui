using System.Collections.ObjectModel;

namespace Lubricentro25.Models.Collections;

public partial class ProviderSelector : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<ProviderSelectorItem> providers;

    public ProviderSelector(IEnumerable<Provider> providers, List<Provider>? selectedProviders = null)
    {
        Providers = [];
        foreach (var provider in providers.OrderBy(p => p.Name))
        {
            Providers.Add(new() { Provider = provider });
        }

        if(selectedProviders != null)
        {
            foreach(var provider in selectedProviders)
            {
                for(int i = 0; i < Providers.Count; i++)
                {
                    if(Providers[i].Provider.Id == provider.Id) Providers[i].IsSelected = true;
                }
            }
        }
    }
    public IEnumerable<Provider> GetSelectedProviders()
    {
        foreach(var item in Providers)
        {
            if(item.IsSelected) yield return item.Provider;
        }
    }
}

public partial class ProviderSelectorItem : ObservableObject
{
    [ObservableProperty]
    Provider provider = null!;

    [ObservableProperty]
    bool isSelected = false;
}
