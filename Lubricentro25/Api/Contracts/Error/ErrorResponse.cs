namespace Lubricentro25.Api.Contracts.Error;

public record ErrorResponse(string Title, Dictionary<string,List<string>> Errors)
{
}
