using Lubricentro25.Api.Contracts.ClientTypeDiscount;

namespace Lubricentro25.Api.Contracts.Discount;

public record UpdateDiscountRequest(string Id,
                                    string Description,
                                    decimal FirstDiscount,
                                    decimal SecondDiscount,
                                    decimal ThirdDiscount,
                                    decimal FourthDiscount,
                                    List<ClientTypeDiscountRequest> ClientTypeDiscounts)
{
    public UpdateDiscountRequest(Models.Discount discount) : this(discount.Id, discount.Description, discount.FirstDiscount, discount.SecondDiscount, discount.ThirdDiscount, discount.FourthDiscount, [])
    {
        foreach (var clientTypeDiscount in discount.ClientTypeDiscounts)
        {
            ClientTypeDiscounts.Add(new ClientTypeDiscountRequest(clientTypeDiscount.Id, clientTypeDiscount.ClientTypeId, clientTypeDiscount.Discount));
        }
    }
}
