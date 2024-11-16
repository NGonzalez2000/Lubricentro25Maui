using Lubricentro25.Api.Contracts.PaymentMethods;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class PaymentEndpoint(ILubricentroApiClient apiClient) : IPaymentEndpoint
{
    public async Task<ApiResponse<PaymentMethod>> GetPaymentMethodsAsync()
    {
        return await apiClient.Get<PaymentMethod, PaymentMethodResponse>("Payment/Methods");
    }

    public Task<ApiResponse<PaymentMethod>> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
    {
        UpdatePaymentMethodRequest request = new(paymentMethod.Id, paymentMethod.Description);
        return apiClient.Post<PaymentMethod, PaymentMethodResponse>("Payment/Methods/Update", request);
    }
}
