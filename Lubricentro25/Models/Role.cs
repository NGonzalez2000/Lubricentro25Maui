namespace Lubricentro25.Models;

public class Role
{
    public string Id {  get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Policy> Policies { get; set; } = [];
}
