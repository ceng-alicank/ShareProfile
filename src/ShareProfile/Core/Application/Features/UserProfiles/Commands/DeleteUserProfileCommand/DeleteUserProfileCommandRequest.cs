using Application.Behaviors.Authorization;
using Application.Wrappers;
using MediatR;

namespace Application.Features.UserProfiles.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommandRequest : IRequest<Response<DeleteUserProfileCommandResponse>>, ISecuredRequest
    {
        public int UserId { get; set; }
        public string[] Roles { get; } = { "user", "admin" };
    }
}
