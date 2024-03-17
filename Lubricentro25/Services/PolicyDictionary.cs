namespace Lubricentro25.Services;

public class PolicyDictionary
{
    private readonly Dictionary<string, string> Spanish = [];
    private readonly Dictionary<string, string> English = [];

    public PolicyDictionary()
    {
        Spanish["Chat"] = "ChatPolicy";
        Spanish["Modificacion de Empleados"] = "EmployeeModificationsPolicy";

        English["ChatPolicy"] = "Chat";
        English["EmployeeModificationsPolicy"] = "Modificacion de Empleados";
    }

    public string TranslateToSpanish(string name)
    {
        return English[name];
    }
    public string TranslateToEnglish(string name)
    {
        return Spanish[name];
    }
}
