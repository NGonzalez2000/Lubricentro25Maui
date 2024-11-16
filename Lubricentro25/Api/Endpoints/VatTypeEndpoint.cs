using Lubricentro25.Api.Contracts.VatTypes;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints
{
    public class VatTypeEndpoint(ILubricentroApiClient apiClient) : IVatTypeEndpoint
    {
        public async Task<ApiResponse<VatType>> GetAllAsync()
        {
            return await apiClient.Get<VatType, VatTypeResponse>("Vat/Types");
        }

        public Task<ApiResponse<VatType>> UpdateVatTypeAsync(VatType vatType)
        {
            UpdateVatTypeRequest request = new(vatType.Id, vatType.Description, vatType.Aliquota, vatType.AfipCode);
            return apiClient.Post<VatType, VatTypeResponse>("Vat/Types/Update", request);
        }
    }
}
