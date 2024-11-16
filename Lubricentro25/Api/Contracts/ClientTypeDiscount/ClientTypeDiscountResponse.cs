namespace Lubricentro25.Api.Contracts.ClientTypeDiscount;

public record ClientTypeDiscountResponse(string Id, string ClientTypeId, string Description, decimal Discount)
{
}
