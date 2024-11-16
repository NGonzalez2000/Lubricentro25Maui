namespace Lubricentro25.Api.Interface;

public interface IVatTypeEndpoint
{
    Task<ApiResponse<VatType>> GetAllAsync();
    Task<ApiResponse<VatType>> UpdateVatTypeAsync(VatType vatType);
}
