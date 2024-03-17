namespace Lubricentro25.Api.Contracts.Chat;

public record ChatMessageResponse(string SenderId, string ReceptorId, string Message)
{
}
