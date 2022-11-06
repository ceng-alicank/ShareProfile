using Application.Behaviors.Caching;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Queries.GetByUserIdUserProfileQuery
{
    public class GetByUserIdUserProfileQueryRequest:IRequest<GetByUserIdUserProfileQueryResponse>,ICachableRequest
    {
        public int UserId { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => "getByUserIdUserProfile/" + UserId.ToString();

        public TimeSpan? SlidingExpiration { get; }
    }
}
