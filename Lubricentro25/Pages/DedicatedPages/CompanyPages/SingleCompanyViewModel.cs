using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Pages.DedicatedPages.CompanyPages;

[QueryProperty(nameof(IsEditingEnable), nameof(IsEditingEnable))]
[QueryProperty(nameof(Company), nameof(Company))]
public partial class SingleCompanyViewModel(ICompanyEndpoint companyEndpoint, IBranchEndpoint branchEndpoint, IPopUpService popUpService) : ObservableObject
{
    [ObservableProperty]
    bool isEditingEnable;

    [ObservableProperty]
    Company company = null!;

    Company backupCompany = null!;
    public async Task LoadDataAsync()
    {
        if(Company.Id == string.Empty)
        {
            Company.Branches = [];
            backupCompany = Company.Clone();
            return;
        }
        var response = await branchEndpoint.GetComapnyBranchesAsync(Company);

        if(!response.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response.ErrorMessage);
        }
        Company.Branches = new(response.ResponseContent.First());
        backupCompany = Company.Clone();
    }
    [RelayCommand]
    private void AddBranch()
    {
        Company.Branches.Add(new());
    }
    [RelayCommand]
    private async Task DeleteBranch(Branch? branch)
    {
        if (branch is null) return;

        if(!await popUpService.ShowWarning("Si elimina esta sucursal perderá toda la información relacionada al stock.")) return;

        if (!await popUpService.ShowWarning("Esta acción es irreversible, desea confirmar ?")) return;

        Company.Branches.Remove(branch);
    }
    [RelayCommand]
    private async Task Accept()
    {
        if(Company.Id == string.Empty)
        {
            var response = await companyEndpoint.Create(Company);
            if(!response.IsSuccessful)
            {
                await popUpService.ShowErrorMessage(response.ErrorMessage);
                return;
            }

            Company.Id = response.ResponseContent.First().Id;
            foreach(var branch in Company.Branches)
            {
                var branchResponse = await branchEndpoint.CreateBranchAsync(Company, branch);
                if(!branchResponse.IsSuccessful)
                {
                    await popUpService.ShowErrorMessage(branchResponse.ErrorMessage);
                }
            }
            await Shell.Current.GoToAsync("..");
            return;
        }


        var response2 = await companyEndpoint.Update(Company);
        if (!response2.IsSuccessful)
        {
            await popUpService.ShowErrorMessage(response2.ErrorMessage);
            return;
        }
        foreach (var backupBranch in backupCompany.Branches)
        {
            if(!Company.Branches.Any(b => b.Id == backupBranch.Id && b.Id != string.Empty))
            {
                var temp = await branchEndpoint.DeleteBranchAsync(backupBranch);
                if (!temp.IsSuccessful)
                {
                    await popUpService.ShowErrorMessage(temp.ErrorMessage);
                }
            }
        }
        foreach(var branch in Company.Branches)
        {
            var temp = branch.Id == string.Empty ? await branchEndpoint.CreateBranchAsync(Company, branch) : await branchEndpoint.UpdateBranchAsync(branch);
            if (!temp.IsSuccessful)
            {
                await popUpService.ShowErrorMessage(temp.ErrorMessage);
            }
        }
        await Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }
    partial void OnCompanyChanged(Company value)
    {
        
    }
}
