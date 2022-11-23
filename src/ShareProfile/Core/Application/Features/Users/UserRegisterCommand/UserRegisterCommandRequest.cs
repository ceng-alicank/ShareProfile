using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.UserRegisterCommand
{
    public class UserRegisterCommandRequest : IRequest<Response<UserRegisterCommandResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
    }
}