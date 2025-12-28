using ECommerceAPI.Application.Features.Commands.Carts.AddItemToCart;
using ECommerceAPI.Application.Features.Commands.Carts.ClearCart;
using ECommerceAPI.Application.Features.Commands.Carts.MergeCarts;
using ECommerceAPI.Application.Features.Commands.Carts.RemoveCartItem;
using ECommerceAPI.Application.Features.Commands.Carts.UpdateCartItem;
using ECommerceAPI.Application.Features.Queries.Carts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserIdFromToken();
            var sessionId = GetOrCreateSessionId();

            var query = new GetCartQueryRequest
            {
                UserId = userId,
                SessionId = sessionId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddItemToCartCommandRequest request)
        {
            var userId = GetUserIdFromToken();
            var sessionId = GetOrCreateSessionId();

            var command = new AddItemToCartCommandRequest
            {
                UserId = userId,
                SessionId = sessionId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("items/{itemId}")]
        public async Task<IActionResult> UpdateItem(Guid itemId, [FromBody] UpdateCartItemsCommandRequest request)
        {
            var userId = GetUserIdFromToken();
            var sessionId = GetOrCreateSessionId();

            var command = new UpdateCartItemsCommandRequest
            {
                UserId = userId,
                SessionId = sessionId,
                CartItemId = itemId,
                Quantity = request.Quantity,
            };
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpDelete("items/{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            var userId = GetUserIdFromToken();
            var sessionId = GetOrCreateSessionId();

            var command = new RemoveCartItemRequest
            {
                UserId = userId,
                SessionId = sessionId,
                CartItemId = itemId
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserIdFromToken();
            var sessionId = GetOrCreateSessionId();
            var command = new ClearCartCommandRequest
            {
                UserId = userId,
                SessionId = sessionId,
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Merge()
        {
            var userId = GetUserIdFromToken();
            var sessionId = HttpContext.Request.Cookies["SessionId"];

            if (!userId.HasValue)
                return Unauthorized(new { IsSuccess = false, Errors = new[] { "Kullanıcı girişi gerekli" } });

            if (string.IsNullOrEmpty(sessionId))
                return BadRequest(new { IsSuccess = false, Errors = new[] { "Misafir sepeti bulunamadı" } });

            var command = new MergeCartsCommandRequest
            {
                UserId = userId.Value,
                SessionId = sessionId
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }


        #region
        private Guid? GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return null;
            if (Guid.TryParse(userIdClaim, out var userId)) return userId;

            return null;
        }
        private string GetOrCreateSessionId()
        {
            var sessionId = HttpContext.Request.Cookies["SessionId"];

            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();

                HttpContext.Response.Cookies.Append("SessionId", sessionId, new CookieOptions
                {
                    HttpOnly = true, //JS erişemez (XSS’e karşı).
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(30),

                    Path = "/"
                });
            }
            return sessionId;
        }
        #endregion
    }
}
