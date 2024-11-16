using Lubricentro25.Api.Contracts.ClientTypeDiscount;

namespace Lubricentro25.Api.Contracts.Discount;

public record CreateDiscountRequest(string BrandId,
                                    string Description,
                                    decimal FirstDiscount,
                                    decimal SecondDiscount,
                                    decimal ThirdDiscount,
                                    decimal FourthDiscount,
                                    List<ClientTypeDiscountRequest> ClientTypeDiscounts)
{
    public CreateDiscountRequest(Models.Brand brand, Models.Discount discount) : this(brand.Id, discount.Description, discount.FirstDiscount, discount.SecondDiscount, discount.ThirdDiscount, discount.FourthDiscount, [])
    {
        foreach(var clientTypeDiscount in discount.ClientTypeDiscounts)
        {
            ClientTypeDiscounts.Add(new ClientTypeDiscountRequest(Guid.Empty.ToString(), clientTypeDiscount.ClientTypeId, clientTypeDiscount.Discount));
        }
    }
}
