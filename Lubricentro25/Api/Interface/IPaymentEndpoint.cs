namespace Lubricentro25.Api.Interface;

public interface IPaymentEndpoint
{
    Task<ApiResponse<PaymentMethod>> GetPaymentMethodsAsync();
    Task<ApiResponse<PaymentMethod>> UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
}
