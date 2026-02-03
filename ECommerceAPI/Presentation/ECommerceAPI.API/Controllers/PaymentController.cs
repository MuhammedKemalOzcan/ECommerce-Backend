using ECommerceAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _configuration = configuration;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> Callback([FromForm] IyzicoCallbackRequest request)
        {
            try
            {
                var result = await _paymentService.ProcessCallbackAsync(request.Token);

                var frontendUrl = _configuration["FrontendUrl"];

                if (result.IsSuccess)
                {
                    return Redirect($"{frontendUrl}/payment/success?orderCode={result.OrderCode}");
                }
                else
                {
                    var encodedError = Uri.EscapeDataString(result.ErrorMessage ?? "Bilinmeyen Hata");
                    return Redirect($"{frontendUrl}/payment/failure?reason={encodedError}");
                }
            }
            catch (Exception ex)
            {
                var encodedEx = Uri.EscapeDataString("Beklenmeyen bir hata oluştu: " + ex.Message);
                return Redirect("http://localhost:3000/payment/error"); // Genel hata sayfası
            }
        }
    }
}

public class IyzicoCallbackRequest
{
    public string Token { get; set; }
}