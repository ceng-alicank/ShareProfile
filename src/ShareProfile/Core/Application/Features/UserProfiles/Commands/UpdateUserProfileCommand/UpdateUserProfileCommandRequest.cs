using Application.Behaviors.Authorization;
using Application.Behaviors.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Commands.UpdateUserProfileCommand
{
    public class UpdateUserProfileCommandRequest : IRequest<UpdateUserProfileCommandResponse>, ISecuredRequest, ICacheRemoverRequest
    {
        public int UserId { get; set; }
        public string InstagramProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string[] Roles { get; } = { "user", "admin" };
        public bool BypassCache { get; }
        public string CacheKey => "getListUserProfile";
        public TimeSpan? SlidingExpiration {get;}
    }
}
