using Application.Features.Users.CreateAccessTokenByRefreshToken;
using Application.Features.Users.UserLoginCommand;
using Application.Features.Users.UserRegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
         
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserRegisterCommandRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(UserLoginCommandRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("getaccesstoken")]
        public async Task<IActionResult> CreateAccessTokenByRefreshToken(CreateAccessTokenByRefreshTokenRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        
    }
}
