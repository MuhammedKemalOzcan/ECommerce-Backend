using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.Application.Features.Commands.Orders.Iyzicocallback;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderHubService _orderHubService;
        private readonly IMediator _mediator;

        public PaymentController(IConfiguration configuration, IOrderHubService orderHubService, IMediator mediator)
        {
            _configuration = configuration;
            _orderHubService = orderHubService;
            _mediator = mediator;
        }

        [HttpPost("callback")]
        public async Task<IActionResult> Callback([FromForm] IyzicoCallbackCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);

                var frontendUrl = _configuration["FrontendUrl"];

                if (result.IsSuccess)
                {
                    await _orderHubService.NewOrderMessageAsync($"Yeni Sipariş Onaylandı! Kod: {result.OrderCode}");
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