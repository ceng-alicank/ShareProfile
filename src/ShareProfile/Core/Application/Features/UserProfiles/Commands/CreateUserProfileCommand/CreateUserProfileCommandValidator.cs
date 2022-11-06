using FluentValidation;

namespace Application.Features.UserProfiles.Commands.CreateUserProfileCommand
{
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
