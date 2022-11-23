using Application.Features.Users.UserRegisterCommand;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.UserLoginCommand
{
    public class UserLoginCommandRequest : IRequest<Response<UserLoginCommandResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
