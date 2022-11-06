using Application.Behaviors.Authorization;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.UserRegisterCommand
{
    public class UserRegisterCommandRequest : IRequest<UserRegisterCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
    }
}