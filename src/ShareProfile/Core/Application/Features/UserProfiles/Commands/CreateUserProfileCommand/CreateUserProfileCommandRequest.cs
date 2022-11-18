using Application.Behaviors.Authorization;
using Application.Wrappers;
using FluentValidation;
using MediatR;

namespace Application.Features.UserProfiles.Commands.CreateUserProfileCommand
{
    public class CreateUserProfileCommandRequest:IRequest<Response<CreateUserProfileCommandResponse>>,ISecuredRequest
    {
        public string? LinkedInProfile { get; set; }
        public string? InstagramProfile { get; set; }
        public int UserId { get; set; }
        public string[] Roles { get; } = { "user","admin" };
    }
    public class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommandRequest>
    {
        public CreateUserProfileCommandValidator()
        {
            RuleFor(u => u.InstagramProfile).NotEmpty();
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.LinkedInProfile).NotEmpty();
        }
    }
}
