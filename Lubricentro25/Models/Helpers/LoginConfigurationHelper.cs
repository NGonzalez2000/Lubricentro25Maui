namespace Lubricentro25.Models.Helpers;

public class LoginConfigurationHelper
{
    public const string SectionName = "Login";
    public string LastUsedEmail { get; set; } = "";
    public string SelectedBranch { get; set; } = "";
}
