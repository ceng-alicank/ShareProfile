using Application.Behaviors.Authorization;
using Application.Behaviors.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Commands.DeleteUserProfileCommand
{
    public class DeleteUserProfileCommandRequest : IRequest<DeleteUserProfileCommandResponse>, ISecuredRequest, ICacheRemoverRequest
    {
        public int UserId { get; set; }
        public string[] Roles { get; } = { "user", "admin" };

        public bool BypassCache { get; }
        public string CacheKey => "getListUserProfile";


    }
}
