using CommunityToolkit.Mvvm.Input;
using Lubricentro25.Models.Migration;

namespace Lubricentro25.ViewModels.Configurations;

public partial class MigrationsConfigurationViewModel : ObservableObject
{
    [ObservableProperty]
    List<MigrationHandlerContainer> migrationHandlerContainers;


    public MigrationsConfigurationViewModel()
    {
        MigrationHandlerContainers = new(GetContainers());
    }

    [RelayCommand]
    private async Task Load()
    {
        await Task.CompletedTask;
    }

    private static IEnumerable<MigrationHandlerContainer> GetContainers()
    {
        MigrationHandlerContainer container = new("Clientes",[
            new("Cond. Clientes", "ClientCondition"),
            new("Clientes","Client")
            ]);
        yield return container;


    }
}
