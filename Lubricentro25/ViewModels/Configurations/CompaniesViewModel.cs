using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Controls.PopUps;
using Lubricentro25.Pages.DedicatedPages.CompanyPages;
using Lubricentro25.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Lubricentro25.ViewModels.Configurations;

public partial class CompaniesViewModel(ICompanyEndpoint _companyEndpoint, IPopUpService _popUpService) : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<Company> companies = [];

    [ObservableProperty]
    ObservableCollection<CompanyService> companyServices = [];
    protected override async Task LoadDataAsync()
    {
        var companyResponse = await _companyEndpoint.GetALlAsync();
        if(!companyResponse.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(companyResponse.ErrorMessage);
            return;
        }

        Companies = new(companyResponse.ResponseContent);

        var companyServiceResponse = await _companyEndpoint.GetServicesAsync();
        if (!companyServiceResponse.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(companyServiceResponse.ErrorMessage);
            return;
        }
        CompanyServices = new(companyServiceResponse.ResponseContent);
    }

    [RelayCommand]
    async Task Create()
    {
        Company newCompany = new();
        await Shell.Current.GoToAsync(nameof(SingleCompanyPage), 
            new Dictionary<string, object> 
                { ["Company"] = newCompany, ["IsEditingEnable"] = true });
    }

    [RelayCommand]
    async Task Update(Company? SelectedCompany)
    {
        if(SelectedCompany is null)
        {
            await _popUpService.ShowMessage("Debe seleccionar una Compañía.");
            return;
        }
        Company editableCompany = SelectedCompany.Clone();

        await Shell.Current.GoToAsync(nameof(SingleCompanyPage),
            new Dictionary<string, object>
            { ["Company"] = editableCompany, ["IsEditingEnable"] = true });

        //CompanyPopUp popUp = new()
        //{ 
        //    BindingContext = editableCompany 
        //};
        //bool accepted = await _popUpService.ShowPopUpAsync(popUp);
        //if (!accepted)
        //{
        //    return;
        //}

        //var response = await _companyEndpoint.Update(editableCompany);
        //if (!response.IsSuccessful)
        //{
        //    await _popUpService.ShowErrorMessage(response.ErrorMessage);
        //    return;
        //}
        
        //Companies.Remove(SelectedCompany);
        //Companies.Add(editableCompany);

        //await _popUpService.ShowMessage("Compañia editada con éxito.");
    }
    [RelayCommand]
    async Task Delete(Company? SelectedCompany)
    {
        if (SelectedCompany is null)
        {
            await _popUpService.ShowMessage("Debe seleccionar una Compañía.");
            return;
        }

        var result = await _popUpService.ShowWarning($"Seguro desea eliminar la Compañía: {SelectedCompany.Name} ?");
        if (!result)
        {
            return;
        }

        var response = await _companyEndpoint.Delete(SelectedCompany.Id);
        if(!response.IsSuccessful)
        {
            await _popUpService.ShowErrorMessage(response.ErrorMessage);
            return;
        }
        Companies.Remove(SelectedCompany);
        await _popUpService.ShowMessage("Compañía eliminada con éxito.");
    }
    [RelayCommand]
    private async Task SelectCompany(CompanyService? companyService)
    {
        if(companyService is null)
        {
            await _popUpService.ShowErrorMessage("CompanyService null reference.\nEl programador la cagó.");
            return;
        }
        Company? selectedCompany = await SelectCompanyAsync();
        if (selectedCompany is null)
        {
            return;
        }

        companyService.Company = selectedCompany;
    }
    private async Task<Company?> SelectCompanyAsync()
    {
        CompanySelector companySelector = new([.. Companies]);
        bool response = await _popUpService.ShowPopUpAsync(companySelector);
        if (!response)
        {
            return null;
        }
        return companySelector.SelectedComapy();
    }

    [RelayCommand]
    async Task FullDetails(Company? company)
    {
        if (company is null)
        {
            return;
        }

        await Shell.Current.GoToAsync(nameof(SingleCompanyPage), new Dictionary<string, object>() { ["Company"] = company });
    }

}
