using Lubricentro25.Api.Contracts.ClientTypeDiscount;

namespace Lubricentro25.Api.Contracts.Discount;


public record DiscountResponse(string Id, string Description, decimal FirstDiscount, decimal SecondDiscount, decimal ThirdDiscount, decimal FourthDiscount, List<ClientTypeDiscountResponse> ClientTypeDiscounts)
{
}

