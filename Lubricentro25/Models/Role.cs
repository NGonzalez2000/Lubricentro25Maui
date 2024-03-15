namespace Lubricentro25.Models;

public class Role
{
    public Role()
    {
        
    }
    public Role(Role role)
    {
        Id = role.Id;
        Name = role.Name;
        Policies = new(role.Policies);
    }


    public string Id {  get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Policy> Policies { get; set; } = [];
}
