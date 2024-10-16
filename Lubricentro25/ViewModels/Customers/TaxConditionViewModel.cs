using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Customers;

public partial class TaxConditionViewModel(ITaxConditionEndpoint _taxConditionEndpoint, IPopUpService _popUpService) : ObservableObject
{
    [ObservableProperty]
    string descriptionSearch = string.Empty;

    [ObservableProperty]
    ObservableCollection<TaxCondition> taxConditions = [];

    List<TaxCondition> _taxConditions = [];
    [RelayCommand]
    async Task Load()
    {
        var response = await _taxConditionEndpoint.GetTaxConditionsAsync();

        if(!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        _taxConditions = new(response.ResponseContent);
        Search();
    }

    [RelayCommand]
    async Task Create()
    {
        TaxCondition newTaxCondition = new();
        TaxConditionPopUp popUp = new()
        {
            BindingContext = newTaxCondition
        };

        var result = await Shell.Current.ShowPopupAsync(popUp);

        if (result is not bool accepted) return;
        if (!accepted) return;


        var response = await _taxConditionEndpoint.CreateTaxCondition(newTaxCondition);

        if (!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        _taxConditions.Add(response.ResponseContent.First());
        Search();
        
    }

    [RelayCommand]
    async Task Update(TaxCondition? taxCondition)
    {
        if(taxCondition is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione una Condicion de Cliente");
            return;
        }

        TaxCondition editedTaxCondition = new(taxCondition.Id,taxCondition.Description,taxCondition.Type,taxCondition.Vat);
        TaxConditionPopUp popUp = new()
        {
            BindingContext = editedTaxCondition
        };

        var result = await Shell.Current.ShowPopupAsync(popUp);

        if (result is not bool accepted) return;
        if (!accepted) return;


        var response = await _taxConditionEndpoint.UpdateTaxCondition(editedTaxCondition);

        if (!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        var temp = response.ResponseContent.First();
        int indx = _taxConditions.IndexOf(_taxConditions.First(t => t.Id == taxCondition.Id));

        _taxConditions[indx].Description = temp.Description;
        _taxConditions[indx].Type = temp.Type;
        _taxConditions[indx].Vat = temp.Vat;

        Search();
    }

    [RelayCommand]
    async Task Delete(TaxCondition? taxCondition)
    {
        if(taxCondition is null)
        {
            await _popUpService.ShowErrorMessage("Seleccione una Condicion de Cliente");
            return;
        }

        bool accepted = await _popUpService.ShowWarning($"Seguro que desea eliminar a la condición {taxCondition.Description}?\nTodos los clientes que tengan asignados esta condicion serán eliminados.");

        if(!accepted) return;

        var response = await _taxConditionEndpoint.DeleteTaxCondition(taxCondition);
    
        if(!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }

        _taxConditions.Remove(taxCondition);
        Search();
    }


    [RelayCommand]
    void Search()
    {
        TaxConditions = new(_taxConditions.Where(t => t.Description.Contains(DescriptionSearch, StringComparison.OrdinalIgnoreCase)));
    }
}
