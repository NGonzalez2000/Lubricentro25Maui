namespace Lubricentro25.Api;

public static class LubricentroClientOptions
{
    public static string GetGetApiAddress()
    {
        return Preferences.Get("DNS Name", "http://host.lubricentroapi.api");
    }
}
