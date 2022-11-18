using Application.Behaviors.Authorization;
using Application.Features.UserProfiles.Commands.CreateUserProfileCommand;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.UserProfiles.Commands.UpdateUserProfileCommand
{
    public class UpdateUserProfileCommandRequest : IRequest<Response<UpdateUserProfileCommandResponse>>, ISecuredRequest
    {
        public int UserId { get; set; }
        public string InstagramProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string[] Roles { get; } = { "user", "admin" };
    }
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommandRequest>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(u => u.InstagramProfile).NotEmpty();
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.LinkedInProfile).NotEmpty();
        }
    }
}
