using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations.Migrations;

public partial class ClientConditionMigrationViewModel(IMigrationEndpoint _migrationEndpoint, IPopUpService _popUpService) : ObservableObject
{
    private CancellationTokenSource? tokenSource;
    [ObservableProperty]
    ObservableCollection<TaxCondition> newTaxConditions = [];
    [ObservableProperty]
    ObservableCollection<string> newTaxConditionsError = [];

    [ObservableProperty]
    ObservableCollection<TaxCondition> existingTaxConditions = [];
    [ObservableProperty]
    ObservableCollection<string> existingTaxConditionsError = [];




    [RelayCommand]
    private async Task ReadOldDb()
    {
        var result = await _migrationEndpoint.GetTaxCondition();
        if (!result.IsSuccessful)
        {
            await Shell.Current.DisplayAlert("Error", result.ErrorMessage, "Aceptar");
            return;
        }

        NewTaxConditions = [];
        NewTaxConditionsError = [];
        ExistingTaxConditions = [];
        ExistingTaxConditionsError = [];

        foreach (TaxCondition temp in result.ResponseContent)
        {
            if (int.TryParse(temp.Id, out int _))
            {
                NewTaxConditions.Add(temp);
                NewTaxConditionsError.Add(string.Empty);
            }
            else
            {
                ExistingTaxConditions.Add(temp);
                ExistingTaxConditionsError.Add(string.Empty);
            }

        }


    }
    [RelayCommand]
    private async Task Migrate()
    {
        tokenSource = new();

        ProgressPopUp popUp = new(NewTaxConditions.Count, "Creando nuevas Condiciones de Clientes", tokenSource);
        _popUpService.ShowParallelPopUp(popUp);
        
        for (int i = 0; i < NewTaxConditions.Count; i++)
        {
            var resposne = await _migrationEndpoint.CreateTaxCondition(new(NewTaxConditions[i].Id,
                                                                           NewTaxConditions[i].Description,
                                                                           NewTaxConditions[i].Type,
                                                                           NewTaxConditions[i].Vat));

            if (resposne.IsSuccessful)
            {
                NewTaxConditionsError[i] = string.Empty;
                NewTaxConditions[i].Id = resposne.ResponseContent.First().Id;
            }
            else
            {
                NewTaxConditionsError[i] = resposne.ErrorMessage;
            }
            _popUpService.ParallelPopupHint();
        }

        tokenSource = new();
        ProgressPopUp popUp2 = new(ExistingTaxConditions.Count, "Actualizando Condiciones de Clientes Existentes.", tokenSource);
        _popUpService.ShowParallelPopUp(popUp2);

        for (int i = 0; i < ExistingTaxConditions.Count; i++)
        {
            var resposne = await _migrationEndpoint.UpdateTaxCondition(new(ExistingTaxConditions[i].Id,
                                                                           ExistingTaxConditions[i].Description,
                                                                           ExistingTaxConditions[i].Type,
                                                                           ExistingTaxConditions[i].Vat));

            if (resposne.IsSuccessful)
            {
                ExistingTaxConditionsError[i] = string.Empty;
            }
            else
            {
                ExistingTaxConditionsError[i] = resposne.ErrorMessage;
            }
            _popUpService.ParallelPopupHint();
        }
    }

    [RelayCommand]
    private static async Task EditTaxCondition(TaxCondition taxCondition)
    {
        TaxCondition temp = new(taxCondition.Description, taxCondition.Type, taxCondition.Vat);
        TaxConditionPopUp popUp = new()
        {
            BindingContext = temp
        };

        var response = await Shell.Current.ShowPopupAsync(popUp);
        if (response is bool accepted)
        {
            if (accepted)
            {
                taxCondition.Description = temp.Description;
                taxCondition.Type = temp.Type;
                taxCondition.Vat = temp.Vat;
            }
        }
    }

    [RelayCommand]
    private static async Task EditNew(TaxCondition taxCondition)
    {
        await EditTaxCondition(taxCondition);
    }
    [RelayCommand]
    private void RemoveNew(TaxCondition taxCondition)
    {
        int indx = NewTaxConditions.IndexOf(taxCondition);
        NewTaxConditions.RemoveAt(indx);
        NewTaxConditionsError.RemoveAt(indx);
    }
    [RelayCommand]
    private void RemoveExisting(TaxCondition taxCondition)
    {
        ExistingTaxConditions.Remove(taxCondition);
    }
}
