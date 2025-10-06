using ECommerceAPI.Application.Dtos.UserDto;
using ECommerceAPI.Application.Features.Commands.LoginUser;
using ECommerceAPI.Application.Features.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserCommandRequest request)
        {
            RegisterUserCommandResponse response = await _mediator.Send(request);

            if (!response.Succeed) return BadRequest(response.Error);

            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            LoginUserCommandResponse response = await _mediator.Send(request);

            if (!response.Succeed) return BadRequest(response.Error);

            return Ok(new
            {
                token = response.Token
            });
        }
    }
}
