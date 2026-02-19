using ECommerceAPI.Application.Dtos.PaymentDto;

namespace ECommerceAPI.Application.Abstractions
{
    public interface IPaymentService
    {
        //Task<PaymentResultDto> ReceivePaymentAsync(PaymentRequestDto paymentModel);
        Task<PaymentInitializeResult> InitializeCheckoutFormAsync(CreateCheckoutFormDto formModel);

        Task<PaymentResultDto> ProcessCallbackAsync(string token);
    }
}