using CommunityToolkit.Mvvm.Input;

namespace Lubricentro25.Models.Migration;

public partial class MigrationHandler : ObservableObject
{

    [ObservableProperty]
    private string migrationName;

    private readonly string _naviagtionUri;

    public MigrationHandler(string migrationName, string naviagtionUri)
    {
        MigrationName = migrationName;
        _naviagtionUri = naviagtionUri;
    }

    [RelayCommand]
    private void Migrate()
    {
        Shell.Current.GoToAsync(_naviagtionUri);
    }
}
