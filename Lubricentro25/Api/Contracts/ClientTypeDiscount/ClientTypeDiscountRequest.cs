namespace Lubricentro25.Api.Contracts.ClientTypeDiscount;

public record ClientTypeDiscountRequest(string Id, string ClientTypeId, decimal Discount)
{
}
