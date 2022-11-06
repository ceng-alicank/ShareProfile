using Application.Features.UserProfiles.Commands.CreateUserProfileCommand;
using Application.Features.UserProfiles.Commands.DeleteUserProfileCommand;
using Application.Features.UserProfiles.Commands.UpdateUserProfileCommand;
using Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery;
using Application.Features.UserProfiles.Queries.GetListUserProfileQuery;
using Application.Features.Users.CreateAccessTokenByRefreshToken;
using Application.Features.Users.UserLoginCommand;
using Application.Features.Users.UserRegisterCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createUserProfile")]
        public async Task<IActionResult> CreateUserProfile(CreateUserProfileCommandRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("deleteUserProfile")]
        public async Task<IActionResult> DeleteUserProfile(DeleteUserProfileCommandRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("updateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommandRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("getByIdUserProfile")]
        public async Task<IActionResult> GetByIdUserProfile(GetByUserIdUserProfileQueryRequest command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("getListUserProfile")]
        public async Task<IActionResult> GetListUserProfile(GetListUserProfileQueryRequest command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
