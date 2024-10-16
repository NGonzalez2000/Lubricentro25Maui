namespace Lubricentro25.Models.Migration;

public partial class MigrationHandlerContainer : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    List<MigrationHandler> migrationHandlers;

    public MigrationHandlerContainer(string name, IEnumerable<MigrationHandler> migrationHandlers)
    {
        Name = name;
        MigrationHandlers = new(migrationHandlers);
    }
}
