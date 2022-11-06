using Application.Behaviors.Caching;
using Application.Behaviors.Logging;
using Application.Features.UserProfiles.Models;
using Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.Queries.GetListUserProfileQuery
{
    public class GetListUserProfileQueryRequest:IRequest<UserProfileListModel> , ILoggableRequest , ICachableRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; }

        public string CacheKey => "getListUserProfile";

        public TimeSpan? SlidingExpiration { get; }
    }
}
