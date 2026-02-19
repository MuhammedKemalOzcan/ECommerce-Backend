namespace ECommerceAPI.Application.Dtos.PaymentDto
{
    public class CallbackResultDto
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string OrderCode { get; set; }
    }
}